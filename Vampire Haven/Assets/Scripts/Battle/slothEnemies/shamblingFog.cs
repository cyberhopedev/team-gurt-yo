using UnityEngine;
using UnityEngine.SceneManagement;

public class shamblingFog : MonoBehaviour
{
    private string[] shamblingFogATK = { "blindingHaze", "condense", "rest" };
    public string enemyID;
    public int maxHP = 60;
    public int currentHP;
    public int attackPower = 0;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    public int poison = 0;

    public int armor = 0;

    public int hazeTimer = 4;
    public int hazeTriggers = 0;

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

        if (armor > 0)
        {
            //armor should only last one turn if any is there
            armor = 0;
        }

        //give back strength on death
        if (currentHP <= 0)
        {
            dmgBuff += hazeTriggers * 3;
        }
    }

    public int chooseAttack()
    {

        int damage = 0;

        poison += 1;

        damage += poison;

        int len = shamblingFogATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(shamblingFogATK[randIdx]);

        if (randomATK.Equals("blindingHaze")){
            damage = blindingHaze();
        }

        if (randomATK.Equals("condense"))
        {
            damage = condense();
        }

        if (randomATK.Equals("rest"))
        {
            damage = rest();
        }

        return damage;

    }

    public int blindingHaze()
    {
        hazeTimer -= 1;

        if(hazeTimer == 0)
        {
            dmgDebuff -= 3;
            hazeTimer = 4;
            hazeTriggers += 1;
        }

        return 0;
    }

    public int condense()
    {
        armor += 12;
        return 0;
    }

    public int rest()
    {
        //rest does nothing
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