package net.whg.paperdeck.players;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import net.whg.paperdeck.IOUtils;
import net.whg.we.net.IPacketSender;
import net.whg.we.net.packets.IBinaryPacket;
import net.whg.we.net.packets.IPacketInitializer;
import net.whg.we.net.server.IConnectedClient;

/**
 * When received, authentications a player and adds them to the player list.
 */
public class AuthPacket implements IBinaryPacket
{
    private static final Logger logger = LoggerFactory.getLogger(AuthPacket.class);
    private static final long PACKET_ID = 3782780678832128L;

    /**
     * Initializes received ping packets.
     */
    public static class Initializer implements IPacketInitializer<AuthPacket>
    {
        private final PlayerList playerList;

        public Initializer(PlayerList playerList)
        {
            this.playerList = playerList;
        }

        @Override
        public long getPacketID()
        {
            return PACKET_ID;
        }

        @Override
        public AuthPacket loadPacket(DataInput input, IPacketSender sender) throws IOException
        {
            var playerName = IOUtils.readString(input);
            var playerID = IOUtils.readString(input);
            return new AuthPacket(sender, playerName, playerID, playerList);
        }
    }

    private final IPacketSender sender;
    private final String playerName;
    private final String playerID;
    private final PlayerList playerList;

    /**
     * Creates a new ping packet.
     * 
     * @param sender
     *     - The client who sent this packet.
     * @param playerName
     *     - The name of the player.
     * @param playerID
     *     - The ID of the player.
     * @param playerList
     *     - The list of players to add the client to.
     */
    AuthPacket(IPacketSender sender, String playerName, String playerID, PlayerList playerList)
    {
        this.sender = sender;
        this.playerName = playerName;
        this.playerID = playerID;
        this.playerList = playerList;
    }

    @Override
    public IPacketSender getSender()
    {
        return sender;
    }

    @Override
    public void handle()
    {
        var conn = (IConnectedClient) sender;
        var client = playerList.getUnauthorizedClient(conn);

        client.setName(playerName);
        client.setID(playerID);

        playerList.registerPlayer(client);

        logger.info("Authenticated player '{}' with id {}.", playerName, playerID);
    }

    @Override
    public long getPacketID()
    {
        return PACKET_ID;
    }

    @Override
    public void write(DataOutput out)
    {
        // Auth packets are not sent from server.
    }
}
