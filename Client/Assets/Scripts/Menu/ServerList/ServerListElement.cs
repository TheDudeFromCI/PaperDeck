using System.Collections;
using System.Threading.Tasks;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class ServerListElement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float m_ConnectionRotationSpeed = -360f;
        [SerializeField] private Color m_SelectedColor;

        [Header("Game Objects")]
        [SerializeField] private Image m_ConnectionImage;
        [SerializeField] private GameObject m_PlayerList;
        [SerializeField] private TextMeshProUGUI m_PlayerListValue;
        [SerializeField] private TextMeshProUGUI m_NameTextBox;
        [SerializeField] private TextMeshProUGUI m_IPTextBox;
        [SerializeField] private Image m_BackgroundImage;

        [Header("Icons")]
        [SerializeField] private Sprite m_ConnectedIcon;
        [SerializeField] private Sprite m_ConnectingIcon;
        [SerializeField] private Sprite m_FailedToConnectIcon;

        private Color m_DefaultColor;
        private string m_ServerName;
        private string m_ServerIP;
        private IEnumerator m_ServerConnectionCoroutine;

        public string ServerName
        {
            get => m_ServerName;
            set
            {
                m_ServerName = value;
                m_NameTextBox.text = value;
            }
        }
        public string ServerIP
        {
            get => m_ServerIP;
            set
            {
                m_ServerIP = value;
                m_IPTextBox.text = value;
            }
        }

        void Start()
        {
            m_DefaultColor = m_BackgroundImage.color;
            TryReconnect();
        }

        IEnumerator DoCheckServerStatus()
        {
            var connection = new ServerConnection
            {
                Name = m_ServerName,
                IP = m_ServerIP,
            };
            var pingServerTask = Task<ServerConnection>.Run(() => ConnectToServer(connection));

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
                m_PlayerListValue.text = $"{connection.PlayersOnline}/{connection.MaxPlayers}";
            }
            else
                m_ConnectionImage.sprite = m_FailedToConnectIcon;
        }

        public void SetSelected(bool selected)
        {
            if (selected)
                m_BackgroundImage.color = m_SelectedColor;
            else
                m_BackgroundImage.color = m_DefaultColor;
        }

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

        private (string address, int port) ParseIP(string ip)
        {
            var port = 23404;

            if (ip.Contains(":"))
            {
                var split = ip.Split(':');
                ip = split[0];
                port = int.Parse(split[1]);
            }

            return (address: ip, port: port);
        }

        ServerConnection ConnectToServer(ServerConnection connection)
        {
            var ip = ParseIP(connection.IP);
            var pingOp = new PingServerOperation(ip.address, ip.port);

            try
            {
                pingOp.Connect();

                // TODO Load server details

                connection.IsOnline = true;
                connection.MaxPlayers = 50;
                connection.PlayersOnline = 2;
            }
            catch (System.Exception)
            {
                connection.IsOnline = false;
            }

            return connection;
        }
    }
}
