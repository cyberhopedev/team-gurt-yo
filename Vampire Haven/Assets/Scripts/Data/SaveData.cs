using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Stores data that needs to be saved for player progression, which includes
/// player position, inventory, current abilities, and cleared encounters
/// </summary>
[System.Serializable]
public class SaveData
{
    // Position of the player in the world, used for loading back into the same spot
    public Vector3 playerPosition;

    // Current player stats
    public int currentHP;
    public int maxHP;
    public int gold;
    public int armor;
    public int attackDamage;

    // Progression of the player based on IDs of defeated enemies On load, these are written back
    // into EnemyTracker.defeatedEnemies so those enemies don't show up again in he overworld
    public List<string> defeatedEnemyIDs = new List<string>();

    // Data held within the inventory save data
    public List<InventorySaveData> inventorySaveData;

    // Information needed for LoadSaveUI to display on save preview
    public string gameName;
    public string saveTimestamp;
    public float totalPlayTimeSeconds;
    public string locationName; 

    // Name of the current scene
    public string sceneName;
}

[System.Serializable]
public class InventorySaveData
{
    public int itemID;
    public int slotIndex; //idx of slot where item is placed within our inventory
}