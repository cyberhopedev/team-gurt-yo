using UnityEngine;
using System.Collections.Generic;

public class PlayerBattler : MonoBehaviour
{
    public PlayerData data;
    public int currentHP;
    public int armor;
    public int gold;
    public int playerDmg;
    // List of active status effects
    public List<StatusEffect> activeStatuses = new List<StatusEffect>();

    private void Awake()
    {
        currentHP = data.maxHP;
        armor = data.armor;
        gold = data.gold;
        playerDmg = data.attackDamage;
    }

    public void TakeDamage(int dmg)
    {
        // Armor absorbs damage first, THEN take damage
        int absorbed = Mathf.Min(armor, dmg);
        armor -= absorbed;
        dmg -= absorbed;
        currentHP = Mathf.Max(0, currentHP - dmg);
    }

    public void giveDamage(int addDmg)
    {
        playerDmg += addDmg;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public void loseArmor(int armRed)
    {
        armor -= armRed;
    }

    public void giveArmor(int addArmor)
    {
        armor += addArmor;
    }

    public void loseGold(int goldAmnt)
    {
        gold -= goldAmnt;
    }

    public void gainGold(int goldAmnt)
    {
        gold += goldAmnt;
    }

    public void loseDamage(int dmgRed)
    {
        playerDmg -= dmgRed;
    }

    /// <summary>
    /// Adds XP to the player and levels them up if they cross the threshold.
    /// Called by BattleManager after a victory.
    /// </summary>
    public void AwardXP(int amount)
    {
        data.xp += amount;

        // While instead of if so a single huge XP grant can level twice.
        while (data.xp >= data.level * 100)
        {
            data.xp -= data.level * 100;
            data.level++;

            // Stat growth on level up. Tweak per class once class-specific
            // growth is implemented (Tank gets more HP, Rogue more attack, etc.)
            data.maxHP       += 5;
            data.attackDamage += 1;
            data.currentHP    = data.maxHP;  // full heal on level up — common JRPG convention

            Debug.Log($"Level up! Now level {data.level}");
        }
    }

    /// <summary>
    /// Apply per-turn status effects (poison ticks, bleed ramp, etc.)
    /// Call this at the start of each unit's turn from BattleManager.
    /// </summary>
    public void TickStatuses()
    {
        // Iterate backwards so we can RemoveAt safely — removing from the front
        // shifts every later index down by one and breaks a forward loop.
        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            StatusEffect s = activeStatuses[i];
            switch (s.type)
            {
                case StatusType.Poison: TakeDamage(s.magnitude); break;
                case StatusType.Bleed:  TakeDamage(s.magnitude); s.magnitude++; break; // bleed escalates
                // Stunned, Marked, Shielded are checked elsewhere — no per-turn dmg
            }
            s.turnsRemaining--;
            if (s.turnsRemaining <= 0) activeStatuses.RemoveAt(i);
        }
    }
}