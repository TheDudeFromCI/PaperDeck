using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PaperDeck.Network;
using System.Collections;

namespace PaperDeck.Menu.ServerList
{
    /// <summary>
    /// Contains all of the properties for interacting with a server.
    /// </summary>
    public class ServerPropertiesPanel : MonoBehaviour
    {
        [Header("Game Objects")]
        [SerializeField] protected TMP_InputField m_NameInput;
        [SerializeField] protected TMP_InputField m_IPInput;
        [SerializeField] protected TextMeshProUGUI m_AddServerButton;
        [SerializeField] protected TextMeshProUGUI m_RemoveServerButton;
        [SerializeField] protected GameObject m_RemoveServerButtonRoot;
        [SerializeField] protected ServerList m_ServerList;
        [SerializeField] protected Button m_JoinButton;

        /// <summary>
        /// Called when the properties panel is constructed to correct the text display.
        /// </summary>
        protected virtual void Awake()
        {
            OnElementSelectionChanged();
        }

        /// <summary>
        /// Called whenever the selected element in the server list is changed.
        /// </summary>
        /// <param name="selection">The new selected element.</param>
        public void OnElementSelectionChanged()
        {
            if (m_ServerList.Selected == null)
            {
                m_NameInput.text = "";
                m_IPInput.text = "";

                m_AddServerButton.text = "Add Server";
                m_RemoveServerButtonRoot.SetActive(false);
                m_JoinButton.interactable = false;
            }
            else
            {
                m_NameInput.text = m_ServerList.Selected.ServerName;
                m_IPInput.text = m_ServerList.Selected.ServerIP;

                m_AddServerButton.text = "Apply Changes";
                m_RemoveServerButton.text = "Remove Server";
                m_RemoveServerButtonRoot.SetActive(true);
                m_JoinButton.interactable = true;
            }
        }

        /// <summary>
        /// Called when the add server button is clicked to add or edit a server in the list.
        /// </summary>
        public void AddServerButton()
        {
            if (m_ServerList.Selected == null)
            {
                m_ServerList.AddServer(m_NameInput.text, m_IPInput.text);

                m_NameInput.text = "";
                m_IPInput.text = "";
            }
            else
            {
                m_ServerList.Selected.ServerName = m_NameInput.text;
                m_ServerList.Selected.ServerIP = m_IPInput.text;
                m_ServerList.Selected.TryReconnect();
            }
        }

        /// <summary>
        /// Called when the remove server button is clicked to remove a server from the list.
        /// </summary>
        public void RemoveServerButton()
        {
            if (m_ServerList.Selected == null)
                return;

            m_ServerList.RemoveServer(m_ServerList.Selected);
            m_NameInput.text = "";
            m_IPInput.text = "";
        }

        /// <summary>
        /// Triggers the selected server to attempt to reconnect.
        /// </summary>
        public void RefreshServerButton()
        {
            m_ServerList.Selected?.TryReconnect();
        }

        /// <summary>
        /// Loads the title screen scene.
        /// </summary>
        public void ToTitleScreen()
        {
            SceneManager.LoadScene("TitleScreen");
        }

        /// <summary>
        /// Joins the selected server.
        /// </summary>
        public void JoinServer()
        {
            if (m_ServerList.Selected == null)
                return;

            StartCoroutine(DoConnectToServer());
        }

        private IEnumerator DoConnectToServer()
        {
            var ip = m_ServerList.Selected.ServerIP;
            var port = m_ServerList.Selected.ServerPort;
            var conn = PendingServerConnection.Connect(ip, port);

            while (conn.Status == ConnectionStatus.Connecting)
                yield return null;

            if (conn.Status == ConnectionStatus.FailedToConnect)
            {
                Debug.LogWarning("Failed to connect to server!");

                // TODO Show connection failed status to user
                yield break;
            }
            else
            {
                DontDestroyOnLoad(conn.gameObject);
                SceneManager.LoadScene("ServerHub");
            }
        }
    }
}
