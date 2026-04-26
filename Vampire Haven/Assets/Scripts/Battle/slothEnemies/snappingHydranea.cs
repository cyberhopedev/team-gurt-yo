using UnityEngine;
using UnityEngine.SceneManagement;

public class snappingHydranea : Enemy
{
    private string[] snappingHydraneaATK = { "prepareTrap", "alluringScent", "ensnare" };
    //public string enemyID;
    //public int maxHP = 85;
    //public int currentHP;
    //public int attackPower = 25;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;

    public int armor = 0;

    public override void TakeDamage(int dmg)
    {

        dmg -= armor;
        currentHP -= dmg;

    }

    public override int chooseAttack()
    {
        //reset debuff if needed
       
        if(armor > 0)
        {
            armor = 0;
        }

        //dont give armor again
        if (armorBuff > 0)
        {
            armorBuff = 0;
        }

        //reset debuff if needed
        if (armorDebuff < 0)
        {
            armorDebuff = 0;
            armorBuff += 10;
        }

        int damage = 0;

        int len = snappingHydraneaATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(snappingHydraneaATK[randIdx]);

        if (randomATK.Equals("prepareTrap")){
            damage = prepareTrap();
        }

        if (randomATK.Equals("alluringScent"))
        {
            damage = alluringScent();
        }

        if (randomATK.Equals("ensnare"))
        {
            damage = ensnare();
        }

        return damage;

    }

    public int prepareTrap()
    {
        armor = 20;
        return 0;
    }

    public int alluringScent()
    {
        armor += 10;
        armorDebuff -= 10;
        return 10;
    }

    public int ensnare()
    {
        return attackPower;
    }

    public int armorReduction()
    {
        return armorDebuff;
    }

    public int armorAddition()
    {
        return armorDebuff;
    }

}