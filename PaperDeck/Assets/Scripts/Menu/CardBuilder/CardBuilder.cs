using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperDeck.Menu.CardBuilder
{
    public class CardBuilder : MonoBehaviour
    {
        public void BackButton()
        {
            SceneManager.LoadScene("ServerHub");
        }

        public void SaveButton()
        {
            // TODO Implement save button
        }
    }
}