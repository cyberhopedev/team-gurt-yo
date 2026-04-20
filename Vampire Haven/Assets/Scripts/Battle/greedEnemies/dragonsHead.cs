using UnityEngine;
using UnityEngine.SceneManagement;

public class dragonsHead : MonoBehaviour
{
    private string[] dragonsHeadATK = { "spiritBreath", "intimidate", "enrage", "bite" };
    public string enemyID;
    public int maxHP = 50;
    public int currentHP;
    public int attackPower = 8;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    private bool enraged = false;
    private bool intimidateBool = false;

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

        //if enraged then two attacks debuff if needed
        if (enraged)
        {
            int randomEnragedATK = UnityEngine.Random.Range(0, 2);

            if (randomEnragedATK == 0)
            {
                damage = spiritBreath();
            } else
            {
                damage = bite();
            }

            randomEnragedATK = UnityEngine.Random.Range(0, 2);

            if (randomEnragedATK == 0)
            {
                damage += spiritBreath();
            } else 
            {       
                damage += bite();
            }

            //reset to false
            enraged = false;

            if (intimidateBool)
            {
                int extraDmg = damage / 2;
                damage += extraDmg;
                intimidateBool = false;
            }

            return damage;

        }

        int len = dragonsHeadATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(dragonsHeadATK[randIdx]);

        if (randomATK.Equals("spiritBreath")){
            damage = spiritBreath();
        }

        if (randomATK.Equals("intimidate"))
        {
            damage = intimidate();
        }

        if (randomATK.Equals("enrage"))
        {
            damage = enrage();
        }

        if (randomATK.Equals("bite"))
        {
            damage = bite();
        }

        if (intimidateBool)
        {

        }

        return damage;

    }

    public int spiritBreath()
    {
        return 10;
    }

    public int intimidate()
    {
        intimidateBool = true;
        return 0;
    }

    public int enrage()
    {
        enraged = true;
        return 0;
    }

    public int bite()
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
}