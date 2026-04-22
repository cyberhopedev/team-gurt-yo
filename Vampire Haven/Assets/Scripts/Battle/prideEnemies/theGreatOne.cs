using UnityEngine;
using UnityEngine.SceneManagement;

public class theGreatOne : MonoBehaviour
{
    private string[] theGreatOneATK = { "mockery", "darkBlast", "shroud", "darkDecay" };
    public string enemyID;
    public int maxHP = 60;
    public int currentHP;
    public int attackPower = 9;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    public bool shroudUsed = false;
    public int shroudTurns = 2;

    public bool darkDecayActive = false;
    public int darkDecayTurns = 2;

    public int armor = 0;
    public int armorReductionNum = 0;

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
        dmg -= armor;
        currentHP -= dmg;

        //impenetrable ego passive, +1 armor per dmg taken
        armor += 1;
    }

    public int chooseAttack()
    {
        armorDebuff = 0;

        //reset armor buff so its only given once
        if (armorBuff > 0)
        {
            armorBuff = 0;
        }

        if(shroudTurns == 1)
        {
            armor += 6;
            shroudTurns -= 1;
        }

        if (darkDecayTurns == 1)
        {
            armorDebuff -= 2;
            armorReductionNum += 2;

            darkDecayTurns -= 1;

        }

        if (darkDecayTurns == 0 && darkDecayActive)
        {
             //reset darkDecay
            darkDecayActive = false;
            darkDecayTurns = 2;
            armorBuff += 5;

        }

        int damage = 0;

        int len = theGreatOneATK.Length;

        ShroudReroll:

            int randIdx = UnityEngine.Random.Range(0, len);

            string randomATK = string.Copy(theGreatOneATK[randIdx]);

            if (randomATK.Equals("mockery")){
                damage = mockery();
            }

            if (randomATK.Equals("darkBlast"))
            {
                damage = darkBlast();
            }

            if (randomATK.Equals("shroud"))
            {
                //one time use, so reroll for attack
                if (shroudUsed)
                {
                    goto ShroudReroll;
                }
                damage = shroud();
            }

            if (randomATK.Equals("darkDecay"))
            {
                damage = darkDecay();
            }

        return damage;

    }

    public int mockery()
    {
        armorDebuff -= 5;
        armorReductionNum += 5;
        return 0;
    }

    public int darkBlast()
    {
        return attackPower;
    }

    public int shroud()
    {
        shroudUsed = true;
        armor += 8;
        shroudTurns -= 1;
        return 0;
    }

    public int darkDecay()
    {
        darkDecayActive = true;
        armorDebuff -= 3;
        armorReductionNum += 3;

        darkDecayTurns -= 1;
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