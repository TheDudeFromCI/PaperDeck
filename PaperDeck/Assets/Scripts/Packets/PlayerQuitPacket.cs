using PaperDeck.Network;
using PaperDeck.ServerInfo;

namespace PaperDeck.Packets
{
    /// <summary>
    /// Received from the server when a player joins the server.
    /// </summary>
    public class PlayerQuitPacket : IPacket
    {
        public const long ID = 5699809778335744L;

        private readonly Server m_Server;
        private readonly string m_PlayerID;

        /// <summary>
        /// Creates a new player joined packet.
        /// </summary>
        /// <param name="server">The server to remove the player from.</param>
        /// <param name="playerID">The ID of the player.</param>
        public PlayerQuitPacket(Server server, string playerID)
        {
            m_Server = server;
            m_PlayerID = playerID;
        }

        /// <inheritdoc cref="IPacket"/>
        public long PacketID => ID;

        /// <inheritdoc cref="IPacket"/>
        public void Handle()
        {
            var player = m_Server.PlayerList.FindPlayer(m_PlayerID);
            m_Server.PlayerList.RemovePlayer(player);
        }
    }

    /// <summary>
    /// The packet IO handler for player joined packets.
    /// </summary>
    public class PlayerQuitPacketIO : IPacketIO<PlayerQuitPacket>
    {
        private readonly Server m_Server;

        /// <summary>
        /// Creates a new player joined packet.
        /// </summary>
        /// <param name="server">The server to add new players to.</param>
        public PlayerQuitPacketIO(Server server)
        {
            m_Server = server;
        }

        /// <inheritdoc cref="IPacketIO{T}"/>
        public long PacketID => PlayerQuitPacket.ID;

        /// <inheritdoc cref="IPacketIO{T}"/>
        public IPacket ReadPacket(DataInput reader)
        {
            var playerID = reader.ReadString();
            return new PlayerQuitPacket(m_Server, playerID);
        }

        /// <inheritdoc cref="IPacketIO{T}"/>
        public void WritePacket(DataOutput writer, PlayerQuitPacket packet)
        {
            // Never sent to server.
        }
    }
}
