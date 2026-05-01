using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles saving and loading the game state, including the player's position and the current map boundary. 
/// The save data is stored in a JSON file in the persistent data path.
/// If no save file exists when loading, a new one will be created with the current state.
/// </summary>
public class SaveManager : MonoBehaviour
{   
    // Singleton instance of the SaveManager
    public static SaveManager Instance { get; private set; }
    
    // Player data
    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;
    // The scene the player starts in
    [Header("Scene")]
    [SerializeField] private string startingSceneName = "AlphaScene";
    // Remembers which slot was used most recently so LoadLastSave() knows where to look after the player dies
    private int _activeSlot = 0;

    /// <summary>
    /// Ensures that the SaveManager is a singleton instance and persists across scenes. 
    /// If another instance is created, it will be destroyed.
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void NewGame(int slot, string gameName)
    {
        _activeSlot = slot;
        Debug.Log($"Starting new game in slot {slot} as '{gameName}'...");

        // Reset all player stats so a previous run doesn't bleed into this one.
        playerData.ResetToDefaults();

        // Clear defeated enemies so all overworld enemies reappear
        EnemyTracker.defeatedEnemies.Clear();
 
        // Write an initial save file immediately so the slot shows up in LoadSaveUI even before the player manually saves
        SaveData initial = BuildSaveData(slot, gameName, startingSceneName);
        WriteSaveFile(slot, initial);

        // Load starting scene
        SceneManager.LoadScene(startingSceneName);
    }

    /// <summary>
    /// Writes the current game state to the given save slot
    /// </summary>
    public void SaveGame(int slot)
    {
        // SaveData saveData = new SaveData
        // {
        //     playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
        //     inventorySaveData = inventoryController.GetInventoryItems()
        // };

        // File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        _activeSlot = slot;

        string sceneName = SceneManager.GetActiveScene().name;
        SaveData data    = BuildSaveData(slot, $"Save {slot + 1}", sceneName);
 
        WriteSaveFile(slot, data);
        Debug.Log($"Game saved to slot {slot} at {GetSavePath(slot)}");

    }

    /// <summary>
    /// Reads a save file and restores all game state from it, 
    /// called by LoadSaveUI when the player picks a slot.
    /// </summary>
    /// <returns>True if the game is saved, false if no file exists</returns>
    public bool LoadGame(int slot)
    {
        // if (File.Exists(saveLocation))
        // {
        //     SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

        //     GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

        //     inventoryController.SetInventoryItems(saveData.inventorySaveData);

        // }
        // else
        // {
        //     SaveGame();
        // }
        string path = GetSavePath(slot);
        if (!File.Exists(path))
        {
            Debug.Log($"No save file found for slot {slot}.");
            return false;
        }
 
        // Read the JSON string and deserialize it back into a SaveData object
        string   json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
 
        if (data == null)
        {
            Debug.LogError("Save file exists but could not be parsed.");
            return false;
        }
        _activeSlot = slot;
 
        // Restore PlayerData stats
        playerData.currentHP    = data.currentHP;
        playerData.maxHP        = data.maxHP;
        playerData.gold         = data.gold;
        playerData.armor        = data.armor;
        playerData.attackDamage = data.attackDamage;
 
        // Restore player position so PlayerController can teleport there on Awake
        PlayerData.lastPosition     = data.playerPosition;
        PlayerData.hasSavedPosition = true;
 
        // Restore defeated enemies
        EnemyTracker.defeatedEnemies.Clear();
        if (data.defeatedEnemyIDs != null)
        {
            foreach (string id in data.defeatedEnemyIDs)
            {
                EnemyTracker.defeatedEnemies.Add(id);
            }
        }
 
        Debug.Log($"Loaded slot {slot}: HP={data.currentHP} Gold={data.gold} Scene={data.sceneName}");

        // Load the scene that was active when this file was saved
        string targetScene = string.IsNullOrEmpty(data.sceneName) ? startingSceneName : data.sceneName;
        SceneManager.LoadScene(targetScene);
 
        return true;
    }

    /// <summary>
    /// Reloads the most recently used save slot.
    /// Called by BattleManager when the player dies, returns them to their
    /// last save point instead of crashing or looping at the game over screen.
    /// Falls back to the starting scene if no save exists yet.
    /// </summary>
    public void LoadLastSave()
    {
        bool loaded = LoadGame(_activeSlot);
 
        if (!loaded)
        {
            Debug.Log("No save to load — restarting from beginning.");
            // Reset stats to defaults before loading so the player doesn't continue with 0 HP or negative gold
            playerData.currentHP = playerData.maxHP;
            playerData.gold      = 0;
            SceneManager.LoadScene(startingSceneName);
        }
    }


    /// <summary>
    /// Returns the SaveData for a slot WITHOUT loading it into the game.
    /// LoadSaveUI calls this to display the slot's name, timestamp, and HP
    /// in the UI before the player decides whether to load it.
    /// </summary>
    public SaveData PeekSlot(int slot)
    {
        string path = GetSavePath(slot);
        if (!File.Exists(path)) return null;
 
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }


    /// <summary>
    /// Deletes the save file for a given slot, used by LoadSaveUI erase option
    /// </summary>
    public void DeleteSave(int slot)
    {
        string path = GetSavePath(slot);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"Deleted save slot {slot}.");
        }
    }


    /// <summary>
    /// Returns the full path for a given save slot.
    /// </summary>
    private string GetSavePath(int slot)
    {
        return Path.Combine(Application.persistentDataPath, $"save_slot_{slot}.json");
    }
    
    /// <summary>
    /// Builds a SaveData object from the current game state.
    /// Separated from SaveGame() so NewGame() can also call it when writing
    /// the initial save file.
    /// </summary>
    private SaveData BuildSaveData(int slot, string gameName, string sceneName)
    {
        // Collect defeated enemy IDs into a plain List<string>
        List<string> defeated = new List<string>(EnemyTracker.defeatedEnemies);
 
        return new SaveData
        {
            // Player stats
            currentHP    = playerData.currentHP,
            maxHP        = playerData.maxHP,
            gold         = playerData.gold,
            armor        = playerData.armor,
            attackDamage = playerData.attackDamage,
 
            // Position — use the last saved position if available, otherwise use their starting position
            playerPosition = PlayerData.hasSavedPosition ? PlayerData.lastPosition: Vector3.zero,
 
            // Progression
            defeatedEnemyIDs = defeated,
 
            // Metadata shown in LoadSaveUI
            gameName       = gameName,
            saveTimestamp  = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            sceneName      = sceneName,
            locationName   = sceneName,
        };
    }

    /// <summary>
    /// Serializes a SaveData object to JSON and writes it to the slot's file.
    /// </summary>
    private void WriteSaveFile(int slot, SaveData data)
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(GetSavePath(slot), json);
    }
}