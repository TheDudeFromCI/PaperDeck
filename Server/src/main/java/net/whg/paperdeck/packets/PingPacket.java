package net.whg.paperdeck.packets;

import java.io.DataOutput;
import java.io.IOException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;
import net.whg.we.net.server.IConnectedClient;

/**
 * When received, replies to the client with MOTD data, and then closes the
 * connection.
 */
public class PingPacket implements IBinaryPacket
{
    private static final Logger logger = LoggerFactory.getLogger(PingPacket.class);

    public static final long PACKET_ID = 1737784354144256L;

    private final IPacketSender sender;

    /**
     * Creates a new ping packet.
     * 
     * @param sender
     *     - The client who sent this packet.
     */
    PingPacket(IPacketSender sender)
    {
        this.sender = sender;
    }

    @Override
    public IPacketSender getSender()
    {
        return sender;
    }

    @Override
    public void handle()
    {
        // TODO load packet data

        var motd = "Welcome to my PaperDeck server!";
        var maxPlayers = 30;
        var currentPlayers = 2;
        var iconData = new byte[0];

        var packet = new PongPacket(motd, maxPlayers, currentPlayers, iconData);

        try
        {
            var client = (IConnectedClient) sender;
            client.sendPacket(packet);
            client.kick();
        }
        catch (IOException e)
        {
            logger.error("Failed to send ping response to client!", e);
        }
    }

    @Override
    public long getPacketID()
    {
        return PACKET_ID;
    }

    @Override
    public void write(DataOutput out)
    {
        // Ping packets are not sent from server.
    }
}
