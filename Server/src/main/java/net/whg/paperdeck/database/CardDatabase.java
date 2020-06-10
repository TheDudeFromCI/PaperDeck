package net.whg.paperdeck.database;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

/**
 * A collection of card decks for this server.
 */
public class CardDatabase
{
    private final List<Deck> decks = new ArrayList<>();

    /**
     * Creates a new card database and loads all cards and decks.
     */
    public CardDatabase()
    {
        CardDatabaseIO.loadCardDatabase(this);
    }

    /**
     * Adds a new deck to this database.
     * 
     * @param deck
     *     - The deck to add.
     */
    public void addDeck(Deck deck)
    {
        decks.add(deck);
    }

    /**
     * Removes a deck from this database.
     * 
     * @param deck
     *     - The deck to remove.
     */
    public void removeDeck(Deck deck)
    {
        decks.remove(deck);
    }

    /**
     * Gets the deck in this card database with the given ID.
     * 
     * @param id
     *     - The deck ID.
     * @return The deck, or null if it couldn't be found.
     */
    public Deck getDeck(UUID id)
    {
        for (var deck : decks)
            if (deck.getID()
                    .equals(id))
                return deck;

        return null;
    }
}
