using PaperDeck.Network;

namespace PaperDeck.Packets
{
    /// <summary>
    /// A generic-less implementation of the IPacketIO interface.
    /// <see cref="IPacketIO<T>"/>
    /// </summary>
    public interface IPacketIO
    {
        /// <summary>
        /// The unique ID value of the packet this handler targets, matching the ID on the server.
        /// </summary>
        long PacketID { get; }

        /// <summary>
        /// Parses the incoming data stream into a packet object.
        /// </summary>
        /// <param name="reader">The data stream.</param>
        /// <returns>The new packet object.</returns>
        IPacket ReadPacket(DataInput reader);
    }

    /// <summary>
    /// A handler for creating packet object when they are received from the server.
    /// </summary>
    public interface IPacketIO<T> : IPacketIO
        where T : IPacket
    {
        /// <summary>
        /// Writes a packet to the data stream.
        /// </summary>
        /// <param name="writer">The data stream to write to.</param>
        /// <param name="packet">The packet to write.</param>
        void WritePacket(DataOutput writer, T packet);
    }
}
