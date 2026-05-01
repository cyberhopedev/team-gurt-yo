using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
    public string enemyID;
    public int maxHP;
    public int currentHP;
    public int attackPower;

    public int armorDebuff = 0;
    public int armorBuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgBuff = 0;
    public int dmgDebuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;


    // List of active status effects
    public List<StatusEffect> activeStatuses = new List<StatusEffect>();

    protected virtual void Start()
    {
        // Get rid of the enemy in battle
        if (EnemyTracker.defeatedEnemies.Contains(enemyID))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Awake()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    public abstract int chooseAttack();
    
    public virtual bool IsDead()
    {
        return currentHP <= 0;
    }

    public virtual int armorReduction()
    {
        return armorDebuff;
    }

    public virtual int goldGive()
    {
        return goldBuff;
    }

    public virtual int goldSteal()
    {
        return goldDebuff;
    }

    public virtual int damageDebuff()
    {
        return dmgDebuff;
    }

    public virtual bool attemptFlee()
    {
        return intentFlee;
    }

    public virtual bool goldDamage()
    {
        return goldToDps;
    }

    public virtual int armorAddition()
    {
        return armorBuff;
    }

    public virtual int damageBuff()
    {
        return dmgBuff;
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