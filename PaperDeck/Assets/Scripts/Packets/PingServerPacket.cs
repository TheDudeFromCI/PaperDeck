using PaperDeck.Network;

namespace PaperDeck.Packets
{
    public class PingServerPacket : IPacket
    {
        public const long ID = 1737784354144256L;

        public long PacketID => PingServerPacket.ID;

        public void Handle() {}
    }

    public class PingServerPacketIO : IPacketIO<PingServerPacket>
    {
        public long PacketID => PingServerPacket.ID;

        public IPacket ReadPacket(DataInput reader) => new PingServerPacket();

        public void WritePacket(DataOutput writer, PingServerPacket packet) {}
    }
}
