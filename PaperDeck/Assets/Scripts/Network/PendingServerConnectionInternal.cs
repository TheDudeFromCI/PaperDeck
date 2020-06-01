using System.Net.Sockets;
using UnityEngine;

namespace PaperDeck.Network
{
    /// <summary>
    /// Represents a server connection which is still trying to connect. When a connection
    /// is established, the ServerConnection behaviour is added to the active game object.
    /// </summary>
    public class PendingServerConnection : MonoBehaviour
    {
        /// <summary>
        /// Creates a new game object representing the connection and adds this behaviour
        /// to it.
        /// </summary>
        /// <param name="ip">The IP of the server to connect to.</param>
        /// <param name="port">The port of the server to connect to.</param>
        /// <returns>The pending server connection behaviour instance.</returns>
        public static PendingServerConnection Connect(string ip, int port)
        {
            var gameObject = new GameObject($"ServerConnection '{ip}:{port}'");
            var behaviour = gameObject.AddComponent<PendingServerConnection>();
            behaviour.m_IP = ip;
            behaviour.m_Port = port;

            return behaviour;
        }

        /// <summary>
        /// Gets the server connection this behaviour created if the connection was successful.
        /// </summary>
        /// <value>The server connection.</value>
        public ServerConnection Connection { get; private set; }

        /// <summary>
        /// Gets the current connection status for this behaviour.
        /// </summary>
        /// <value>The connection status.</value>
        public ConnectionStatus Status { get; private set; } = ConnectionStatus.Connecting;

        private string m_IP;
        private int m_Port;

        /// <summary>
        /// Called when the behaviour is loaded to start to connection process.
        /// </summary>
        protected void Start()
        {
            try
            {
                var tcp = new TcpClient(m_IP, m_Port);

                // TODO Make async connection work again.
                // With ConnectAsync(), certain addresses (I.e. "localhost") will always throw errors.
                // await tcp.ConnectAsync();

                var conn = new Connection(tcp);
                Connection = ServerConnection.CreateBehaviour(gameObject, conn);
                Status = ConnectionStatus.Connected;
            }
            catch (System.Exception)
            {
                Status = ConnectionStatus.FailedToConnect;
            }
        }
    }

    /// <summary>
    /// A list of connection status states for the PendingServerConnection behaviour.
    /// </summary>
    public enum ConnectionStatus
    {
        Connecting,
        Connected,
        FailedToConnect,
    }
}