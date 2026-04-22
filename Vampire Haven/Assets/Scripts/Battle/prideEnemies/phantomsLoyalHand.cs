using UnityEngine;
using UnityEngine.SceneManagement;

public class phantomsLoyalHand : MonoBehaviour
{
    private string[] phantomsLoyalHandATK = { "greatOnesBlessing", "inPlainSight", "decay" };
    public string enemyID;
    public int maxHP = 45;
    public int currentHP;
    public int attackPower = 7;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    public bool loyalToTheEnd = false;
    public bool loyalToTheEndUsed = false;

    public int armor = 0;

    public bool decaying = false;



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
        if (armor > 0)
        {
            int armorDmg = armor - dmg;

            //if more than armor hp set to 0
            if(armor - dmg < 0)
            {
                armor = 0;
            } else
            {
                //else set armor hp to new value after dmg
                armor -= armorDmg;
            }
        }

        dmg -= armor;

        currentHP -= dmg;

        if(currentHP < (maxHP / 2) && !loyalToTheEndUsed)
        {
            loyalToTheEndUsed = true;
            loyalToTheEnd = true;
        }
    }

    public int chooseAttack()
    {
        //reset debuff if needed
       

        int damage = 0;

        if (loyalToTheEnd)
        {
            //two attacks
            damage += greatOnesBlessing();
            damage += greatOnesBlessing();

            //set armor
            armor = 6;

            loyalToTheEndUsed = true;
            loyalToTheEnd = false;

            if (decaying)
            {
                damage += 1;
            }

            return damage;

        }

        int len = phantomsLoyalHandATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(phantomsLoyalHandATK[randIdx]);

        if (randomATK.Equals("greatOnesBlessing")){
            damage = greatOnesBlessing();
        }

        if (randomATK.Equals("inPlainSight"))
        {
            damage = inPlainSight();
        }

        if (randomATK.Equals("decay"))
        {
            damage = decay();
        }

        if (decaying)
        {
            damage += 1;
        }

        return damage;

    }

    public int greatOnesBlessing()
    {
        return attackPower;
    }

    public int inPlainSight()
    {
        armor += 5;
        return 0;
    }

    public int decay()
    {
        decaying = true;
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