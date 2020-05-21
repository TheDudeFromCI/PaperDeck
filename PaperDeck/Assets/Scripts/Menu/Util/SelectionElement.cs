using UnityEngine;

namespace PaperDeck.Menu.Util
{
    /// <summary>
    /// A single element which can be placed within a selection list.
    /// </summary>
    [RequireComponent(typeof(ElementWidthFix))]
    public abstract class SelectionElement : MonoBehaviour
    {
        /// <summary>
        /// Gets whether or not this element is selected.
        /// </summary>
        public bool IsSelected { get; private set; }

        /// <summary>
        /// Sets whether or not this element is selected.
        /// </summary>
        /// <param name="selected"></param>
        internal void SetSelected(bool selected)
        {
            if (IsSelected == selected)
                return;

            IsSelected = selected;
            OnSelectionChange(selected);
        }

        /// <summary>
        /// Called when the selection status for this element changes.
        /// </summary>
        /// <param name="selected">The new selection state.</param>
        protected abstract void OnSelectionChange(bool selected);
    }
}