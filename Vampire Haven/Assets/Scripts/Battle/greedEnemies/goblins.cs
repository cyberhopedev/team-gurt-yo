using UnityEngine;
using UnityEngine.SceneManagement;

public class goblins : MonoBehaviour
{
    private string[] goblinsATK = { "steal", "stab", "flee" };
    public string enemyID;
    public int maxHP = 25;
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

    public int goldStolen = 0;

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
        //reset debuff if needed
        goldDebuff = 0;
        intentFlee = false;

        int damage = 0;

        int len = goblinsATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(goblinsATK[randIdx]);

        if (randomATK.Equals("steal")){
            damage = steal();
        }

        if (randomATK.Equals("stab"))
        {
            damage = stab();
        }

        if (randomATK.Equals("flee"))
        {
            damage = flee();
        }

        return damage;

    }

    public int steal()
    {
        goldDebuff = 5;
        goldStolen += 5;
        return 0;
    }

    public int stab()
    {
        return attackPower;
    }

    public int flee()
    {
        intentFlee = true;
        return 0;
    }

    public bool IsDead()
    {
        if (currentHP < 0)
        {
            goldBuff = goldStolen;
        }
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