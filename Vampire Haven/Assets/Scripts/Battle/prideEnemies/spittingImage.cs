using UnityEngine;
using UnityEngine.SceneManagement;

public class spittingImage : Enemy
{
    private string[] spittingImageATK = { "punch", "guard", "headbutt" };
    //public string enemyID;
    //public int maxHP = 35;
    //public int currentHP;
    //public int attackPower = 6;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;

    public bool guarding = false;

    public int headbuttDmg = 6;

    public override void TakeDamage(int dmg)
    {
        if (guarding)
        {
            //if guarding block 5 dmg
            dmg -= 5;

            if(dmg < 0)
            {
                dmg = 0;
            }

            guarding = false;
            
        }
        currentHP -= dmg;
    }

    public override int chooseAttack()
    {
        //reset debuff if needed

        int damage = 0;

        int len = spittingImageATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(spittingImageATK[randIdx]);

        if (randomATK.Equals("punch")){
            damage = punch();
        }

        if (randomATK.Equals("guard"))
        {
            damage = guard();
        }

        if (randomATK.Equals("headbutt"))
        {
            damage = headbutt();
        }

        return damage;

    }

    public int punch()
    {
        return attackPower;
    }

    public int guard()
    {
        guarding = true;
        return 0;
    }

    public int headbutt()
    {
        headbuttDmg += 1;
        return headbuttDmg;
    }

    
}