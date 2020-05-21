using TMPro;

using UnityEngine;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class ServerPropertiesPanel : MonoBehaviour
    {
        [SerializeField] protected TMP_InputField m_NameInput;
        [SerializeField] protected TMP_InputField m_IPInput;
        [SerializeField] protected TextMeshProUGUI m_AddServerButton;
        [SerializeField] protected TextMeshProUGUI m_RemoveServerButton;
        [SerializeField] protected GameObject m_RemoveServerButtonRoot;
        [SerializeField] protected ServerList m_ServerList;

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
            }
            else
            {
                m_NameInput.text = m_ServerList.Selected.ServerName;
                m_IPInput.text = m_ServerList.Selected.ServerIP;

                m_AddServerButton.text = "Apply Changes";
                m_RemoveServerButton.text = "Remove Server";
                m_RemoveServerButtonRoot.SetActive(true);
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
    }
}
