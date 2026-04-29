using UnityEngine;
using UnityEngine.SceneManagement;

public class coinSlime : Enemy
{
    private string[] slimeATK = { "flingGold" };
    //public string enemyID;
    //public int maxHP = 15;
    //public int currentHP;
    //public int attackPower = 3;

    //public int armorDebuff = 0;
    //public int goldBuff = 1;
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
        int damage = 0;

        int len = slimeATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(slimeATK[randIdx]);

        if (randomATK.Equals("flingGold")){
            damage = flingGold();
        }

        return damage;

    }

    public int flingGold()
    {
        return attackPower;
    }

   
    public override int goldGive()
    {
        return goldBuff;
    }

}