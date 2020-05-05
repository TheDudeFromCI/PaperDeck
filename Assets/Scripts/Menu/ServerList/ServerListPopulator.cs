using System.Collections.Generic;
using System.Linq;

using UnityEngine;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class ServerListPopulator : MonoBehaviour
    {
        [SerializeField] private ServerListElement m_ServerListElementPrefab;
        [SerializeField] private ServerElementSelection m_ElementSelection;

        private readonly List<ServerListElement> m_Elements = new List<ServerListElement>();

        void Awake()
        {
            //TODO Load from config

            for (int i = 0; i < 5; i++)
            {
                var name = RandomString(random.Next() % 20 + 3);
                var ip = RandomString(random.Next() % 15 + 5);

                AddServer(name, ip, false);
            }
        }

        public void AddServer(string name, string ip, bool save = true)
        {
            var elem = Instantiate(m_ServerListElementPrefab);
            elem.transform.SetParent(transform, false);

            elem.ServerName = name;
            elem.ServerIP = ip;

            m_ElementSelection.AddElement(elem);

            if (save)
                SaveConfig();
        }

        public void RemoveServer(ServerListElement elem, bool save = true)
        {
            Destroy(elem.gameObject);
            m_ElementSelection.RemoveElement(elem);

            if (save)
                SaveConfig();
        }

        private void SaveConfig()
        {
            // TODO Save to config
        }

        private static System.Random random = new System.Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .-'+={}()[]_";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
