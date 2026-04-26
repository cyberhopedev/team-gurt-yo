using UnityEngine;
using UnityEngine.SceneManagement;

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


    //will have more status effects as more enemies implemented

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
        return armorDebuff;
    }

    public virtual int damageBuff()
    {
        return dmgDebuff;
    }
}