using UnityEngine;
using UnityEngine.SceneManagement;

public class theGlob : MonoBehaviour
{
    private string[] theGlobATK = { "regurgitate", "slap" };
    public string enemyID;
    public int maxHP = 35;
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

    public bool poison = false;
    public int poisonTimer = 3;

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
       

        int damage = 0;

        int len = theGlobATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(theGlobATK[randIdx]);

        if (poison)
        {
            poisonTimer -= 1;

            if (poisonTimer < 0)
            {
                poisonTimer = 3;
                poison = false;
            }
            else
            {
                damage += 1;
            }
        }

        if (randomATK.Equals("regurgitate")){
            damage = regurgitate();
        }

        if (randomATK.Equals("slap"))
        {
            damage = slap();
        }

        return damage;

    }

    public int regurgitate()
    {
        //if already poisoned, reset timer
        if (poison)
        {
            poisonTimer = 3;
            return 0;
        }

        poison = true;
        return 6;
    }

    public int slap()
    {
        return attackPower;
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