using System.Net;
using System.Net.Sockets;

using PaperDeck.Network;
using PaperDeck.Packets;

namespace PaperDeck.Menu.ServerList
{
    public class PingServerOperation
    {
        private readonly PacketHandler m_PacketHandler;

        public Connection Connection { get; }

        public PingServerOperation(string ip, int port)
        {
            Connection = new Connection(ip, port);
            m_PacketHandler = PacketHandler.CreateDefaultHandler();
        }

        public ServerMOTD SendPing()
        {
            var ping = new PingServerPacket();
            m_PacketHandler.WritePacket(Connection.Writer, ping);

            var pong = m_PacketHandler.ReadPacket(Connection.Reader) as PongServerPacket;
            if (pong == null)
                throw new System.Exception("Unexpected packet received!");

            var motd = new ServerMOTD
            {
                Message = pong.MOTD,
                MaxPlayers = pong.MaxPlayers,
                CurrentPlayers = pong.CurrentPlayers,
                IconData = pong.IconData,
            };

            return motd;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
