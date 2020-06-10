package net.whg.paperdeck.packets;

import java.io.DataOutput;
import java.io.IOException;
import java.util.UUID;
import net.whg.paperdeck.IOUtils;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;

/**
 * Sent from the sender to the client when a player joins the server.
 */
public class PlayerJoinedPacket implements IBinaryPacket
{
    private static final long PACKET_ID = 3172928403275776L;

    private final String playerName;
    private final String playerID;

    /**
     * Creates a new player joined packet.
     * 
     * @param playerName
     *     - The name of the player who joined.
     * @param playerID
     *     - The ID of the player who joined.
     */
    public PlayerJoinedPacket(String playerName, UUID playerID)
    {
        this.playerName = playerName;
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
        IOUtils.writeString(writer, playerName);
        IOUtils.writeString(writer, playerID);
    }
}
