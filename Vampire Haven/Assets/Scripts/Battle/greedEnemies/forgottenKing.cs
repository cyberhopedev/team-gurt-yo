using UnityEngine;
using UnityEngine.SceneManagement;

public class forgottenKing : MonoBehaviour
{
    private string[] forgottenKingATK = { "command", "curse", "regret" };
    public string enemyID;
    public int maxHP = 40;
    public int currentHP;
    public int attackPower = 7;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;


    private void Start()
    {
        // Get rid of the enemy in battle
        if (EnemyTracker.defeatedEnemies.Contains(enemyID))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    public int chooseAttack()
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

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public int armorReduction()
    {
        return armorDebuff;
    }

    public int goldGive()
    {
        return goldBuff;
    }

    public int goldSteal()
    {
        return goldDebuff;
    }

    public int damageDebuff()
    {
        return dmgDebuff;
    }

    public bool attemptFlee()
    {
        return intentFlee;
    }

    public bool goldDamage()
    {
        return goldToDps;
    }

    public int armorAddition()
    {
        return armorDebuff;
    }

    public int damageBuff()
    {
        return dmgDebuff;
    }
}