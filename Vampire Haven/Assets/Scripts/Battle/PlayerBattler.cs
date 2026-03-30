using UnityEngine;

public class PlayerBattler : MonoBehaviour
{
    public PlayerData data;
    public int currentHP;

    private void Awake()
    {
        currentHP = data.maxHP;
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