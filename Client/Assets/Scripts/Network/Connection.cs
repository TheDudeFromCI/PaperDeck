using System.Net.Sockets;

namespace PaperDeck.Network
{
    public class Connection
    {
        private readonly TcpClient m_TCP;

        public DataInput Reader { get; }
        public DataOutput Writer { get; }

        public Connection(string ip, int port)
        {
            m_TCP = new TcpClient(ip, port);
            var stream = m_TCP.GetStream();

            Reader = new DataInput(stream);
            Writer = new DataOutput(stream);
        }

        public void Close()
        {
            m_TCP.Close();
        }
    }
}
