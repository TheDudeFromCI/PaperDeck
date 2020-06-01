using System.Collections.Generic;

namespace PaperDeck.ServerInfo
{
    /// <summary>
    /// A list of players currently on the server.
    /// </summary>
    public class PlayerList
    {
        private readonly List<Player> m_Players = new List<Player>();

        /// <summary>
        /// Adds a player to this player list.
        /// </summary>
        /// <param name="player">The player to add.</param>
        public void AddPlayer(Player player)
        {
            m_Players.Add(player);
        }

        /// <summary>
        /// Removes a player from this player list.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        public void RemovePlayer(Player player)
        {
            m_Players.Remove(player);
        }

        /// <summary>
        /// Attempts to find the player in this list with the given player ID.
        /// </summary>
        /// <param name="id">The player ID value.</param>
        /// <returns>The player, or null if they could not be found.</returns>
        public Player FindPlayer(string id)
        {
            foreach (var player in m_Players)
                if (player.ID == id)
                    return player;

            return null;
        }
    }
}