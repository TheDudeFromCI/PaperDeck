package net.whg.paperdeck.players;

import net.whg.we.net.server.IClientHandler;
import net.whg.we.net.server.IConnectedClient;

/**
 * Handles moving incoming and outgoing clients with the player list.
 */
public class ClientReceiver implements IClientHandler
{
    private final PlayerList playerList;

    /**
     * Creates a new client receiver for the given player list.
     */
    public ClientReceiver(PlayerList playerList)
    {
        this.playerList = playerList;
    }

    @Override
    public void onClientConnected(IConnectedClient client)
    {
        var unauthorized = new UnauthorizedClient(client);
        playerList.addUnauthorizedClient(unauthorized);
    }

    @Override
    public void onClientDisconnected(IConnectedClient client)
    {
        playerList.removePlayer(client);
    }

    @Override
    public void readData(IConnectedClient client)
    {
        // Do nothing.
    }
}
