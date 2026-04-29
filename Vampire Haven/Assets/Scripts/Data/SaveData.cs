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
    // Health of the player, used for loading back with the same health
    public int currentHP;

    public List<InventorySaveData> inventorySaveData;

    // Information needed for LoadSaveUI
    public string gameName;
    public string saveTimestamp;
    public float totalPlayTimeSeconds;
    public string locationName; 
    public string sceneName;
}

[System.Serializable]
public class InventorySaveData
{
    public int itemID;
    public int slotIndex; //idx of slot where item is placed within our inventory
}