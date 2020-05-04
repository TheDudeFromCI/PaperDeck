namespace PaperDeck.Menu.ServerList
{
    public struct ServerConnection
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int PlayersOnline { get; set; }
        public int MaxPlayers { get; set; }
        public bool IsOnline { get; set; }
    }
}