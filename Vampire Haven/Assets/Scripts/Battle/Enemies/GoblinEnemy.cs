using UnityEngine;

public class GoblinEnemy : MonoBehaviour
{
    public int maxHP = 15;
    public int currentHP;
    public int attackPower = 3;

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