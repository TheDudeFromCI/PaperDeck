package net.whg.paperdeck.database;

import java.util.UUID;

/**
 * A unique card type within a game.
 */
public class Card
{
    /**
     * Creates a new card with a random UUID, adds it to the card deck, and saves
     * it.
     * 
     * @param deck
     *     - The card deck to add this card to.
     * @return The newly created card.
     */
    public static Card createCard(Deck deck)
    {
        var id = UUID.randomUUID();
        var card = new Card(id);
        deck.addCard(card);
        deck.save();

        return card;
    }

    private final UUID id;

    /**
     * Creates a new card object.
     * 
     * @param id
     *     - The ID of this card.
     */
    public Card(UUID id)
    {
        this.id = id;
    }

    /**
     * Gets the ID of this card.
     */
    public UUID getID()
    {
        return id;
    }
}
