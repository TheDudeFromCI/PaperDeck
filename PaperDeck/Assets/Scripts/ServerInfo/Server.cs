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
        /// Gets the display name of this client.
        /// </summary>
        public string ClientName { get; }

        /// <summary>
        /// Gets the ID of this client.
        /// </summary>
        public string ClientID { get; }

        /// <summary>
        /// Creates a new server object.
        /// </summary>
        public Server()
        {
            PlayerList = new PlayerList();

            // TODO Load this from an actual location
            ClientName = "TheDudeFromCI";
            ClientID = System.Guid.NewGuid().ToString();
        }
    }
}