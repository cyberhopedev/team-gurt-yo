using UnityEngine;
using UnityEngine.SceneManagement;

public class goblins : Enemy
{
    private string[] goblinsATK = { "steal", "stab", "flee" };

    public int goldStolen = 0;

    public override void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    public override int chooseAttack()
    {
        //reset debuff if needed
        goldDebuff = 0;
        intentFlee = false;

        int damage = 0;

        int len = goblinsATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(goblinsATK[randIdx]);

        if (randomATK.Equals("steal")){
            damage = steal();
        }

        if (randomATK.Equals("stab"))
        {
            damage = stab();
        }

        if (randomATK.Equals("flee"))
        {
            damage = flee();
        }

        return damage;

    }

    public int steal()
    {
        goldDebuff = 5;
        goldStolen += 5;
        return 0;
    }

    public int stab()
    {
        return attackPower;
    }

    public int flee()
    {
        intentFlee = true;
        return 0;
    }

    public override bool IsDead()
    {
        if (currentHP < 0)
        {
            goldBuff = goldStolen;
        }
        return currentHP <= 0;
    }

    public override int goldGive()
    {
        return goldBuff;
    }

    public override int goldSteal()
    {
        return goldDebuff;
    }

  
}