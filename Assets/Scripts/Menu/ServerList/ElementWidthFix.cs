using UnityEngine;
using UnityEngine.UI;

namespace PaperDeck.Menu.ServerList
{
    [ExecuteInEditMode]
    public class ElementWidthFix : MonoBehaviour
    {
        private RectTransform rect;
        private RectTransform parentRect;
        private VerticalLayoutGroup layoutGroup;

        void OnEnable()
        {
            UpdateWidth();
        }

        void OnRectTransformDimensionsChange()
        {
            UpdateWidth();
        }

        private void UpdateWidth()
        {
            if (layoutGroup == null || rect == null || parentRect == null)
            {
                layoutGroup = GetComponentInParent<VerticalLayoutGroup>();
                if (layoutGroup != null)
                {
                    parentRect = layoutGroup.GetComponent<RectTransform>();
                    rect = GetComponent<RectTransform>();
                    rect.pivot = new Vector2(0, 1);
                    rect.sizeDelta = new Vector2(parentRect.rect.size.x - (layoutGroup.padding.left + layoutGroup.padding.right),
                        rect.sizeDelta.y);
                }
            }
            else
            {
                rect.sizeDelta = new Vector2(parentRect.rect.size.x - (layoutGroup.padding.left + layoutGroup.padding.right),
                    rect.sizeDelta.y);
            }
        }
    }
}
