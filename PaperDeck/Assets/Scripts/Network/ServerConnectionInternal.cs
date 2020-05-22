using System.Collections.Concurrent;
using System.Threading.Tasks;
using PaperDeck.Packets;
using UnityEngine;

namespace PaperDeck.Network
{
    /// <summary>
    /// Represents a long-lived connection to a server.
    /// </summary>
    public class ServerConnection : MonoBehaviour
    {
        /// <summary>
        /// Creates a new server connection behaviour and adds it to the given game object.
        /// </summary>
        /// <param name="gameObject">The game object to add to.</param>
        /// <param name="ip">The IP address to connect to.</param>
        /// <param name="port">The port number to connect to.</param>
        /// <returns>The newly created ServerConnection behaviour.</returns>
        public static ServerConnection CreateBehaviour(GameObject gameObject, string ip, int port)
        {
            var connection = new Connection(ip, port);

            var b = gameObject.AddComponent<ServerConnection>();
            b.m_Connection = connection;
            b.m_PacketHandler = PacketHandler.CreateDefaultHandler();

            Task.Run(b.ReadPackets);

            return b;
        }

        /// <summary>
        /// Gets the first active instance of this server connection within the scene.
        /// </summary>
        /// <returns>The first server connection instance, or null if there isn't one.</returns>
        public static ServerConnection Instance => FindObjectOfType<ServerConnection>();

        private readonly ConcurrentQueue<IPacket> m_ReceivedPackets = new ConcurrentQueue<IPacket>();
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
            m_Connection.Close();
        }

        /// <summary>
        /// Sends a packet to the server. Does nothing if connection is closed.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        public void SendPacket(IPacket packet)
        {
            if (m_Connection.IsOpen)
                m_PacketHandler.WritePacket(m_Connection.Writer, packet);
        }

        /// <summary>
        /// Loops until the connection is closed, reading all incoming packets.
        /// </summary>
        private void ReadPackets()
        {
            while (m_Connection.IsOpen)
            {
                var packet = m_PacketHandler.ReadPacket(m_Connection.Reader);
                m_ReceivedPackets.Enqueue(packet);
            }
        }

        /// <summary>
        /// Called each physics frame to handle all currently queued packets.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            while (true)
            {
                if (!m_ReceivedPackets.TryDequeue(out IPacket packet))
                    break;

                packet.Handle();
            }
        }
    }
}