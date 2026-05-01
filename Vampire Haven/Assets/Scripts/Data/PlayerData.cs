using UnityEngine;

/// <summary> 
/// Holds the data between the overworld and battle scene transition
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "Battle/PlayerData")]
public class PlayerData : ScriptableObject
{
    // Position
    public static Vector3 lastPosition;
    public static bool hasSavedPosition = false;

    // Core stats
    public int maxHP = 100;
    public int attackDamage = 10;
    public int armor = 0;
    public int gold = 0;
    public int speedStat = 5;

    // Persistent current HP, starts at max and is updated later
    public int currentHP = 100;

    [Header("Class & Progression")]
    public PlayerClass playerClass = PlayerClass.Fighter; // default starter

    // Regular level with XP, current scale for needed XP is NextLevel = CurrentLevel * 100
    public int level = 1;
    public int xp    = 0;

    // Current amount of vitae
    [Header("Vitae and Vampirism Level")]
    public int vitae    = 0;     // current resource
    public int maxVitae = 100;
    // vampirismLevel    = VampirismLevel.None;

    // List of the unlocked abilities
    public List<Ability> unlockedAbilities = new List<Ability>();

    /// <summary>
    /// Resets all fields back to default values.
    /// Call on "New Game" so a previous run's data doesn't bleed in.
    /// </summary>
    public void ResetToDefaults()
    {
        lastPosition      = Vector3.zero;
        hasSavedPosition  = false;
        maxHP             = 100;
        currentHP         = 100;
        attackDamage      = 10;
        armor             = 0;
        gold              = 0;
        speedStat         = 5;
    }

}