using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Item scriptable object that holds information that we can load
/// into various items
/// </summary>
[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;
    public bool isGold;

    // Stats that can be buffed/changed with items including Weapons, Potions, Armor, and Abilities
    [Header("Stats")]
    public int maxHealth;
    public int currentHealth;
}