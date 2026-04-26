using UnityEngine;
using UnityEngine.SceneManagement;

public class giantSnail : Enemy
{
    private string[] giantSnailATK = { "rest", "panicSpray", "slam", "energyBurst" };
    //public string enemyID;
    //public int maxHP = 100;
    //public int currentHP;
    //public int attackPower = 20;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;

    int armor = 0;

    public bool attackChosen = false;

    public override void TakeDamage(int dmg)
    {
        dmg -= armor;
        currentHP -= dmg;
        armor = 0;
    }

    public override int chooseAttack()
    {

        int damage = 0;

        int len = giantSnailATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(giantSnailATK[randIdx]);

        if (randomATK.Equals("rest"))
        {
            damage = rest();
        }

        if (randomATK.Equals("panicSpray"))
        {
            damage = panicSpray();
        }

        if (randomATK.Equals("slam"))
        {
            damage = slam();
            attackChosen = true;
        }

        if (randomATK.Equals("energyBurst"))
        {
            damage = energyBurst();
        }

        return damage;

    }

    public int rest()
    {
        return currentHP += 15;
    }

    public int panicSpray()
    {
        armor += 15; 
        return 0;
    }

    public int slam()
    {
        return attackPower;
    }

    public int energyBurst()
    {
        armor += 10;
        return 10;
    }
}