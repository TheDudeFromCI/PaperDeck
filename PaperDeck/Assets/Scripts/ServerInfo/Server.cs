namespace PaperDeck.ServerInfo
{
    /// <summary>
    /// An active connection to a server and all information about the server.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Gets the list of players on this server.
        /// </summary>
        public PlayerList PlayerList { get; }

        /// <summary>
        /// Creates a new server object.
        /// </summary>
        public Server()
        {
            PlayerList = new PlayerList();
        }
    }
}