using System.Collections;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using PaperDeck.Menu.Util;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    /// <summary>
    /// A server within the server list.
    /// </summary>
    public class ServerListElement : SelectionElement
    {
        [Header("Settings")]
        [SerializeField] protected float m_ConnectionRotationSpeed = -360f;
        [SerializeField] protected Color m_SelectedColor;

        [Header("Game Objects")]
        [SerializeField] protected Image m_ConnectionImage;
        [SerializeField] protected GameObject m_PlayerList;
        [SerializeField] protected TextMeshProUGUI m_PlayerListValue;
        [SerializeField] protected TextMeshProUGUI m_NameTextBox;
        [SerializeField] protected TextMeshProUGUI m_IPTextBox;
        [SerializeField] protected Image m_BackgroundImage;

        [Header("Icons")]
        [SerializeField] protected Sprite m_ConnectedIcon;
        [SerializeField] protected Sprite m_ConnectingIcon;
        [SerializeField] protected Sprite m_FailedToConnectIcon;

        protected Color m_DefaultColor;
        protected string m_ServerName;
        protected string m_ServerIP;
        protected IEnumerator m_ServerConnectionCoroutine;

        /// <summary>
        /// Gets the name of this server.
        /// </summary>
        /// <value>The server name.</value>
        public string ServerName
        {
            get => m_ServerName;
            set
            {
                m_ServerName = value;
                m_NameTextBox.text = value;
            }
        }

        /// <summary>
        /// Gets the IP of this server.
        /// </summary>
        /// <value>The server IP.</value>
        public string ServerIP
        {
            get => m_ServerIP;
            set
            {
                m_ServerIP = value;
                m_IPTextBox.text = value;
            }
        }

        /// <summary>
        /// Called when the list element is initialized to ping the server.
        /// </summary>
        protected virtual void Start()
        {
            m_DefaultColor = m_BackgroundImage.color;
            TryReconnect();
        }

        /// <summary>
        /// Triggers a coroutine to handle the server connection in the background.
        /// </summary>
        /// <returns>The coroutine operation.</returns>
        private IEnumerator DoCheckServerStatus()
        {
            var connection = new ServerConnection
            {
                Name = m_ServerName,
                IP = m_ServerIP,
            };
            var pingServerTask = Task.Run(() => ConnectToServer(connection));

            var rect = m_ConnectionImage.GetComponent<RectTransform>();
            while (!pingServerTask.IsCompleted)
            {
                rect.Rotate(new Vector3(0, 0, m_ConnectionRotationSpeed * Time.unscaledDeltaTime), Space.Self);
                yield return null;
            }
            rect.rotation = Quaternion.identity;

            connection = pingServerTask.Result;
            if (connection.IsOnline)
            {
                m_ConnectionImage.sprite = m_ConnectedIcon;

                m_PlayerList.SetActive(true);
                m_PlayerListValue.text = $"{connection.MOTD.CurrentPlayers}/{connection.MOTD.MaxPlayers}";
                m_IPTextBox.text = connection.MOTD.Message;
            }
            else
                m_ConnectionImage.sprite = m_FailedToConnectIcon;
        }

        /// <summary>
        /// Pings the server to check if the server is active.
        /// </summary>
        public void TryReconnect()
        {
            m_ConnectionImage.sprite = m_ConnectingIcon;
            m_PlayerList.SetActive(false);

            m_NameTextBox.text = m_ServerName;
            m_IPTextBox.text = m_ServerIP;

            if (m_ServerConnectionCoroutine != null)
                StopCoroutine(m_ServerConnectionCoroutine);

            m_ServerConnectionCoroutine = DoCheckServerStatus();
            StartCoroutine(m_ServerConnectionCoroutine);
        }

        /// <summary>
        /// Parses a server IP into a host name and port number.
        /// </summary>
        /// <param name="ip">The combined IP address.</param>
        /// <returns>The host address and the port number.</returns>
        private (string address, int port) ParseIP(string ip)
        {
            var port = 23404; // Default port number

            if (ip.Contains(":"))
            {
                var split = ip.Split(':');
                ip = split[0];
                port = int.Parse(split[1]);
            }

            return (ip, port);
        }

        /// <summary>
        /// Preforms the raw server connection operation.
        /// </summary>
        /// <param name="connection">The server connection data.</param>
        /// <returns>The retrieved connection data.</returns>
        private ServerConnection ConnectToServer(ServerConnection connection)
        {
            try
            {
                var ip = ParseIP(connection.IP);
                var pingOp = new PingServerOperation(ip.address, ip.port);
                var motd = pingOp.SendPing();

                connection.IsOnline = true;
                connection.MOTD = motd;
            }
            catch (System.Exception)
            {
                connection.IsOnline = false;
            }

            return connection;
        }

        /// <inheritdoc cref="SelectionElement"/>
        protected override void OnSelectionChange(bool selected)
        {
            if (selected)
                m_BackgroundImage.color = m_SelectedColor;
            else
                m_BackgroundImage.color = m_DefaultColor;
        }
    }
}
