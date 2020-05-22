using System.Net.Sockets;

namespace PaperDeck.Network
{
    /// <summary>
    /// A simple container for a TCP Socket
    /// </summary>
    public class Connection
    {
        private readonly TcpClient m_TCP;
        private readonly NetworkStream m_Stream;

        /// <summary>
        /// Gets the data reader for the input stream of this socket.
        /// </summary>
        /// <value>The input data stream.</value>
        public DataInput Reader { get; }

        /// <summary>
        /// Gets the data writer for the output stream of this socket.
        /// </summary>
        /// <value>The output data stream.</value>
        public DataOutput Writer { get; }

        /// <summary>
        /// Checks if the connection is still open.
        /// </summary>
        public bool IsOpen => m_TCP.Connected;

        /// <summary>
        /// Creates a new container for the given tcp client.
        /// </summary>
        /// <param name="tcp">The TCP Client.</param>
        public Connection(TcpClient tcp)
        {
            m_TCP = tcp;
            m_Stream = m_TCP.GetStream();

            Reader = new DataInput(m_Stream);
            Writer = new DataOutput(m_Stream);
        }

        /// <summary>
        /// Creates a new container for the given ip address.
        /// </summary>
        /// <param name="ip">The IP to connect to.</param>
        /// <param name="port">The port to connect to.</param>
        public Connection(string ip, int port)
            : this(new TcpClient(ip, port)) { }

        /// <summary>
        /// Closes this connection.
        /// </summary>
        public void Close()
        {
            m_Stream.Close();
            m_TCP.Close();
        }
    }
}
