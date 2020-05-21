using PaperDeck.Menu.Util;

namespace PaperDeck.Menu.ServerList
{
    /// <summary>
    /// A list of servers which can be connected to.
    /// </summary>
    public class ServerList : SelectionList<ServerListElement>
    {
        /// <summary>
        /// Called when the server list is initialized.
        /// </summary>
        protected virtual void Start()
        {
            LoadConfig();
        }

        /// <summary>
        /// Adds a new server to this list.
        /// </summary>
        /// <param name="name">The server name.</param>
        /// <param name="ip">The server IP.</param>
        public void AddServer(string name, string ip)
        {
            var elem = AddElement();

            elem.ServerName = name;
            elem.ServerIP = ip;
        }

        /// <summary>
        /// Removes a server from this list.
        /// </summary>
        /// <param name="server">The server to remove.</param>
        public void RemoveServer(ServerListElement server)
        {
            RemoveElement(server);
        }

        /// <summary>
        /// Saves all servers in this list to the local config.
        /// </summary>
        public void SaveConfig()
        {
            // TODO Save to config
        }

        /// <summary>
        /// Loads all servers in local config to this list.
        /// </summary>
        private void LoadConfig()
        {
            // TODO Load from config
        }
    }
}
