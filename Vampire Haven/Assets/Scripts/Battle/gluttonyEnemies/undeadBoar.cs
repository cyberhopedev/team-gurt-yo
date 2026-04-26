using UnityEngine;
using UnityEngine.SceneManagement;

public class undeadBoar : Enemy
{
    private string[] undeadBoarATK = { "foulStench", "charge", "protectiveSkin" };
    //public string enemyID;
    //public int maxHP = 60;
    //public int currentHP;
    //public int attackPower = 12;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;

    public bool foulStenchActive = false;

    public int armor = 0;

    public override void TakeDamage(int dmg)
    {
        //if foul stench active, attack misses
        if (foulStenchActive)
        {
            dmg = 0;
            foulStenchActive = false;
        }

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

    public override int chooseAttack()
    {
        //reset debuff if needed

        int damage = 0;

        int len = undeadBoarATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(undeadBoarATK[randIdx]);

        if (randomATK.Equals("foulStench")){
            damage = foulStench();
        }

        if (randomATK.Equals("charge"))
        {
            damage = charge();
        }

        if (randomATK.Equals("protectiveSkin"))
        {
            damage = protectiveSkin();
        }

        return damage;

    }

    public int foulStench()
    {
        foulStenchActive = true;
        return 0;
    }

    public int charge()
    {
        return attackPower;
    }

    public int protectiveSkin()
    {
        armor += 8;
        return 0;
    }
}