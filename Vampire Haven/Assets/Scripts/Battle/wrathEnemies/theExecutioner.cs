using UnityEngine;
using UnityEngine.SceneManagement;

public class theExecutioner : Enemy
{
    private string[] theExecutionerATK = { "markedForDeath", "swingAxe", "boomingShout", "execution" };
    //public string enemyID;
    //public int maxHP = 80;
    //public int currentHP;
    //public int attackPower = 16;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;

    public int markedForDeathCounter = 5;

    public override int chooseAttack()
    {
        //reset debuff if needed

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

        if (markedForDeathCounter > 0)
        {
            markedForDeathCounter -= 1;
        }

        if (markedForDeathCounter == 0)
        {
            markedForDeathCounter = 10;
            return 24;
        }

        int len = theExecutionerATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(theExecutionerATK[randIdx]);

        if (randomATK.Equals("swingAxe"))
        {
            damage = swingAxe();
        }

        if (randomATK.Equals("boomingShout"))
        {
            damage = boomingShout();
        }

        if (randomATK.Equals("execution"))
        {
            damage = execution();
        }

        return damage;

    }

    public int swingAxe()
    {
        return attackPower;
    }

    public int boomingShout()
    {
        dmgDebuff -= 7;
        return 0;
    }

    public int execution()
    {
        markedForDeathCounter -= 1;
        return 10;
    }

    public override int damageDebuff()
    {
        return dmgDebuff;
    }

    public override int damageBuff()
    {
        return dmgDebuff;
    }
}