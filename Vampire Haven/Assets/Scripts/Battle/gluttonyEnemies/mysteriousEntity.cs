using UnityEngine;
using UnityEngine.SceneManagement;

public class mysteriousEntity : MonoBehaviour
{
    private string[] mysteriousEntityATK = { "devour", "sapEnergy", "energizedPulse" };
    public string enemyID;
    public int maxHP = 70;
    public int currentHP;
    public int attackPower = 11;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    public int armorAmtDevoured = 0;
    public int playerDmgReduced = 0;

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
            //give player back dmg
            armorBuff = armorAmtDevoured;
        }

        int damage = 0;

        int len = mysteriousEntityATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(mysteriousEntityATK[randIdx]);

        if (randomATK.Equals("devour")){
            damage = devour();
        }

        if (randomATK.Equals("sapEnergy"))
        {
            damage = sapEnergy();
        }

        if (randomATK.Equals("energizedPulse"))
        {
            damage = energizedPulse();
        }

        return damage;

    }

    public int devour()
    {
        armorDebuff -= 5;
        dmgDebuff -= 5;
        return 0;
    }

    public int sapEnergy()
    {
        dmgDebuff -= 1;
        playerDmgReduced += 1;
        return 0;
    }

    public int energizedPulse()
    {
        return attackPower;
    }

    public bool IsDead()
    {
        //give back dmg to player once dead
        if(currentHP <= 0)
        {
            dmgBuff = playerDmgReduced;
        }
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