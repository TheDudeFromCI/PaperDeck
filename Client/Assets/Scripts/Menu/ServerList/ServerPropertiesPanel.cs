using TMPro;

using UnityEngine;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class ServerPropertiesPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_NameInput;
        [SerializeField] private TMP_InputField m_IPInput;
        [SerializeField] private TextMeshProUGUI m_AddServerButton;
        [SerializeField] private TextMeshProUGUI m_RemoveServerButton;
        [SerializeField] private GameObject m_RemoveServerButtonRoot;
        [SerializeField] private ServerListPopulator m_ServerListPopulator;

        private ServerListElement m_Selected;

        void Awake()
        {
            OnElementSelectionChanged(null);
        }

        public void OnElementSelectionChanged(ServerListElement selection)
        {
            m_Selected = selection;

            if (selection == null)
            {
                m_NameInput.text = "";
                m_IPInput.text = "";

                m_AddServerButton.text = "Add Server";
                m_RemoveServerButtonRoot.SetActive(false);
            }
            else
            {
                m_NameInput.text = selection.ServerName;
                m_IPInput.text = selection.ServerIP;

                m_AddServerButton.text = "Apply Changes";
                m_RemoveServerButton.text = "Remove Server";
                m_RemoveServerButtonRoot.SetActive(true);
            }
        }

        public void AddServerButton()
        {
            if (m_Selected == null)
            {
                m_ServerListPopulator.AddServer(m_NameInput.text, m_IPInput.text);

                m_NameInput.text = "";
                m_IPInput.text = "";
            }
            else
            {
                m_Selected.ServerName = m_NameInput.text;
                m_Selected.ServerIP = m_IPInput.text;
                m_Selected.TryReconnect();
            }
        }

        public void RemoveServerButton()
        {
            if (m_Selected == null)
                return;

            m_ServerListPopulator.RemoveServer(m_Selected);
            m_NameInput.text = "";
            m_IPInput.text = "";
        }

        public void RefreshServerButton()
        {
            m_Selected?.TryReconnect();
        }
    }
}
