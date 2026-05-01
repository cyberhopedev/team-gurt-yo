/// <summary>
/// Constants that will be used by the project
/// </summary>
public static class GameConstants {

    /// <summary>
    /// Constants related to scene management
    /// </summary>
    public static class Scenes
    {
        public const string TITLE_SCENE = "TitleScreenScene";
        public const string CLASS_SELECT_SCENE = "ClassSelectScene";
        public const string INTRO_SCENE = "IntroScene";
        public const string BATTLE_SCENE = "BattleEncounterScene";
        public const string MAIN_SCENE = "AlphaScene";
    }

    /// <summary>
    /// Constants related to the saving logic
    /// </summary>
    public static class Save
    {
        public const int MAX_SLOTS = 3;
    }

    /// <summary>
    /// Constants related to the inventory logic
    /// </summary>
    public static class Inventory
    {
        public const int MAX_SLOTS = 36;
    }
}