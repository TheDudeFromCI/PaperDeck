using PaperDeck.Network;

namespace PaperDeck.Packets
{
    public class PongServerPacket : IPacket
    {
        public const long ID = 8181459344949248L;

        public long PacketID => PongServerPacket.ID;

        public string MOTD { get; set; }
        public int MaxPlayers { get; set; }
        public int CurrentPlayers { get; set; }
        public byte[] IconData { get; set; }

        public void Handle() {}
    }

    public class PongServerPacketIO : IPacketIO<PongServerPacket>
    {
        public long PacketID => PongServerPacket.ID;

        public IPacket ReadPacket(DataInput reader)
        {
            var packet = new PongServerPacket();

            packet.MOTD = reader.ReadString();

            packet.MaxPlayers = reader.ReadInt32();
            packet.CurrentPlayers = reader.ReadInt32();

            var iconLength = reader.ReadInt32();
            packet.IconData = reader.ReadBytes(iconLength);

            return packet;
        }

        public void WritePacket(DataOutput writer, PongServerPacket packet) {}
    }
}
