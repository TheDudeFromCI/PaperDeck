package net.whg.paperdeck.database;

import java.io.DataInput;
import java.io.DataInputStream;
import java.io.DataOutput;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.UUID;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import net.whg.paperdeck.IOUtils;

/**
 * A static utility class for saving and loading the card database.
 */
public final class CardDatabaseIO
{
    private static final Logger logger = LoggerFactory.getLogger(CardDatabaseIO.class);

    /**
     * The file version index to write in the header when saving files.
     */
    public static final int FILE_VERSION = 1;

    /**
     * The file path to save cards to.
     */
    public static final String CARD_FOLDER = "./cards";

    /**
     * Loads all of the cards and decks in this database.
     */
    public static void loadCardDatabase(CardDatabase database)
    {
        File databaseFolder = new File(CARD_FOLDER);
        databaseFolder.mkdir();

        for (var deckFile : databaseFolder.listFiles())
        {
            var deck = loadDeck(deckFile);

            if (deck != null)
                database.addDeck(deck);
        }
    }

    /**
     * Loads the card deck for the given file.
     * 
     * @param file
     *     - The card deck file.
     * @return The loaded deck, or null if it could not be loaded.
     */
    public static Deck loadDeck(File file)
    {
        try (var reader = new DataInputStream(new FileInputStream(file)))
        {
            var name = file.getName();
            var id = UUID.fromString(name.substring(0, name.length() - 4));
            var deck = new Deck(id);

            loadCards(reader, deck);
            return deck;
        }
        catch (IllegalArgumentException e)
        {
            logger.error("Tried to load a file which isn't a card deck! '{}''", file.getAbsolutePath(), e);
            return null;
        }
        catch (Exception e)
        {
            logger.error("Failed to load card deck! '{}'", file.getAbsolutePath(), e);
            return null;
        }
    }

    /**
     * Loads a deck from a data input stream.
     * 
     * @param in
     *     - The data input stream.
     * @param deck
     *     - The card deck to write to.
     * @throws IOException
     *     If an error occurs while reading from the data stream.
     */
    private static void loadCards(DataInput in, Deck deck) throws IOException
    {
        var fileVersion = in.readInt();

        switch (fileVersion)
        {
            case 1:
                loadCardsFileVersion1(in, deck);
                break;

            default:
                throw new IllegalStateException("Unknown file version!");
        }
    }

    /**
     * Loads a deck from a data input stream using the encoding for file version 1.
     * 
     * @param in
     *     - The data input stream to read from.
     * @param deck
     *     - The card deck to write to.
     * @throws IOException
     *     If an error occurs while reading from the data stream.
     */
    private static void loadCardsFileVersion1(DataInput in, Deck deck) throws IOException
    {
        var cardCount = in.readInt();
        for (int i = 0; i < cardCount; i++)
            loadCard(in, deck);
    }

    /**
     * Loads a card from a data input stream.
     * 
     * @param in
     *     - The data input stream.
     * @param deck
     *     - The card deck to add the card to.
     * @return The card.
     * @throws IOException
     *     If an error occurs while reading from the data stream.
     */
    public static Card loadCard(DataInput in, Deck deck) throws IOException
    {
        var id = IOUtils.readUUID(in);
        var card = new Card(id, deck);
        deck.addCard(card);

        readCardInfo(in, card);

        return card;
    }

    /**
     * Creates all the card information from the data input stream and writes it to
     * the card.
     * 
     * @param in
     *     - The data input stream.
     * @param card
     *     - The card to write to.
     * @throws IOException
     *     If an error occurs while reading from the data stream.
     */
    private static void readCardInfo(DataInput in, Card card) throws IOException
    {
        var keyCount = in.readInt();
        for (int j = 0; j < keyCount; j++)
        {
            var key = IOUtils.readString(in);
            var bytes = new byte[in.readInt()];
            in.readFully(bytes);

            card.setInfoNoSave(key, bytes);
        }
    }

    /**
     * Saves a card deck to file, creating the file as needed.
     * 
     * @param deck
     *     - The card deck to save.
     */
    public static void saveDeck(Deck deck)
    {
        var databaseFolder = new File(CARD_FOLDER);
        databaseFolder.mkdir();

        var fileName = deck.getID()
                           .toString()
                + ".dat";

        var file = new File(databaseFolder, fileName);
        try (var writer = new DataOutputStream(new FileOutputStream(file)))
        {
            saveDeck(writer, deck);
        }
        catch (Exception e)
        {
            logger.error("Failed to save card deck! '{}'", file.getAbsolutePath(), e);
        }
    }

    /**
     * Writes the given card deck to a data output stream.
     * 
     * @param out
     *     - The data output to write to.
     * @param deck
     *     - The deck.
     * @throws IOException
     *     If an error occurs while writing to the output.
     */
    public static void saveDeck(DataOutput out, Deck deck) throws IOException
    {
        out.writeInt(FILE_VERSION);

        int cardCount = deck.getCardCount();
        out.writeInt(cardCount);

        for (int i = 0; i < cardCount; i++)
        {
            var card = deck.getCardAt(i);
            saveCard(out, card);
        }
    }

    /**
     * Writes a given card to the data output stream.
     * 
     * @param out
     *     - The data stream to write to.
     * @param card
     *     - The card to write.
     * @throws IOException
     *     If an error occurs while writing to the output.
     */
    public static void saveCard(DataOutput out, Card card) throws IOException
    {
        IOUtils.writeUUID(out, card.getID());
        saveCardInfo(out, card);
    }

    /**
     * Writes all the information from a card to the data output stream.
     * 
     * @param out
     *     - The data output stream to write to.
     * @param card
     *     - The card to read information from.
     * @throws IOException
     *     If an error occurs while writing to the output.
     */
    private static void saveCardInfo(DataOutput out, Card card) throws IOException
    {
        var keys = card.getKeys();
        out.writeInt(keys.size());

        for (var key : keys)
        {
            IOUtils.writeString(out, key);

            var bytes = card.getInfo(key);
            out.write(bytes.length);
            out.write(bytes);
        }
    }

    private CardDatabaseIO()
    {
        // Static class only. Prevent instancing.
    }
}
