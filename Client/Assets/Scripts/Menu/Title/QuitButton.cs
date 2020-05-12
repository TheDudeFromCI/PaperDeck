using UnityEngine;

namespace PaperDeck.Menu.Title
{
    public class QuitButton : MonoBehaviour
    {
        public void QuitGame()
        {
            Debug.Log("Exiting Paper Deck");
            Application.Quit();
        }
    }
}
