using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperDeck.Menu.ServerList
{
    public class BackButton : MonoBehaviour
    {
        public void ToTitleScreen()
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
