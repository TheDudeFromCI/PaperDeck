using UnityEngine;
using UnityEngine.UI;

namespace PaperDeck.Menu.CardBuilder
{
    public class ImageScaler : MonoBehaviour
    {
        [Tooltip("The maximum amount of zoom allowed for the image.")]
        [SerializeField, Range(1f, 20f)] protected float m_ScalingStrength = 10f;

        [Tooltip("The image to apply the scaling to.")]
        [SerializeField] protected Image m_Image;

        /// <summary>
        /// Gets the size of the image. Value is normalized between -1 and 1.
        /// </summary>
        /// <param name="value">The amount of scaling.</param>
        public void SetSize(float value)
        {
            value = Mathf.Pow(m_ScalingStrength, value);
            m_Image.transform.localScale = Vector3.one * value;
        }
    }
}