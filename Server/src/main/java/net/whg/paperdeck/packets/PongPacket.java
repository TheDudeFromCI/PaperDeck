package net.whg.paperdeck.packets;

import java.io.DataOutput;
import java.io.IOException;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;

/**
 * Sent as a reply to ping packets from the client, containing MOTD data.
 */
public class PongPacket implements IBinaryPacket
{
    public static final long PACKET_ID = 8181459344949248L;

    private final String motd;
    private final int maxPlayers;
    private final int currentPlayers;
    private final byte[] iconData;

    /**
     * Creates a new pong packet.
     * 
     * @param motd
     *     - The MOTD text.
     * @param maxPlayers
     *     - The maximum number of allowed players on this server.
     * @param currentPlayers
     *     - The current number of online players.
     * @param iconData
     *     - Byte data from the server icon file.
     */
    public PongPacket(String motd, int maxPlayers, int currentPlayers, byte[] iconData)
    {
        this.motd = motd;
        this.maxPlayers = maxPlayers;
        this.currentPlayers = currentPlayers;
        this.iconData = iconData;
    }

    @Override
    public IPacketSender getSender()
    {
        // Packet is never received from client.
        return null;
    }

    @Override
    public void handle()
    {
        // Packet is never received from client.
    }

    @Override
    public long getPacketID()
    {
        return PACKET_ID;
    }

    @Override
    public void write(DataOutput out)
    {
        try
        {
            out.writeInt(motd.length());
            out.writeChars(motd);
            out.writeInt(maxPlayers);
            out.writeInt(currentPlayers);
            out.writeInt(iconData.length);
            out.write(iconData);
        }
        catch (IOException e)
        {
            // TODO Remove this in WraithEngine Build29
            e.printStackTrace();
        }
    }
}
