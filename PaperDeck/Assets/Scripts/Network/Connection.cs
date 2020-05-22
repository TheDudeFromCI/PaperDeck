using System.Net.Sockets;

namespace PaperDeck.Network
{
    /// <summary>
    /// A simple container for a TCP Socket
    /// </summary>
    public class Connection
    {
        private readonly TcpClient m_TCP;

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
        /// Creates a new socket connection to the given host and port number.
        /// </summary>
        /// <param name="ip">The host IP address.</param>
        /// <param name="port">The host port number.</param>
        public Connection(string ip, int port)
        {
            m_TCP = new TcpClient(ip, port);
            var stream = m_TCP.GetStream();

            Reader = new DataInput(stream);
            Writer = new DataOutput(stream);
        }

        /// <summary>
        /// Closes this connection.
        /// </summary>
        public void Close()
        {
            if (IsOpen)
                m_TCP.Close();
        }
    }
}
