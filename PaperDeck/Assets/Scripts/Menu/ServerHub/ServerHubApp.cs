using PaperDeck.Network;
using UnityEngine;

namespace PaperDeck.Menu.ServerHub
{
    /// <summary>
    /// The controller for handling the server hub menu.
    /// </summary>
    public class ServerHubApp : MonoBehaviour
    {
        [Header("Game Objects")]
        [Tooltip("The game list panel.")]
        [SerializeField] protected GameList m_GameList;

        private ServerConnection m_ServerConnection;

        /// <summary>
        /// Called when the scene is loaded to verify a server connection exists.
        /// </summary>
        protected virtual void Awake()
        {
            m_ServerConnection = ServerConnection.Instance;

            if (m_ServerConnection == null)
                throw new System.InvalidOperationException("Cannot load scene without active server connection!");
        }
    }
}
