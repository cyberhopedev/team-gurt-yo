using UnityEngine;

/// <summary>
/// Base class for an ability the player can use in battle. Subclasses
/// implement Execute() with the actual mechanics.
/// </summary>
[CreateAssetMenu(fileName = "New Ability", menuName = "Battle/Ability")]
public class Ability : ScriptableObject
{
    [Header("Identity")]
    public string abilityName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Costs")]
    [Tooltip("Vitae spent when this ability is cast. 0 = free.")]
    public int vitaeCost = 0;

    [Tooltip("HP self-damage as a % of maxHP. Used by vampire abilities.")]
    public int hpCostPercent = 0;

    [Header("Unlock")]
    public PlayerClass classRequirement = PlayerClass.Fighter;

    [Tooltip("Dungeon # required for unlock (0 = always unlocked).")]
    public int dungeonUnlock = 0;

    /// <summary>
    /// Override per-ability. Returns the damage dealt to the enemy (0 for buffs).
    /// </summary>
    public virtual int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        return 0;
    }

    /// <summary>
    /// Helper called by subclasses to apply the HP cost. Centralizes the
    /// maxHP math so each ability doesn't repeat it. `protected` means
    /// subclasses can call it but outside code can't.
    /// </summary>
    protected void PayHpCost(PlayerBattler caster)
    {
        if (hpCostPercent <= 0)
        {
            return;
        }
        int hpDamage = Mathf.RoundToInt(caster.data.maxHP * (hpCostPercent / 100f));
        caster.TakeDamage(hpDamage);
    }
}

// CLASS STARTER ABILITIES

/// <summary>
/// Tank starter: gain a flat armor bonus this turn. The bonus persists until
/// damage chips through it, just like any other armor.
/// </summary>
[CreateAssetMenu(fileName = "Block", menuName = "Battle/Abilities/Block")]
public class BlockAbility : Ability
{
    [Tooltip("How much armor (block) to grant when cast.")]
    public int armorBonus = 5;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        caster.giveArmor(armorBonus);
        return 0;
    }
}

/// <summary>
/// Rogue starter: skip current turn, then attack twice with crit advantage
/// next turn. Implemented by adding a Stealthed status — BattleManager checks
/// for it at the top of the next PlayerTurn.
/// </summary>
[CreateAssetMenu(fileName = "Stealth", menuName = "Battle/Abilities/Stealth")]
public class StealthAbility : Ability
{
    [Tooltip("How many turns the Stealthed status persists.")]
    public int turnsActive = 1;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        // Magnitude unused for Stealthed — it's a flag, not a number. We
        // store it anyway so the status struct has a consistent shape.
        caster.activeStatuses.Add(new StatusEffect(StatusType.Stealthed, 0, turnsActive));
        return 0;
    }
}

/// <summary>
/// Archer starter: target enemy takes 150% damage from the next attack.
/// Applied as a Marked status — BattleManager.Attack reads it, multiplies
/// damage, then removes the status.
/// </summary>
[CreateAssetMenu(fileName = "Mark", menuName = "Battle/Abilities/Mark")]
public class MarkAbility : Ability
{
    [Tooltip("Damage multiplier expressed as a percentage (150 = 1.5x).")]
    public int damageMultiplierPercent = 150;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        target.activeStatuses.Add(new StatusEffect(StatusType.Marked, damageMultiplierPercent, 1));
        return 0;
    }
}

/// <summary>
/// Fighter starter: deal half normal damage. Per design doc, the multi-target
/// version was simplified to single-target — extend later by iterating over
/// enemies if the battle system supports party fights.
/// </summary>
[CreateAssetMenu(fileName = "Swipe", menuName = "Battle/Abilities/Swipe")]
public class SwipeAbility : Ability
{
    [Tooltip("Damage as a percentage of the player's basic attack (50 = half).")]
    public int damagePercent = 50;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        // Mathf.RoundToInt avoids losing the .5 from odd attack values
        // (e.g. 5 attack → 2 damage instead of 3). Round to the nearest int.
        int damage = Mathf.RoundToInt(caster.data.attackDamage * (damagePercent / 100f));
        target.TakeDamage(damage);
        return damage;
    }
}

// VAMPIRE ABILITIES

/// <summary>
/// Dungeon 1 (20% vampire): moderate damage; if the hit kills, heal a chunk
/// of maxHP. Models the JRPG "drain" archetype.
/// </summary>
[CreateAssetMenu(fileName = "Leech", menuName = "Battle/Abilities/Leech")]
public class LeechAbility : Ability
{
    public int damage = 6;

    [Tooltip("Percent of maxHP healed if the strike kills.")]
    public int healPercent = 20;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        target.TakeDamage(damage);

        // Only heal on the killing blow. Mathf.Min caps healing at maxHP so
        // we never push currentHP above the visible health bar.
        if (target.IsDead())
        {
            int heal = Mathf.RoundToInt(caster.data.maxHP * (healPercent / 100f));
            caster.currentHP = Mathf.Min(caster.data.maxHP, caster.currentHP + heal);
        }
        return damage;
    }
}

/// <summary>
/// Dungeon 2 (40% vampire): pay 3% HP to grant the player armor and weaken
/// the enemy's attack damage for two turns.
/// </summary>
[CreateAssetMenu(fileName = "BloodVision", menuName = "Battle/Abilities/BloodVision")]
public class BloodVisionAbility : Ability
{
    public int armorGain = 5;

    [Tooltip("Enemy attack damage reduced by this magnitude per turn while debuff is active.")]
    public int attackReduction = 4;

    public int debuffTurns = 2;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        caster.giveArmor(armorGain);
        target.activeStatuses.Add(new StatusEffect(StatusType.AttackDebuff, attackReduction, debuffTurns));
        return 0;
    }
}

/// <summary>
/// Dungeon 3 (60% vampire): big single-target damage in exchange for 8% HP.
/// </summary>
[CreateAssetMenu(fileName = "BloodBat", menuName = "Battle/Abilities/BloodBat")]
public class BloodBatAbility : Ability
{
    public int damage = 20;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        target.TakeDamage(damage);
        return damage;
    }
}

/// <summary>
/// Dungeon 4 (80% vampire): pay 5% HP for moderate damage and 3 stacks of
/// permanent escalating bleed (3 → 4 → 5 → … per turn until enemy dies).
/// </summary>
[CreateAssetMenu(fileName = "Claw", menuName = "Battle/Abilities/Claw")]
public class ClawAbility : Ability
{
    public int damage = 7;

    [Tooltip("Initial bleed magnitude. Phase 5's TickStatuses ramps it +1 per turn.")]
    public int bleedAmount = 3;

    [Tooltip("Effectively 'forever' — bleed is permanent in the design doc.")]
    public int bleedTurns = 99;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        PayHpCost(caster);
        target.TakeDamage(damage);
        target.activeStatuses.Add(new StatusEffect(StatusType.Bleed, bleedAmount, bleedTurns));
        return damage;
    }
}

/// <summary>
/// Dungeon 5 (optional): deals 4 damage per 10% HP the caster has, max 40.
/// Caster also takes 10% maxHP self-damage. The "low HP / high payoff"
/// inverted-incentive ability.
/// </summary>
[CreateAssetMenu(fileName = "EssenceBeam", menuName = "Battle/Abilities/EssenceBeam")]
public class EssenceBeamAbility : Ability
{
    public int damagePerTenPercent = 4;
    public int maxDamage           = 40;

    public override int Execute(PlayerBattler caster, Enemy target)
    {
        // Pay the cost FIRST so the damage calc reflects the post-cost HP.
        // Otherwise full-HP players would always hit max damage AND not bleed —
        // a meaningful in-fight choice becomes a no-brainer.
        PayHpCost(caster);

        // currentHP / maxHP is a float ratio (0.0 to 1.0). Multiply by 10 to
        // get "tenths" (a percentage in tens). Each tenth = damagePerTenPercent.
        float pctOfMaxHp     = (float)caster.currentHP / caster.data.maxHP;
        int tenthsRemaining  = Mathf.FloorToInt(pctOfMaxHp * 10f);
        int damage           = Mathf.Min(maxDamage, tenthsRemaining * damagePerTenPercent);

        target.TakeDamage(damage);
        return damage;
    }
}