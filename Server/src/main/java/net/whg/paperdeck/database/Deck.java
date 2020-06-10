package net.whg.paperdeck.database;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

/**
 * A collection of cards which can be used within a game.
 */
public class Deck
{
    /**
     * Creates a new card deck with a random UUID and saves it.
     * 
     * @return The new card deck.
     */
    public static Deck createDeck()
    {
        var id = UUID.randomUUID();
        var deck = new Deck(id);
        deck.save();

        return deck;
    }

    private final List<Card> cards = new ArrayList<>();
    private final UUID id;

    /**
     * Creates a new deck object.
     * 
     * @param id
     *     - The ID of this deck.
     */
    public Deck(UUID id)
    {
        this.id = id;
    }

    /**
     * Gets the ID of this deck.
     * 
     * @return The ID.
     */
    public UUID getID()
    {
        return id;
    }

    /**
     * Gets the card in this deck with the given ID.
     * 
     * @return The card, or null if it could not be found.
     */
    public Card getCard(UUID id)
    {
        for (var card : cards)
            if (card.getID()
                    .equals(id))
                return card;

        return null;
    }

    /**
     * Gets the number of cards in this deck.
     * 
     * @return The card count.
     */
    public int getCardCount()
    {
        return cards.size();
    }

    /**
     * Gets the card at the specified index within this deck.
     * 
     * @return The card.
     */
    public Card getCardAt(int index)
    {
        return cards.get(index);
    }

    /**
     * Adds a new card to this deck.
     * 
     * @param card
     *     - The card to add.
     */
    public void addCard(Card card)
    {
        cards.add(card);
    }

    /**
     * Removes a card from this deck.
     * 
     * @param card
     *     - The card to remove.
     */
    public void removeCard(Card card)
    {
        cards.remove(card);
    }

    /**
     * Saves this card deck.
     */
    public void save()
    {
        CardDatabaseIO.saveDeck(this);
    }
}
