using UnityEngine;
using UnityEngine.EventSystems;

namespace PaperDeck.Menu.Util
{
    /// <summary>
    /// Causes a start selection list to be deselected when clicked.
    /// </summary>
    public class ListDeselect : MonoBehaviour, IPointerDownHandler
    {
        [Tooltip("The selection list to trigger the deselect for.")]
        [SerializeField] protected SelectionList m_Selector;

        /// <summary>
        /// Called when the user clicks on this item.
        /// </summary>
        public void OnPointerDown(PointerEventData eventData) => m_Selector.Select(-1);
    }
}
