namespace PaperDeck.Menu.ServerList
{
    /// <summary>
    /// A small summary of a server's connection status.
    /// </summary>
    public struct ServerStatus
    {
        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <value>The server name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the IP address of the server.
        /// </summary>
        /// <value>The server IP.</value>
        public string IP { get; set; }

        /// <summary>
        /// Gets the port number of the server.
        /// </summary>
        /// <value>The server port number.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets whether or not the server is online.
        /// </summary>
        /// <value>True if the server is online. False otherwise.</value>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets the message of the day for the server, if online.
        /// </summary>
        /// <value>The server message of the day.</value>
        public string MOTD { get; set; }

        /// <summary>
        /// Gets the maximum number of players allowed on the server, if online.
        /// </summary>
        /// <value>The maximum player count.</value>
        public int MaxPlayers { get; set; }

        /// <summary>
        /// Gets the number of players currently online.
        /// </summary>
        /// <value>The current number of players.</value>
        public int CurrentPlayers { get; set; }

        /// <summary>
        /// Gets the bytes representing the raw png file for the server icon.
        /// </summary>
        /// <value>The server icon as a PNG byte array.</value>
        public byte[] IconData { get; set; }
    }

}
