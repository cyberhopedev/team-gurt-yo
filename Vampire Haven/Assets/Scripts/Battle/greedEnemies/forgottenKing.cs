using UnityEngine;
using UnityEngine.SceneManagement;

public class forgottenKing : Enemy
{
    private string[] forgottenKingATK = { "command", "curse", "regret" };
    //public string enemyID;
    //public int maxHP = 40;
    //public int currentHP;
    //public int attackPower = 7;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;


    public override void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

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

        goldToDps = false;

        int damage = 0;

        int len = forgottenKingATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(forgottenKingATK[randIdx]);

        if (randomATK.Equals("command")){
            damage = command();
        }

        if (randomATK.Equals("curse"))
        {
            damage = curse();
        }

        if (randomATK.Equals("regret"))
        {
            damage = regret();
        }

        return damage;

    }

    public int command()
    {
        return attackPower;
    }

    public int curse()
    {
        dmgDebuff -= 3;
        return 0;
    }

    public int regret()
    {
        intentFlee = true;
        return 0;
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