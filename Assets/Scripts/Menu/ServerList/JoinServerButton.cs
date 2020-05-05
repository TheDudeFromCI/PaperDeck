using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class JoinServerButton : MonoBehaviour
    {
        [SerializeField] private Button m_JoinButton;

        private ServerListElement m_Selected;

        public void OnSelectedElementChanged(ServerListElement elem)
        {
            m_Selected = elem;
            m_JoinButton.interactable = elem != null;
        }

        public void JoinServer()
        {
            if (m_Selected == null)
                return;

            // TODO Connect to server.
        }
    }
}
