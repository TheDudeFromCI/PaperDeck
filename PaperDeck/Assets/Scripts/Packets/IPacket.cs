namespace PaperDeck.Packets
{
    public interface IPacket
    {
        long PacketID { get; }

        void Handle();
    }
}