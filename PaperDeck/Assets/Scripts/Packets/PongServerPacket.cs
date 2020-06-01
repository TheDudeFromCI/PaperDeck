using PaperDeck.Network;

namespace PaperDeck.Packets
{
    /// <summary>
    /// Received from the server as a response to the ping packet.
    /// </summary>
    public class PongServerPacket : IPacket
    {
        public const long ID = 8181459344949248L;

        /// <inheritdoc cref="IPacket"/>
        public long PacketID => ID;

        /// <summary>
        /// Gets the Message of the Day received from the server.
        /// </summary>
        public string MOTD { get; set; }

        /// <summary>
        /// Gets the maximum number of players allowed on the server.
        /// </summary>
        public int MaxPlayers { get; set; }

        /// <summary>
        /// Gets the current number of players on the server.
        /// </summary>
        public int CurrentPlayers { get; set; }

        /// <summary>
        /// Gets the raw byte data, in PNG format, of the server icon.
        /// </summary>
        public byte[] IconData { get; set; }

        /// <inheritdoc cref="IPacket"/>
        public void Handle()
        {
            // Nothing to do.
        }
    }

    public class PongServerPacketIO : IPacketIO<PongServerPacket>
    {
        /// <inheritdoc cref="IPacketI{T}"/>
        public long PacketID => PongServerPacket.ID;

        /// <inheritdoc cref="IPacketI{T}"/>
        public IPacket ReadPacket(DataInput reader)
        {
            var packet = new PongServerPacket
            {
                MOTD = reader.ReadString(),
                MaxPlayers = reader.ReadInt32(),
                CurrentPlayers = reader.ReadInt32(),
                IconData = reader.ReadBytes(reader.ReadInt32())
            };

            return packet;
        }

        /// <inheritdoc cref="IPacketI{T}"/>
        public void WritePacket(DataOutput writer, PongServerPacket packet)
        {
            // Never sent to server.
        }
    }
}
