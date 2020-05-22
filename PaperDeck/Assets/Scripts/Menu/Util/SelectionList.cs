using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PaperDeck.Menu.Util
{
    /// <summary>
    /// A non-generic version of SelectionList for internal use.
    /// </summary>
    public abstract class SelectionList : MonoBehaviour
    {
        /// <summary>
        /// Selected the element at the given index.
        /// </summary>
        /// <param name="index">The index of the selected element.</param>
        public abstract void Select(int index);
    }

    /// <summary>
    /// A selection list is a container for handling a list of selectable UI elements.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public abstract class SelectionList<T> : SelectionList, IPointerDownHandler
        where T : SelectionElement
    {
        [Tooltip("An event which is triggered when the selected element changes.")]
        [SerializeField] protected UnityEvent m_OnSelectionChange = new UnityEvent();

        [Tooltip("Gets the height of each element in this list.")]
        [SerializeField] protected float m_ElementHeight;

        [Tooltip("The prefab item which is created for this list.")]
        [SerializeField] protected T m_ListElementPrefab;

        [Tooltip("The panel to add the prefab objects to.")]
        [SerializeField] protected Transform m_ContentPanel;

        private readonly List<T> m_Elements = new List<T>();
        private RectTransform m_RectTransform;
        private T m_Selected;

        /// <summary>
        /// Gets the currently selected element within this list.
        /// </summary>
        /// <value>The selected element, or null if nothing is selected.</value>
        public T Selected
        {
            get => m_Selected;
            private set
            {
                if (m_Selected == value)
                    return;

                m_Selected?.SetSelected(false);
                m_Selected = value;
                m_Selected?.SetSelected(true);

                m_OnSelectionChange?.Invoke();
            }
        }

        /// <summary>
        /// Called when this behaviour is initialized.
        /// </summary>
        protected virtual void Awake()
        {
            m_RectTransform = m_ContentPanel.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Creates a new instance of the target element and adds it to this list.
        /// </summary>
        /// <returns>The newly created element.</returns>
        protected T AddElement()
        {
            var elem = Instantiate(m_ListElementPrefab);
            elem.transform.SetParent(m_ContentPanel, false);
            m_Elements.Add(elem);

            return elem;
        }

        /// <summary>
        /// Removes an element from this selection list.
        /// </summary>
        /// <param name="selection">The list element to remove.</returns>
        protected void RemoveElement(T elem)
        {
            if (Selected == elem)
                Selected = null;

            m_Elements.Remove(elem);
            Destroy(elem.gameObject);
        }

        /// <inheritdoc cref="SelectionList"/>
        public override void Select(int index)
        {
            if (index == -1)
                Selected = null;
            else
                Selected = m_Elements[index];
        }

        /// <summary>
        /// Called when the user clicks on this scroll view.
        /// </summary>
        /// <param name="eventData">The mouse event data.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_RectTransform, eventData.position, null, out var pos);

            int elem = Mathf.FloorToInt(pos.y / -m_ElementHeight);

            if (elem < 0 || elem >= m_Elements.Count)
                elem = -1;

            Select(elem);
        }
    }
}