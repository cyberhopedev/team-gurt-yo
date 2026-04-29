using UnityEngine;
using UnityEngine.SceneManagement;

public class natureAnamaly : Enemy
{
    private string[] natureAnamalyATK = { "naturesWrath", "zap" };
    //public string enemyID;
    //public int maxHP = 60;
    //public int currentHP;
    //public int attackPower = 11;

    //public int armorDebuff = 0;
    //public int goldBuff = 0;
    //public int goldDebuff = 0;
    //public int dmgDebuff = 0;
    //public int armorBuff = 0;
    //public int dmgBuff = 0;
    //public bool intentFlee = false;
    //public bool goldToDps = false;

    //shiftingForms passive, rotate through them per turn
    public enum shiftingForms { dmgBonus, armorBonus, healthBonus }
    shiftingForms form = shiftingForms.dmgBonus;

    public int armor = 0;
    public int armorDmg = 0;

    public int playerArmorTaken = 0;

    public override void TakeDamage(int dmg)
    {
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

        if (currentHP <= 0)
        {
            armorBuff = playerArmorTaken;
        }

    }

    public override int chooseAttack()
    {
        
        int damage = 0;

        //trigger passive and set to next one for next turn
        switch(form)
        {
            case shiftingForms.dmgBonus:
                damage += 5;
                form = shiftingForms.armorBonus;
                break;

            case shiftingForms.armorBonus:
                armor += 6;
                form = shiftingForms.healthBonus;
                break;

            case shiftingForms.healthBonus:
                currentHP += 5;
                form = shiftingForms.dmgBonus;
                break;

        }

        int len = natureAnamalyATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(natureAnamalyATK[randIdx]);

        if (randomATK.Equals("naturesWrath")){
            damage = naturesWrath();
        }

        if (randomATK.Equals("zap"))
        {
            damage = zap();
        }

        return damage;

    }

    public int naturesWrath()
    {
        armorDebuff -= 4;
        return 0;
    }

    public int zap()
    {
        return attackPower;
    }

    public override int armorReduction()
    {
        return armorDebuff;
    }

    public override int armorAddition()
    {
        return armorBuff;
    }
}