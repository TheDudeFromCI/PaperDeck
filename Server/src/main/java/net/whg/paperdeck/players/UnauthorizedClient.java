package net.whg.paperdeck.players;

import net.whg.we.net.server.IConnectedClient;

/**
 * A client which is connected to the server, but has not yet sent an
 * authorization packet.
 */
public class UnauthorizedClient
{
    private final IConnectedClient client;
    private String name;

    /**
     * Creates a new unauthorized client container for the given client.
     * 
     * @param client
     *     - The client.
     */
    public UnauthorizedClient(IConnectedClient client)
    {
        this.client = client;
    }

    /**
     * Sets the name of this client.
     * 
     * @param name
     *     - The name to assign.
     */
    void setName(String name)
    {
        this.name = name;
    }

    /**
     * Gets the name of this client.
     * 
     * @return The name, or null if it has not yet been received.
     */
    public String getName()
    {
        return name;
    }

    /**
     * Converts this client into a regular player.
     * 
     * @return The new player object.
     */
    Player toPlayer()
    {
        return new Player(name, client);
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
}
