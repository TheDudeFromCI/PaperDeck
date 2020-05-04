using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using System.Collections;

namespace PaperDeck.Menu.ServerList
{
    public class ServerListElement : MonoBehaviour
    {
        [Header("Settings")]
        public string m_ServerName;
        public string m_ServerIP;
        public float m_ConnectionRotationSpeed = -360f;

        [Header("Game Objects")]
        public Image m_ConnectionImage;
        public GameObject m_PlayerList;
        public TextMeshProUGUI m_PlayerListValue;
        public TextMeshProUGUI m_ServerNameValue;
        public TextMeshProUGUI m_ServerIPValue;

        [Header("Icons")]
        public Sprite m_ConnectedIcon;
        public Sprite m_ConnectingIcon;
        public Sprite m_FailedToConnectIcon;

        void Start()
        {
            m_ConnectionImage.sprite = m_ConnectingIcon;
            m_PlayerList.SetActive(false);

            m_ServerNameValue.text = m_ServerName;
            m_ServerIPValue.text = m_ServerIP;

            StartCoroutine(DoCheckServerStatus());
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

        static System.Random random = new System.Random();
        ServerConnection ConnectToServer(ServerConnection connection)
        {
            int time;
            lock (random)
            {
                time = random.Next() % 7000;
            }

            Task.Delay(time).Wait();

            lock (random)
            {
                connection.IsOnline = random.NextDouble() > 0.5;

                if (connection.IsOnline)
                {
                    connection.MaxPlayers = (random.Next() % 20 + 1) * 5;
                    connection.PlayersOnline = random.Next() % connection.MaxPlayers;
                }

                return connection;
            }
        }
    }
}