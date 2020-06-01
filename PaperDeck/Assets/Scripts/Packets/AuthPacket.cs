using PaperDeck.Network;

namespace PaperDeck.Packets
{
    /// <summary>
    /// Received from the server when a player joins the server.
    /// </summary>
    public class AuthPacket : IPacket
    {
        public const long ID = 3782780678832128L;

        /// <summary>
        /// The name of this client's player.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// The ID of this client's player.
        /// </summary>
        public string PlayerID { get; set; }

        /// <inheritdoc cref="IPacket"/>
        public long PacketID => ID;

        /// <inheritdoc cref="IPacket"/>
        public void Handle()
        {
            // Never received from server.
        }
    }

    /// <summary>
    /// The packet IO handler for sending auth packets.
    /// </summary>
    public class AuthPacketIO : IPacketIO<AuthPacket>
    {
        /// <inheritdoc cref="IPacketIO{T}"/>
        public long PacketID => AuthPacket.ID;

        /// <inheritdoc cref="IPacketIO{T}"/>
        public IPacket ReadPacket(DataInput reader)
        {
            // Never sent from server
            throw new System.ApplicationException("Illegal packet received from server!");
        }

        /// <inheritdoc cref="IPacketIO{T}"/>
        public void WritePacket(DataOutput writer, AuthPacket packet)
        {
            writer.Write(packet.PlayerName);
            writer.Write(packet.PlayerID);
        }
    }
}
