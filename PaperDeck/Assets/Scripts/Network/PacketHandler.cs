using System.Collections.Generic;

using PaperDeck.Packets;

namespace PaperDeck.Network
{
    public class PacketHandler
    {
        public static PacketHandler CreateDefaultHandler()
        {
            var handler = new PacketHandler();

            handler.Register(new PingServerPacketIO());
            handler.Register(new PongServerPacketIO());

            return handler;
        }

        private readonly List<IPacketIO> list = new List<IPacketIO>();

        public void Register(IPacketIO io) => list.Add(io);

        public IPacket ReadPacket(DataInput reader)
        {
            var id = reader.ReadInt64();
            var io = GetInitializer(id);

            return io.ReadPacket(reader);
        }

        public void WritePacket<T>(DataOutput writer, T packet)
        where T : IPacket
        {
            var id = packet.PacketID;
            var io = (IPacketIO<T>) GetInitializer(id);

            writer.Write(id);
            io.WritePacket(writer, packet);
        }

        private IPacketIO GetInitializer(long id)
        {
            foreach (var io in list)
                if (io.PacketID == id)
                    return io;

            throw new System.Exception("Unknown packet!");
        }
    }
}
