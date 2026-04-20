using UnityEngine;

/// <summary> 
/// Holds the data between the overworld and battle scene transition
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "Battle/PlayerData")]
public class PlayerData : ScriptableObject
{
    public static Vector3 lastPosition;
    public static bool hasSavedPosition = false;
    public int maxHP = 100;
    public int attackDamage = 10;
    public int armor = 0;
    public int gold = 0;
    public int speedStat = 5;
}