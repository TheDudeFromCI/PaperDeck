package net.whg.paperdeck;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;
import java.util.UUID;

public final class IOUtils
{
    /**
     * Reads a string from a data input.
     * 
     * @param in
     *     - The data input stream.
     * @return The string.
     * @throws IOException
     *     If an error occurs while reading from the data input stream.
     */
    public static String readString(DataInput in) throws IOException
    {
        int length = in.readInt();
        char[] chars = new char[length];

        for (int i = 0; i < chars.length; i++)
            chars[i] = in.readChar();

        return new String(chars);
    }

    /**
     * Reads a UUID from the data input stream.
     * 
     * @param in
     *     - The data input stream.
     * @return The UUID.
     * @throws IOException
     *     If an error occurs while reading from the data input stream.
     */
    public static UUID readUUID(DataInput in) throws IOException
    {
        var idHigh = in.readLong();
        var idLow = in.readLong();
        return new UUID(idHigh, idLow);
    }

    /**
     * Writes a string to the data output stream.
     * 
     * @param out
     *     - The data output stream.
     * @param str
     *     - The string to write.
     * @throws IOException
     *     If an error occurs while writing to the data output stream.
     */
    public static void writeString(DataOutput out, String str) throws IOException
    {
        out.writeInt(str.length());
        out.writeChars(str);
    }

    /**
     * Writes a UUID to the data output stream.
     * 
     * @param out
     *     - The data output stream.
     * @param id
     *     - The UUID to write.
     * @throws IOException
     *     If an error occurs while writing to the data output stream.
     */
    public static void writeUUID(DataOutput out, UUID id) throws IOException
    {
        var idHigh = id.getMostSignificantBits();
        var idLow = id.getLeastSignificantBits();
        out.writeLong(idHigh);
        out.writeLong(idLow);
    }

    private IOUtils()
    {}
}
