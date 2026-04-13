using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 15;
    public int currentHP;
    public int attackPower = 10;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }
}