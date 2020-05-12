using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperDeck.Menu.Title
{
    public class ServerListButton : MonoBehaviour
    {
        public void OpenServerList()
        {
            SceneManager.LoadScene("ServerList");
        }
    }
}
