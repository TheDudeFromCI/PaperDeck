namespace PaperDeck.ServerInfo
{
    /// <summary>
    /// A player currently on a server.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets the name of this player.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the ID of this player.
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Creates a new player object.
        /// </summary>
        /// <param name="name">The name of this player.</param>
        /// <param name="id">The ID value of this player.</param>
        public Player(string name, string id)
        {
            Name = name;
            ID = id;
        }
    }
}