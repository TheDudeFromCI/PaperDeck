package net.whg.paperdeck;

import java.io.IOException;
import org.apache.commons.cli.CommandLine;
import org.apache.commons.cli.DefaultParser;
import org.apache.commons.cli.HelpFormatter;
import org.apache.commons.cli.Option;
import org.apache.commons.cli.Options;
import org.apache.commons.cli.ParseException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * The main PaperDeck server entry point.
 */
public class Main
{
    private static final Logger logger = LoggerFactory.getLogger(Main.class);

    /**
     * Starts the program.
     * 
     * @param args
     *     - The arguments for running this program.
     */
    public static void main(String[] args)
    {
        var cmd = parseOptions(args);

        if (cmd.hasOption("h"))
        {
            showHelpText();
            return;
        }

        int port = getPort(cmd);
        startServer(port);
    }

    /**
     * Gets the port to open the server on, based on the provided input arguments.
     * Defaults to 23404 if port is not specified.
     * 
     * @param cmd
     *     - The command line handler.
     * @return The target port number.
     */
    private static int getPort(CommandLine cmd)
    {
        if (cmd.hasOption("p"))
            return Integer.valueOf(cmd.getOptionValue("p"));

        return 23404;
    }

    /**
     * Starts the game server on the given port.
     * 
     * @param port
     *     - The port to start the server on.
     */
    private static void startServer(int port)
    {
        GameServer server;

        try
        {
            server = GameServer.initialize(port);
        }
        catch (IOException e)
        {
            logger.error("Failed to start server!", e);
            return;
        }

        server.loop();
    }

    /**
     * Parses the provided input arguments.
     * 
     * @return The command line handler.
     */
    private static CommandLine parseOptions(String[] args)
    {
        try
        {
            var options = getOptions();
            return new DefaultParser().parse(options, args);
        }
        catch (ParseException e)
        {
            logger.error("Failed to parse input arguments!", e);
            System.exit(1);
            return null;
        }
    }

    /**
     * Gets the input options list for this program.
     * 
     * @return The input options.
     */
    private static Options getOptions()
    {
        var options = new Options();
        options.addOption(getHelpOption());
        options.addOption(getPortOption());

        return options;
    }

    /**
     * Gets the help option specification.
     * 
     * @return The help option.
     */
    private static Option getHelpOption()
    {
        return Option.builder("h")
                     .longOpt("help")
                     .desc("Show this help text.")
                     .hasArg(false)
                     .required(false)
                     .build();
    }

    /**
     * Gets the port option specification.
     * 
     * @return The port option.
     */
    private static Option getPortOption()
    {
        return Option.builder("p")
                     .longOpt("port")
                     .desc("Sets the port number to start the server on. Defaults to 23404.")
                     .hasArg(true)
                     .numberOfArgs(1)
                     .optionalArg(false)
                     .required(false)
                     .type(Integer.class)
                     .valueSeparator(' ')
                     .build();
    }

    /**
     * Shows the help text for this program.
     */
    private static void showHelpText()
    {
        HelpFormatter formatter = new HelpFormatter();
        formatter.printHelp("paperdeck [options]", getOptions());
    }
}
