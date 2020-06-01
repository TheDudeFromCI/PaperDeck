namespace PaperDeck.Packets
{
    /// <summary>
    /// A collection of data sent to or received from the server.
    /// </summary>
    public interface IPacket
    {
        /// <summary>
        /// The unique ID value of this packet, matching the ID on the server.
        /// </summary>
        long PacketID { get; }

        /// <summary>
        /// Called on the main thread when the packet is received.
        /// </summary>
        void Handle();
    }
}