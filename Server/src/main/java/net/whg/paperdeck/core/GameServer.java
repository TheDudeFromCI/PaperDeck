package net.whg.paperdeck.core;

import java.io.IOException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import net.whg.we.external.ServerSocketAPI;
import net.whg.we.external.TimeSupplierApi;
import net.whg.we.main.SceneGameLoop;
import net.whg.we.main.Timer;
import net.whg.we.main.TimerAction;
import net.whg.we.net.server.IServer;
import net.whg.we.net.server.SimpleServer;
import net.whg.we.net.packets.PacketDataHandler;
import net.whg.we.net.packets.PacketFactory;
import net.whg.paperdeck.net.ClientReceiver;
import net.whg.paperdeck.net.HandleServerPacketsAction;
import net.whg.paperdeck.packets.PingPacketInitializer;
import net.whg.paperdeck.util.FramerateLimiterAction;

public class GameServer
{
    private static final Logger logger = LoggerFactory.getLogger(GameServer.class);

    public static GameServer initialize(int port) throws IOException
    {
        logger.info("Starting PaperDeck server on port {}", port);

        var gameLoop = new SceneGameLoop();

        var timer = new Timer(new TimeSupplierApi());
        gameLoop.addAction(new TimerAction(timer));
        gameLoop.addAction(new FramerateLimiterAction(timer, 10f));

        var packetFactory = new PacketFactory();
        packetFactory.register(new PingPacketInitializer());

        var server = new SimpleServer();
        server.setClientHandler(new ClientReceiver());
        server.setDataHandler(new PacketDataHandler(packetFactory));
        gameLoop.addAction(new HandleServerPacketsAction(server));

        server.start(new ServerSocketAPI(), port);
        return new GameServer(server, gameLoop, timer);
    }

    private final IServer server;
    private final SceneGameLoop gameLoop;
    private final Timer timer;

    /**
     * Creates a new game server object.
     * 
     * @param server
     *     - The server backing this game.
     * @param gameLoop
     *     - The main game loop.
     * @param timer
     *     - The timer managing the game loop.
     */
    private GameServer(IServer server, SceneGameLoop gameLoop, Timer timer)
    {
        this.server = server;
        this.gameLoop = gameLoop;
        this.timer = timer;
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
}