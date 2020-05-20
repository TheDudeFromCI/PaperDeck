using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#pragma warning disable 649

namespace PaperDeck.Menu.ServerList
{
    public class ServerElementDeselect : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private ServerElementSelection m_Selector;

        public void OnPointerDown(PointerEventData eventData) => m_Selector.Select(-1);
    }
}
