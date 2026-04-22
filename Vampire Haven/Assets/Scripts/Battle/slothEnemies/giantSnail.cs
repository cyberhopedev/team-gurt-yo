using UnityEngine;
using UnityEngine.SceneManagement;

public class giantSnail : MonoBehaviour
{
    private string[] giantSnailATK = { "rest", "panicSpray", "slam", "energyBurst" };
    public string enemyID;
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;


    private void Start()
    {
        // Get rid of the enemy in battle
        if (EnemyTracker.defeatedEnemies.Contains(enemyID))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    public int chooseAttack()
    {

        int damage = 0;

        int len = giantSnailATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(giantSnailATK[randIdx]);

        if (randomATK.Equals("rest"))
        {
            damage = rest();
        }

        if (randomATK.Equals("panicSpray"))
        {
            damage = panicSpray();
        }

        if (randomATK.Equals("slam"))
        {
            damage = slam();
        }

        if (randomATK.Equals("energyBurst"))
        {
            damage = energyBurst();
        }

        return damage;

    }

    public int rest()
    {
        return attackPower;
    }

    public int panicSpray()
    {
        return 0;
    }

    public int slam()
    {
        return 0;
    }

    public int energyBurst()
    {
        return 0;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public int armorReduction()
    {
        return armorDebuff;
    }

    public int goldGive()
    {
        return goldBuff;
    }

    public int goldSteal()
    {
        return goldDebuff;
    }

    public int damageDebuff()
    {
        return dmgDebuff;
    }

    public bool attemptFlee()
    {
        return intentFlee;
    }

    public bool goldDamage()
    {
        return goldToDps;
    }

    public int armorAddition()
    {
        return armorDebuff;
    }

    public int damageBuff()
    {
        return dmgDebuff;
    }
}