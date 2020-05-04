using UnityEngine;
using System.Linq;

namespace PaperDeck.Menu.ServerList
{
    public class ServerListPopulator : MonoBehaviour
    {
        public ServerListElement m_ServerListElementPrefab;

        void Awake()
        {
            //TODO Load from config

            for (int i = 0; i < 50; i++)
            {
                var elem = Instantiate(m_ServerListElementPrefab);
                elem.transform.SetParent(transform, false);

                elem.m_ServerName = RandomString(random.Next() % 30 + 10);
                elem.m_ServerIP = RandomString(random.Next() % 15 + 5);
            }
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