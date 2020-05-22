using System.Collections.Concurrent;
using System.Threading.Tasks;
using PaperDeck.Packets;
using UnityEngine;

namespace PaperDeck.Network
{
    /// <summary>
    /// Represents a high level connection to a server.
    /// </summary>
    public class ServerConnection : MonoBehaviour
    {
        /// <summary>
        /// Creates a new server connection behaviour and adds it to the given game object.
        /// </summary>
        /// <param name="connection">The active connection to wrap around.</param>
        /// <returns>The newly created ServerConnection behaviour.</returns>
        internal static ServerConnection CreateBehaviour(GameObject gameObject, Connection connection)
        {
            var behaviour = gameObject.AddComponent<ServerConnection>();
            behaviour.m_Connection = connection;
            behaviour.m_PacketHandler = PacketHandler.CreateDefaultHandler();

            Task.Run(behaviour.ReadPackets);

            return behaviour;
        }

        /// <summary>
        /// Gets the first active instance of this server connection within the scene.
        /// </summary>
        /// <returns>The first server connection instance, or null if there isn't one.</returns>
        public static ServerConnection Instance => FindObjectOfType<ServerConnection>();

        private readonly ConcurrentQueue<IPacket> m_ReceivedPackets = new ConcurrentQueue<IPacket>();
        private volatile bool m_ConnectionActive = true;
        private Connection m_Connection;
        private PacketHandler m_PacketHandler;

        /// <summary>
        /// Gets whether or not the server connection is still active.
        /// </summary>
        public bool IsConnected => m_Connection.IsOpen;

        /// <summary>
        /// Logs out from the server.
        /// </summary>
        public void Logout()
        {
            m_ConnectionActive = false;
            m_Connection.Close();

            Destroy(gameObject);

            NotifyServerClosed();
        }

        /// <summary>
        /// Sends a packet to the server. Does nothing if connection is closed.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        public void SendPacket(IPacket packet)
        {
            try
            {
                m_PacketHandler.WritePacket(m_Connection.Writer, packet);
            }
            catch (System.ObjectDisposedException)
            {
                // Server closed.
                Logout();
            }
        }

        /// <summary>
        /// Loops until the connection is closed, reading all incoming packets.
        /// </summary>
        private void ReadPackets()
        {
            try
            {
                while (m_ConnectionActive)
                {
                    var packet = m_PacketHandler.ReadPacket(m_Connection.Reader);
                    m_ReceivedPackets.Enqueue(packet);
                }
            }
            catch (System.ObjectDisposedException)
            {
                // Server closed.
                Debug.Log("NetworkStream closed. Closing client.");
                m_ConnectionActive = false;
            }
        }

        /// <summary>
        /// Called each physics frame to handle all currently queued packets.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            while (m_ConnectionActive)
            {
                if (!m_ReceivedPackets.TryDequeue(out IPacket packet))
                    break;

                packet.Handle();
            }

            if (!m_ConnectionActive)
                Logout();
        }

        /// <summary>
        /// Shows a message to the user that the connection to the server was closed.
        /// </summary>
        private void NotifyServerClosed()
        {
            // TODO Show the server disconnected screen.
        }
    }
}