using UnityEngine;
using UnityEngine.SceneManagement;

public class serpant : MonoBehaviour
{
    private string[] serpantATK = { "strike", "consume", "tailWhip" };
    public string enemyID;
    public int maxHP = 40;
    public int currentHP;
    public int attackPower = 8;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    public bool poison = false;
    public int poisonTimer = 3;

    public int consumeCounter = 3;


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
            armorBuff = 10;
        }

        if (poison)
        {
            poisonTimer -= 1;

            if(poisonTimer < 0)
            {
                poisonTimer = 3;
                poison = false;
            } else
            {
                damage += 1;
            }
        }

        ConsumeReroll:

            int len = serpantATK.Length;

            int randIdx = UnityEngine.Random.Range(0, len);

            string randomATK = string.Copy(serpantATK[randIdx]);

            if (randomATK.Equals("strike")){
                damage = strike();
            }

            if (randomATK.Equals("consume"))
            {

                //one time use, so reroll for attack
                if (consumeCounter == 0)
                {
                    goto ConsumeReroll;
                }
                damage = consume();
            }

            if (randomATK.Equals("tailWhip"))
            {
                damage = tailWhip();
            }

            return damage;

    }

    public int strike()
    {
        //if already poisoned, reset timer
        if (poison)
        {
            poisonTimer = 3;
            return 0;
        }

        poison = true;
        return 4;
    }

    public int consume()
    {
        currentHP += 5;
        consumeCounter -= 1;
        return attackPower;
    }

    public int tailWhip()
    {
        armorDebuff -= 10;
        return 14;
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