using UnityEngine;

/// <summary>
/// Base class for an ability the player can use in battle. Subclasses
/// implement Execute() with the actual mechanics.
/// </summary>
[CreateAssetMenu(fileName = "New Ability", menuName = "Battle/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    [TextArea] public string description;
    public Sprite icon;
    public int vitaeCost = 0;
    public int hpCost    = 0;     // % of max HP, used by vampire abilities

    [Tooltip("Which class unlocks this. None = vampire ability available to all.")]
    public PlayerClass classRequirement = PlayerClass.Fighter;

    [Tooltip("Dungeon # required for unlock (0 = always unlocked).")]
    public int dungeonUnlock = 0;

    /// <summary>
    /// Override per-ability. Returns the damage dealt to the enemy (0 for buffs).
    /// </summary>
    public virtual int Execute(PlayerBattler caster, Enemy target) => 0;
}