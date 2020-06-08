using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PaperDeck.Menu.CardBuilder
{
    public class ImagePanner : MonoBehaviour
    {
        class ImagePanCallback : MonoBehaviour, IDragHandler
        {
            public void OnDrag(PointerEventData eventData)
            {
                var delta = eventData.delta;
                transform.localPosition += new Vector3(delta.x, delta.y, 0f);
            }
        }

        [SerializeField] protected Image m_Image;

        protected void Awake()
        {
            m_Image.gameObject.AddComponent<ImagePanCallback>();
        }
    }
}