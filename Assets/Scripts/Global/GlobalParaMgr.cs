namespace Global
{
    public static class GlobalParaMgr
    {
        public static Scenes NextSceneName = Scenes.LogonScene;
        public static string PlayerName = "";
        public static PlayerType PlayerTypes = PlayerType.SwordHero;
        public static CurrentGameType CurGameType = CurrentGameType.NewGame;
        public static bool IsBossFightingScene = false;
    }
}
