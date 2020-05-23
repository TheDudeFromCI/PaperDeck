package net.whg.paperdeck.players;

import java.util.ArrayList;
import java.util.List;
import net.whg.we.net.server.IConnectedClient;

/**
 * Represents a list of connected players on the server.
 */
public class PlayerList
{
    private final List<Player> onlinePlayers = new ArrayList<>();
    private final List<UnauthorizedClient> unauthorizedClients = new ArrayList<>();

    /**
     * Registers a client as a player and adds them to this player list.
     * 
     * @param client
     *     - The client to register.
     */
    void registerPlayer(UnauthorizedClient client)
    {
        if (!unauthorizedClients.contains(client))
            throw new IllegalArgumentException("Unauthorized client does not exist!");

        unauthorizedClients.remove(client);
        onlinePlayers.add(client.toPlayer());
    }

    /**
     * Removes a client from this player list based on the given connection data.
     * 
     * @param client
     *     - The raw client connection.
     */
    void removePlayer(IConnectedClient client)
    {
        var player = getPlayer(client);
        var unauthorized = getUnauthorizedClient(client);

        if (player != null)
            onlinePlayers.remove(player);
        else if (unauthorized != null)
            unauthorizedClients.remove(unauthorized);
    }

    /**
     * Gets the player with the given connection data.
     * 
     * @param client
     *     - The raw connection data.
     * @return The player, or null if there isn't one.
     */
    private Player getPlayer(IConnectedClient client)
    {
        for (var player : onlinePlayers)
            if (player.getRawConnection() == client)
                return player;

        return null;
    }

    /**
     * Gets the unauthorized client with the given connection data.
     * 
     * @param client
     *     - The raw connection data.
     * @return The unauthorized client, or null if there isn't one.
     */
    private UnauthorizedClient getUnauthorizedClient(IConnectedClient client)
    {
        for (var unauthorized : unauthorizedClients)
            if (unauthorized.getRawConnection() == client)
                return unauthorized;

        return null;
    }

    /**
     * Adds a new unauthorized client to this player list.
     * 
     * @param client
     *     - The client to add.
     */
    void addUnauthorizedClient(UnauthorizedClient client)
    {
        unauthorizedClients.add(client);
    }
}
