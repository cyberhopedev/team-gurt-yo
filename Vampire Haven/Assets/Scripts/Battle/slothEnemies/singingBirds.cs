using UnityEngine;
using UnityEngine.SceneManagement;

public class singingBirds : MonoBehaviour
{
    private string[] singingBirdsATK = { "songOfRelaxation", "peck", "hibernate", "rest" };
    public string enemyID;
    public int maxHP = 55;
    public int currentHP;
    public int attackPower = 5;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    public int peckStrength = 0;

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
        //dont give armor again
        if (armorBuff > 0)
        {
            armorBuff = 0;
        }

        //reset debuff if needed
        if (armorDebuff < 0)
        {
            armorDebuff = 0;
            armorBuff += 999;
        }

        int damage = 0;

        int len = singingBirdsATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(singingBirdsATK[randIdx]);

        if (randomATK.Equals("songOfRelaxation")){
            damage = songOfRelaxation();
        }

        if (randomATK.Equals("peck"))
        {
            damage = peck();
        }

        if (randomATK.Equals("hibernate"))
        {
            damage = hibernate();
        }

        if (randomATK.Equals("rest"))
        {
            damage = rest();
        }

        return damage;

    }

    public int songOfRelaxation()
    {
        //no armor at all next turn
        armorDebuff -= 999;
        return 0;
    }

    public int peck()
    {
        return attackPower;
    }

    public int hibernate()
    {
        attackPower += 5;
        return 0;
    }

    public int rest()
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

    public int armorAddition()
    {
        return armorBuff;
    }

    public int goldGive()
    {
        return goldBuff;
    }

    public int goldSteal()
    {
        return goldDebuff;
    }

    public int damageBuff()
    {
        return dmgBuff;
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

}