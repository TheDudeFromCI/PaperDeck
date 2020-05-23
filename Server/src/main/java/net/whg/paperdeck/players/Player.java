package net.whg.paperdeck.players;

import java.io.IOException;
import java.util.UUID;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import net.whg.we.net.IPacket;
import net.whg.we.net.server.IConnectedClient;

/**
 * A player currently connected to the server.
 */
public class Player
{
    private static final Logger logger = LoggerFactory.getLogger(Player.class);

    private final String name;
    private final UUID id;
    private final IConnectedClient client;

    /**
     * Create a new player with the given name and client connection.
     */
    Player(String name, UUID id, IConnectedClient client)
    {
        this.name = name;
        this.id = id;
        this.client = client;
    }

    /**
     * Kicks this player from the server.
     */
    public void kick()
    {
        try
        {
            client.kick();
        }
        catch (IOException e)
        {
            logger.error("Failed to kick player ''" + name + "'!", e);
        }
    }

    /**
     * Sends a packet to this player.
     * 
     * @param packet
     *     - The packet to send.
     */
    public void sendPacket(IPacket packet)
    {
        try
        {
            client.sendPacket(packet);
        }
        catch (IOException e)
        {
            logger.error("Failed send packet to player ''" + name + "'!", e);
            kick();
        }
    }

    /**
     * Gets the raw connection data for this player.
     * 
     * @return The raw connection.
     */
    IConnectedClient getRawConnection()
    {
        return client;
    }

    /**
     * Gets the ID of this player.
     * 
     * @return The player ID.
     */
    public UUID getID()
    {
        return id;
    }

    /**
     * Gets the name of this player.
     * 
     * @return The name.
     */
    public String getName()
    {
        return name;
    }
}
