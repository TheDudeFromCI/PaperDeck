package net.whg.paperdeck.database;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;
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
        var card = new Card(id, deck);
        deck.save();

        return card;
    }

    private final Map<String, byte[]> info = new HashMap<>();
    private final UUID id;
    private Deck deck;

    /**
     * Creates a new card object.
     * 
     * @param id
     *     - The ID of this card.
     * @param deck
     *     - The deck this card is in.
     */
    Card(UUID id, Deck deck)
    {
        this.id = id;
        this.deck = deck;

        deck.addCard(this);
    }

    /**
     * Gets the ID of this card.
     */
    public UUID getID()
    {
        return id;
    }

    /**
     * Gets the deck this card is in.
     * 
     * @return The deck.
     * @throws IllegalStateException
     *     If this card was deleted.
     */
    public Deck getDeck()
    {
        checkDeleted();
        return deck;
    }

    /**
     * Removes this card from the deck. Does nothing if this card has already been
     * deleted.
     */
    public void delete()
    {
        if (isDeleted())
            return;

        deck.removeCard(this);
        deck.save();

        deck = null;
    }

    /**
     * Checks if this card has been removed from the deck.
     * 
     * @return True if this card has been deleted.
     */
    public boolean isDeleted()
    {
        return deck == null;
    }

    /**
     * Checks if this card has been deleted, before actions are preformed on this
     * card.
     * 
     * @throws IllegalStateException
     *     If this card was deleted.
     */
    private void checkDeleted()
    {
        if (isDeleted())
            throw new IllegalStateException("Card has already been deleted!");
    }

    /**
     * Gets the info on this card with the given key.
     * 
     * @param key
     *     - The path of the information.
     * @return The byte array representing the information, or null if the key
     *     doesn't exist on this card.
     * @throws IllegalStateException
     *     If this card was deleted.
     */
    public byte[] getInfo(String key)
    {
        checkDeleted();
        return info.get(key);
    }

    /**
     * Sets the information on this card at the given key path to a specific value.
     * The card deck is saved after preforming this action.
     * 
     * @param key
     *     - The path of the information on this card.
     * @param value
     *     - The value to write as a byte array, or null to remove this key from the
     *     card.
     * @throws IllegalStateException
     *     If this card was deleted.
     */
    public void setInfo(String key, byte[] value)
    {
        setInfoNoSave(key, value);
        deck.save();
    }

    /**
     * Sets the information on this card at the given key path to a specific value.
     * 
     * @param key
     *     - The path of the information on this card.
     * @param value
     *     - The value to write as a byte array, or null to remove this key from the
     *     card.
     * @throws IllegalStateException
     *     If this card was deleted.
     */
    void setInfoNoSave(String key, byte[] value)
    {
        checkDeleted();

        if (value == null)
        {
            info.remove(key);
            deck.save();
            return;
        }

        info.put(key, value);
    }

    /**
     * Gets a set of all keys currently on this card.
     * 
     * @return The set of all keys.
     * @throws IllegalStateException
     *     If this card was deleted.
     */
    public Set<String> getKeys()
    {
        checkDeleted();
        return info.keySet();
    }
}
