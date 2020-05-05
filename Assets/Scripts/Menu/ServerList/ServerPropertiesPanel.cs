using TMPro;

using UnityEngine;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class ServerPropertiesPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_NameInput;
        [SerializeField] private TMP_InputField m_IPInput;

        public void OnElementSelectionChanged(ServerListElement selection)
        {
            if (selection == null)
            {
                m_NameInput.text = "";
                m_IPInput.text = "";
            }
            else
            {
                m_NameInput.text = selection.ServerName;
                m_IPInput.text = selection.ServerIP;
            }
        }
    }
}
