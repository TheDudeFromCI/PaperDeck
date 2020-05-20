namespace PaperDeck.Menu.ServerList
{
    public struct ServerConnection
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public bool IsOnline { get; set; }
        public ServerMOTD MOTD { get; set; }
    }

    public struct ServerMOTD
    {
        public string Message { get; set; }
        public int MaxPlayers { get; set; }
        public int CurrentPlayers { get; set; }
        public byte[] IconData { get; set; }
    }

}
