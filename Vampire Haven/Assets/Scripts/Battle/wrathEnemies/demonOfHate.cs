using UnityEngine;
using UnityEngine.SceneManagement;

public class demonOfHate : MonoBehaviour
{
    private string[] demonOfHateATK = { "flurryOfSwipes", "claw", "flight", "screech" };
    public string enemyID;
    public int maxHP = 70;
    public int currentHP;
    public int attackPower = 14;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    public bool bleed = false;
    public int bleedTimer = 3;

    public int armor = 0;
    public int armorDmg = 0;

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
            if (armor - dmg < 0)
            {
                armor = 0;
            }
            else
            {
                //else set armor hp to new value after dmg
                armor -= armorDmg;
            }
        }

        dmg -= armor;
        currentHP -= dmg;
    }

    public int chooseAttack()
    {

        //dont give dmg again
        if (dmgBuff > 0)
        {
            dmgBuff = 0;
        }

        //reset debuff if needed
        if (dmgDebuff < 0)
        {
            dmgDebuff = 0;
            dmgBuff += 7;
        }

        int damage = 0;

        if (bleed)
        {
            bleedTimer -= 1;

            if(bleedTimer < 0)
            {
                bleedTimer = 3;
                bleed = false;
            } else
            {
                //dmg does 1 first turn, then 1 more each time to 3
                switch (bleedTimer)
                {
                    case 2:
                        damage += 1; 
                        break;

                    case 1:
                        damage += 2;
                        break;

                    case 0:
                        damage += 3;
                        break;
                }
            }
        }

        int len = demonOfHateATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(demonOfHateATK[randIdx]);

        if (randomATK.Equals("flurryOfSwipes")){
            damage = flurryOfSwipes();
        }

        if (randomATK.Equals("claw"))
        {
            damage = claw();
        }

        if (randomATK.Equals("flight"))
        {
            damage = flight();
        }

        if (randomATK.Equals("screech"))
        {
            damage = screech();
        }

        return damage;

    }

    public int flurryOfSwipes()
    {
        //if already bleeding, reset timer
        if (bleed)
        {
            bleedTimer = 3;
            return 15;
        }

        bleed = true;
        return 15;
    }

    public int claw()
    {
        return attackPower;
    }

    public int flight()
    {
        armor += 5;
        return 0;
    }

    public int screech()
    {
        dmgDebuff -= 7;
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