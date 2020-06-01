using PaperDeck.Network;

namespace PaperDeck.Packets
{
    /// <summary>
    /// A packet sent to the server to request MOTD information.
    /// </summary>
    public class PingServerPacket : IPacket
    {
        public const long ID = 1737784354144256L;

        /// <inheritdoc cref="IPacket"/>
        public long PacketID => ID;

        /// <inheritdoc cref="IPacket"/>
        public void Handle()
        {
            // Never received from server, so nothing to do.
        }
    }

    /// <summary>
    /// The packet IO handler for ping packets.
    /// </summary>
    public class PingServerPacketIO : IPacketIO<PingServerPacket>
    {
        /// <inheritdoc cref="IPacketI{T}"/>
        public long PacketID => PingServerPacket.ID;

        /// <inheritdoc cref="IPacketI{T}"/>
        public IPacket ReadPacket(DataInput reader) => new PingServerPacket();

        /// <inheritdoc cref="IPacketI{T}"/>
        public void WritePacket(DataOutput writer, PingServerPacket packet) { }
    }
}
