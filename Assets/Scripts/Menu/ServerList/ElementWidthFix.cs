using UnityEngine;
using UnityEngine.UI;

namespace PaperDeck.Menu.ServerList
{
    [ExecuteInEditMode]
    public class ElementWidthFix : MonoBehaviour
    {
        private RectTransform m_Rect;
        private RectTransform m_ParentRect;
        private VerticalLayoutGroup m_LayoutGroup;

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
            if (m_LayoutGroup == null || m_Rect == null || m_ParentRect == null)
            {
                m_LayoutGroup = GetComponentInParent<VerticalLayoutGroup>();
                if (m_LayoutGroup != null)
                {
                    m_ParentRect = m_LayoutGroup.GetComponent<RectTransform>();
                    m_Rect = GetComponent<RectTransform>();
                    m_Rect.pivot = new Vector2(0, 1);
                    m_Rect.sizeDelta = new Vector2(m_ParentRect.rect.size.x - (m_LayoutGroup.padding.left + m_LayoutGroup.padding.right),
                        m_Rect.sizeDelta.y);
                }
            }
            else
            {
                m_Rect.sizeDelta = new Vector2(m_ParentRect.rect.size.x - (m_LayoutGroup.padding.left + m_LayoutGroup.padding.right),
                    m_Rect.sizeDelta.y);
            }
        }
    }
}
