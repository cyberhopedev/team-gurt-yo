using UnityEngine;
using UnityEngine.SceneManagement;

public class coinSlime : MonoBehaviour
{
    private string[] slimeATK = { "flingGold" };
    public string enemyID;
    public int maxHP = 15;
    public int currentHP;
    public int attackPower = 3;

    public int armorDebuff = 0;
    public int goldBuff = 1;
    public int goldDebuff = 0;
    public int dmgDebuff = 0;

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
}