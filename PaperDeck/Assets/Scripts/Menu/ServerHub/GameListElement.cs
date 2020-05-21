using PaperDeck.Menu.Util;
using UnityEngine;
using UnityEngine.UI;

namespace PaperDeck.Menu.ServerHub
{
    /// <summary>
    /// A single active game on a server.
    /// </summary>
    public class GameListElement : SelectionElement
    {
        [Header("Settings")]
        [SerializeField] protected Color m_SelectedColor;

        [Header("Game Objects")]
        [SerializeField] protected Image m_BackgroundImage;

        protected Color m_DefaultColor;

        /// <summary>
        /// Gets the name of this game.
        /// </summary>
        /// <value>The name of the game.</value>
        public string GameName { get; set; }

        /// <summary>
        /// Gets the number of online players in this game.
        /// </summary>
        /// <value>The player count.</value>
        public int GamePlayerCount { get; set; }

        /// <summary>
        /// Called when the game list element is initialized.
        /// </summary>
        protected virtual void Start()
        {
            m_DefaultColor = m_BackgroundImage.color;
        }

        /// <inheritdoc cref="SelectionElement"/>
        protected override void OnSelectionChange(bool selected)
        {
            if (selected)
                m_BackgroundImage.color = m_SelectedColor;
            else
                m_BackgroundImage.color = m_DefaultColor;
        }
    }
}