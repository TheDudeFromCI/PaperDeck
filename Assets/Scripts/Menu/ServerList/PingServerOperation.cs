using System.Net.Sockets;
using System.Net;

namespace PaperDeck.Menu.ServerList
{
    public class PingServerOperation
    {
        private readonly string m_IP;
        private readonly int m_Port;

        public PingServerOperation(string ip, int port)
        {
            m_IP = ip;
            m_Port = port;
        }

        public void Connect()
        {
            var hostEntry = Dns.GetHostEntry(m_IP);
            foreach (IPAddress address in hostEntry.AddressList)
            {
                var ipe = new IPEndPoint(address, m_Port);
                var socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                var result = socket.BeginConnect(m_IP, m_Port, null, null);

                bool success = result.AsyncWaitHandle.WaitOne(5000, true);
                if (socket.Connected)
                {
                    // TODO Setup packet system for downloading server details.

                    socket.EndConnect(result);
                    return;
                }

                socket.Close();
            }

            throw new System.Exception("Failed to connect to server!");
        }
    }
}