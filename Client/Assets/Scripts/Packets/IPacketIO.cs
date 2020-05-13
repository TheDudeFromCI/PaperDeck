using PaperDeck.Network;

namespace PaperDeck.Packets
{
    public interface IPacketIO
    {
        long PacketID { get; }
        IPacket ReadPacket(DataInput reader);
    }

    public interface IPacketIO<T> : IPacketIO
    where T : IPacket
    {
        void WritePacket(DataOutput writer, T packet);
    }
}
