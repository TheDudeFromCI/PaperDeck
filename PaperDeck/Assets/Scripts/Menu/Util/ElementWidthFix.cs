using UnityEngine;
using UnityEngine.UI;

namespace PaperDeck.Menu.Util
{
    /// <summary>
    /// Fixes a list element within a stretched vertical layout.
    /// </summary>
    [ExecuteInEditMode]
    public class ElementWidthFix : MonoBehaviour
    {
        private RectTransform m_Rect;
        private RectTransform m_ParentRect;
        private VerticalLayoutGroup m_LayoutGroup;

        /// <summary>
        /// Called when the behaviour is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            UpdateWidth();
        }

        /// <summary>
        /// Called when the parent element resizes this element.
        /// </summary>
        protected virtual void OnRectTransformDimensionsChange()
        {
            UpdateWidth();
        }

        /// <summary>
        /// Updates the width to match the parent.
        /// </summary>
        protected void UpdateWidth()
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
