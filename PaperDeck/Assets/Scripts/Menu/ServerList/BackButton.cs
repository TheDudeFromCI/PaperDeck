using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperDeck.Menu.ServerList
{
    /// <summary>
    /// A simple button action which takes the user back to the title screen.
    /// </summary>
    public class BackButton : MonoBehaviour
    {
        /// <summary>
        /// Loads the title screen scene.
        /// </summary>
        public void ToTitleScreen()
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
