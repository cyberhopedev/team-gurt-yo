using UnityEngine;
using UnityEngine.SceneManagement;

public class vengefulSpirit : MonoBehaviour
{
    private string[] vengefulSpiritATK = { "piercingGaze", "perfectImage", "reflection", "anger" };
    public string enemyID;
    public int maxHP = 55;
    public int currentHP;
    public int attackPower = 9;

    public int armorDebuff = 0;
    public int goldBuff = 0;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;
    public int armorBuff = 0;
    public int dmgBuff = 0;
    public bool intentFlee = false;
    public bool goldToDps = false;

    //anger
    public int angerExtraDmg = 0;

    //perfectImg
    public int lastAmntDmgTaken = 0;

    //reflection
    public bool reflectionActive = false;

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
        //reflection
        if (reflectionActive)
        {
            dmg = 0;
            reflectionActive = false;
        }

        //perfectImg (should come after reflection dmg change)
        lastAmntDmgTaken = dmg;

        currentHP -= dmg;

    }

    public int chooseAttack()
    {
        //reset debuff if needed
       

        int damage = 0;

        int len = vengefulSpiritATK.Length;

        int randIdx = UnityEngine.Random.Range(0, len);

        string randomATK = string.Copy(vengefulSpiritATK[randIdx]);

        if (randomATK.Equals("piercingGaze")){
            damage = piercingGaze();
        }

        if (randomATK.Equals("perfectImage"))
        {
            damage = perfectImage();
        }

        if (randomATK.Equals("reflection"))
        {
            damage = reflection();
        }

        if (randomATK.Equals("anger"))
        {
            damage = anger();
        }

        return damage;

    }

    public int piercingGaze()
    {
        return attackPower + angerExtraDmg;
    }

    public int perfectImage()
    {
        return lastAmntDmgTaken;
    }

    public int reflection()
    {
        reflectionActive = true;
        return 0;
    }

    public int anger()
    {
        angerExtraDmg += 3;
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