using System.Linq;

using UnityEngine;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class ServerListPopulator : MonoBehaviour
    {
        [SerializeField] private ServerListElement m_ServerListElementPrefab;
        [SerializeField] private ServerElementSelection m_ElementSelection;

        void Awake()
        {
            //TODO Load from config

            for (int i = 0; i < 5; i++)
            {
                var elem = Instantiate(m_ServerListElementPrefab);
                elem.transform.SetParent(transform, false);

                elem.ServerName = RandomString(random.Next() % 20 + 3);
                elem.ServerIP = RandomString(random.Next() % 15 + 5);

                m_ElementSelection.AddElement(elem);
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
