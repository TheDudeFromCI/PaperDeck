package net.whg.paperdeck.database;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;
import java.util.UUID;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import net.whg.paperdeck.IOUtils;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;
import net.whg.we.net.packets.IPacketInitializer;

public class CardPacket implements IBinaryPacket
{
    private static final Logger logger = LoggerFactory.getLogger(CardPacket.class);
    private static final long PACKET_ID = 4097734833340416L;

    /**
     * Initializes received ping packets.
     */
    public static class Initializer implements IPacketInitializer<CardPacket>
    {
        private final CardDatabase cardDatabase;

        public Initializer(CardDatabase cardDatabase)
        {
            this.cardDatabase = cardDatabase;
        }

        @Override
        public long getPacketID()
        {
            return PACKET_ID;
        }

        @Override
        public CardPacket loadPacket(DataInput in, IPacketSender sender) throws IOException
        {
            var deck = new Deck(IOUtils.readUUID(in));
            var card = CardDatabaseIO.loadCard(in, deck);
            return new CardPacket(sender, cardDatabase, card);
        }
    }

    private final IPacketSender sender;
    private final CardDatabase cardDatabase;
    private Card card;

    private CardPacket(IPacketSender sender, CardDatabase cardDatabase, Card card)
    {
        this.sender = sender;
        this.cardDatabase = cardDatabase;
        this.card = card;
    }

    public CardPacket(Card card)
    {
        this(null, null, card);
    }

    @Override
    public IPacketSender getSender()
    {
        return sender;
    }

    @Override
    public void handle()
    {
        var deck = getDeck(card.getDeck()
                               .getID());

        var realCard = getCard(deck, card.getID());

        for (var key : card.getKeys())
            realCard.setInfoNoSave(key, card.getInfo(key));

        deck.save();
        card = realCard;

        logger.info("Updated card '{}' in deck '{}'", card.getID(), deck.getID());
    }

    /**
     * Gets or creates a card with the given ID in the given deck.
     * 
     * @param deck
     *     - The deck.
     * @param id
     *     - The card ID.
     * @return The card.
     */
    private Card getCard(Deck deck, UUID id)
    {
        var card = deck.getCard(id);
        if (card != null)
            return card;

        card = Card.createCard(deck);
        return card;
    }

    /**
     * Gets or creates a deck with the given ID in the card database.
     * 
     * @param id
     *     - The deck ID.
     * @return The deck.
     */
    private Deck getDeck(UUID id)
    {
        var deck = cardDatabase.getDeck(id);
        if (deck != null)
            return deck;

        deck = new Deck(id);
        cardDatabase.addDeck(deck);
        return deck;
    }

    /**
     * Gets the card being sent/received by this packet.
     * 
     * @return The card.
     */
    public Card getCard()
    {
        return card;
    }

    @Override
    public long getPacketID()
    {
        return PACKET_ID;
    }

    @Override
    public void write(DataOutput out) throws IOException
    {
        CardDatabaseIO.saveCard(out, card);
    }
}
