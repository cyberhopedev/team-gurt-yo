using UnityEngine;

public class PlayerBattler : MonoBehaviour
{
    public PlayerData data;
    public int currentHP;
    public int armor;
    public int gold;
    public int playerDmg;

    private void Awake()
    {
        currentHP = data.maxHP;
        armor = data.armor;
        gold = data.gold;
        playerDmg = data.attackDamage;
    }

    public void TakeDamage(int dmg)
    {
        currentHP += armor -= dmg;
    }

    public void giveDamage(int addDmg)
    {
        playerDmg += addDmg;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public void loseArmor(int armRed)
    {
        armor -= armRed;
    }

    public void giveArmor(int addArmor)
    {
        armor += addArmor;
    }

    public void loseGold(int goldAmnt)
    {
        gold -= goldAmnt;
    }

    public void gainGold(int goldAmnt)
    {
        gold += goldAmnt;
    }

    public void loseDamage(int dmgRed)
    {
        playerDmg -= dmgRed;
    }
}