using PaperDeck.Network;
using PaperDeck.ServerInfo;

namespace PaperDeck.Packets
{
    /// <summary>
    /// Received from the server when a player joins the server.
    /// </summary>
    public class PlayerJoinedPacket : IPacket
    {
        public const long ID = 3172928403275776L;

        private readonly Server m_Server;
        private readonly string m_PlayerName;
        private readonly string m_PlayerID;

        /// <summary>
        /// Creates a new player joined packet.
        /// </summary>
        /// <param name="server">The server to add the player to.</param>
        /// <param name="playerName">The name of the player.</param>
        /// <param name="playerID">The ID of the player.</param>
        public PlayerJoinedPacket(Server server, string playerName, string playerID)
        {
            m_Server = server;
            m_PlayerName = playerName;
            m_PlayerID = playerID;
        }

        /// <inheritdoc cref="IPacket"/>
        public long PacketID => ID;

        /// <inheritdoc cref="IPacket"/>
        public void Handle()
        {
            var player = new Player(m_PlayerName, m_PlayerID);
            m_Server.PlayerList.AddPlayer(player);
        }
    }

    /// <summary>
    /// The packet IO handler for player joined packets.
    /// </summary>
    public class PlayerJoinedPacketIO : IPacketIO<PlayerJoinedPacket>
    {
        private readonly Server m_Server;

        /// <summary>
        /// Creates a new player joined packet.
        /// </summary>
        /// <param name="server">The server to add new players to.</param>
        public PlayerJoinedPacketIO(Server server)
        {
            m_Server = server;
        }

        /// <inheritdoc cref="IPacketIO{T}"/>
        public long PacketID => PlayerJoinedPacket.ID;

        /// <inheritdoc cref="IPacketIO{T}"/>
        public IPacket ReadPacket(DataInput reader)
        {
            var playerName = reader.ReadString();
            var playerID = reader.ReadString();
            return new PlayerJoinedPacket(m_Server, playerName, playerID);
        }

        /// <inheritdoc cref="IPacketIO{T}"/>
        public void WritePacket(DataOutput writer, PlayerJoinedPacket packet)
        {
            // Never sent to server.
        }
    }
}
