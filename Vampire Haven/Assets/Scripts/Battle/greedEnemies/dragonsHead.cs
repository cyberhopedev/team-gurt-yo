using UnityEngine;
using UnityEngine.SceneManagement;

public class dragonsHead : Enemy
{
    private string[] dragonsHeadATK = { "spiritBreath", "intimidate", "enrage", "bite" };
    //public string enemyID;
    //public int maxHP = 50;
    //public int currentHP;
    //public int attackPower = 8;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;

    private bool enraged = false;
    private bool intimidateBool = false;

    public override int chooseAttack()
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
            int extraDmg = damage / 2;
            damage += extraDmg;
            intimidateBool = false;
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

}