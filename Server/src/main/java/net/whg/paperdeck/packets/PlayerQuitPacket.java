package net.whg.paperdeck.packets;

import java.io.DataOutput;
import java.io.IOException;
import java.util.UUID;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;

/**
 * Sent from the sender to the client when a player quits the server.
 */
public class PlayerQuitPacket implements IBinaryPacket
{
    private static final long PACKET_ID = 5699809778335744L;

    private final String playerID;

    /**
     * Creates a new player quit packet.
     * 
     * @param playerID
     *     - The ID of the player who quit.
     */
    public PlayerQuitPacket(UUID playerID)
    {
        this.playerID = playerID.toString();
    }

    @Override
    public IPacketSender getSender()
    {
        return null;
    }

    @Override
    public void handle()
    {
        // Packet is never received from the client.
    }

    @Override
    public long getPacketID()
    {
        return PACKET_ID;
    }

    @Override
    public void write(DataOutput writer) throws IOException
    {
        writer.writeInt(playerID.length());
        writer.writeChars(playerID);
    }
}
