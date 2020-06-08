using UnityEngine;
using UnityEngine.UI;

namespace PaperDeck.Menu.CardBuilder
{
    public class ImageRotator : MonoBehaviour
    {
        [Tooltip("The image to apply the rotation to.")]
        [SerializeField] protected Image m_Image;

        /// <summary>
        /// Gets the rotation of the image.
        /// </summary>
        /// <param name="value">The rotation in degrees</param>
        public void SetRotation(float value)
        {
            m_Image.transform.localRotation = Quaternion.Euler(0f, 0f, value);
        }
    }
}