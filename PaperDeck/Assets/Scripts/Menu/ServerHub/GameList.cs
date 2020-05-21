using PaperDeck.Menu.Util;

namespace PaperDeck.Menu.ServerHub
{
    /// <summary>
    /// A list of active games on a server.
    /// </summary>
    public class GameList : SelectionList<GameListElement>
    {
        protected void Start()
        {
            // TODO Remove this

            AddGame("Game1", 10);
            AddGame("Game2", 20);
            AddGame("Game3", 30);
        }

        /// <summary>
        /// Adds an active game to this game list.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="players">The number of players in the game.</param>
        /// <returns>The newly created game list element.</returns>
        public GameListElement AddGame(string name, int players)
        {
            var elem = AddElement();
            elem.GameName = name;
            elem.GamePlayerCount = players;

            return elem;
        }

        /// <summary>
        /// Removes a game from this list.
        /// </summary>
        /// <param name="game">The game to remove.</param>
        public void RemoveGame(GameListElement game)
        {
            RemoveElement(game);
        }
    }
}