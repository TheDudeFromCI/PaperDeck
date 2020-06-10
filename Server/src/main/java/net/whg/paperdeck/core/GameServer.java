package net.whg.paperdeck.core;

import java.io.IOException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import net.whg.we.external.ServerSocketAPI;
import net.whg.we.external.TimeSupplierAPI;
import net.whg.we.main.SceneGameLoop;
import net.whg.we.main.Timer;
import net.whg.we.main.TimerAction;
import net.whg.we.net.server.IServer;
import net.whg.we.net.server.SimpleServer;
import net.whg.we.net.packets.PacketDataHandler;
import net.whg.we.net.packets.PacketFactory;
import net.whg.lib.actions.FramerateLimiterAction;
import net.whg.lib.actions.HandleServerPacketsAction;
import net.whg.paperdeck.players.PlayerList;
import net.whg.paperdeck.database.CardDatabase;
import net.whg.paperdeck.packets.PingPacket;
import net.whg.paperdeck.players.AuthPacket;
import net.whg.paperdeck.players.ClientReceiver;

public class GameServer
{
    private static final Logger logger = LoggerFactory.getLogger(GameServer.class);

    /**
     * Creates the game server object and starts the server thread.
     * 
     * @param port
     *     - The port to open the server on.
     * @return The game server.
     * @throws IOException
     *     If the server could not be started.
     */
    public static GameServer initialize(int port) throws IOException
    {
        logger.info("Starting PaperDeck server on port {}", port);

        var gameLoop = new SceneGameLoop();

        var timer = new Timer(new TimeSupplierAPI());
        gameLoop.addAction(new TimerAction(timer));
        gameLoop.addAction(new FramerateLimiterAction(timer, 10f));

        var server = new SimpleServer();
        var playerList = new PlayerList();

        var packetFactory = new PacketFactory();
        packetFactory.register(new PingPacket.Initializer());
        packetFactory.register(new AuthPacket.Initializer(playerList));

        server.setClientHandler(new ClientReceiver(playerList));
        server.setDataHandler(new PacketDataHandler(packetFactory));
        gameLoop.addAction(new HandleServerPacketsAction(server));

        var cardDatabase = new CardDatabase();

        server.start(new ServerSocketAPI(), port);
        return new GameServer(server, gameLoop, timer, playerList, cardDatabase);
    }

    private final PlayerList playerList;
    private final IServer server;
    private final SceneGameLoop gameLoop;
    private final Timer timer;
    private final CardDatabase cardDatabase;

    /**
     * Creates a new game server object.
     * 
     * @param server
     *     - The server backing this game.
     * @param gameLoop
     *     - The main game loop.
     * @param timer
     *     - The timer managing the game loop.
     * @param playerList
     *     - The list of players on this server.
     * @param cardDatabase
     *     - The card database.
     */
    private GameServer(IServer server, SceneGameLoop gameLoop, Timer timer, PlayerList playerList,
            CardDatabase cardDatabase)
    {
        this.server = server;
        this.gameLoop = gameLoop;
        this.timer = timer;
        this.playerList = playerList;
        this.cardDatabase = cardDatabase;
    }

    /**
     * Starts the game loop. This action blocks until the game loop is stopped.
     */
    public void loop()
    {
        timer.startTimer();
        gameLoop.loop();

        server.stop();
    }

    /**
     * Stops the server and game loop.
     */
    public void stopServer()
    {
        gameLoop.stop();
        server.stop();
    }

    /**
     * Gets the player list for this game server.
     * 
     * @return The player list.
     */
    public PlayerList getPlayerList()
    {
        return playerList;
    }

    /**
     * Gets the card database.
     * 
     * @return The card database.
     */
    public CardDatabase getCardDatabase()
    {
        return cardDatabase;
    }
}
