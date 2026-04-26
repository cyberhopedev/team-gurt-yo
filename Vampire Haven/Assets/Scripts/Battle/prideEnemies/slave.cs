using UnityEngine;
using UnityEngine.SceneManagement;

public class slave : Enemy
{
    private string[] slaveATK = { "slice", "infect", "pity" };
    //public string enemyID;
    //public int maxHP = 30;
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

    public bool poison = false;
    public int poisonTimer = 3;

    public bool pityBool = false;

    public override int chooseAttack()
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
            //give player back dmg
            dmgBuff = 5;
        }

        int damage = 0;

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

        int len = slaveATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(slaveATK[randIdx]);

        if (randomATK.Equals("slice")){
            damage = slice();
        }

        if (randomATK.Equals("infect"))
        {
            damage = infect();
        }

        if (randomATK.Equals("pity"))
        {
            damage = pity();
        }

        return damage;

    }

    public int slice()
    {
        return attackPower;
    }

    public int infect()
    {
        //if already poisoned, reset timer
        if (poison)
        {
            poisonTimer = 3;
            return 0;
        }

        poison = true;
        return 0;
    }

    public int pity()
    {
        dmgDebuff -= 5;
        return 0;
    }

    public int damageDebuff()
    {
        return dmgDebuff;
    }

    public int damageBuff()
    {
        return dmgDebuff;
    }

}