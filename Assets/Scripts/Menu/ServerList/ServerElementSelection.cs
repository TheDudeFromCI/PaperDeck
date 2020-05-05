using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PaperDeck.Menu.ServerList
{
    public class ServerElementSelection : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private int m_ElementHeight = 75;
        [SerializeField] private SelectionChangeEvent m_OnSelectionChange = new SelectionChangeEvent();

        private readonly List<ServerListElement> m_Elements = new List<ServerListElement>();
        private RectTransform m_RectTransform;
        private int m_Index;

        public ServerListElement Selected => m_Index == -1 ? null : m_Elements[m_Index];

        void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
        }

        public void AddElement(ServerListElement selection)
        {
            m_Elements.Add(selection);
        }

        public void Select(int index)
        {
            if (m_Index != -1)
                m_Elements[m_Index].SetSelected(false);

            if (index != -1)
                m_Elements[index].SetSelected(true);

            m_Index = index;

            m_OnSelectionChange?.Invoke(Selected);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_RectTransform, eventData.position, null, out var pos);
            int elem = Mathf.FloorToInt(pos.y / -m_ElementHeight);

            if (elem < 0 || elem >= m_Elements.Count)
                elem = -1;

            Select(elem);
        }
    }

    [System.Serializable]
    public class SelectionChangeEvent : UnityEvent<ServerListElement> {}
}
