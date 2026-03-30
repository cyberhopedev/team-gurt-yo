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
    // Total amount of save slots available, can be adjusted in inspector
    [SerializeField] private int totalSlots = 3;
    // Assign the player data in inspector
    [SerializeField] private PlayerData playerData;
    // The time when the current session started, used for tracking playtime
    private float _sessionStartTime;
    // For when player doesn't exist yet (load screen)
    private Vector3 _pendingSpawnPosition;
    private bool _hasPendingSpawn = false; 
    // Tracks which game is being played atm
    public int currentSlotIdx;

    /// <summary>
    /// Ensures that the SaveManager is a singleton instance and persists across scenes. 
    /// If another instance is created, it will be destroyed.
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Starts the timer for tracking playtime
        _sessionStartTime = Time.time;
    }

    public void NewGame(int slot, string gameName)
    {
        // Reset player data to defaults
        Debug.Log("playerData is: " + playerData); // if this prints null, it's the inspector
        playerData.ResetHP();
        playerData.knownAbilities = new List<Ability>() { Ability.STRUGGLE }; // reset to default
        
        SaveData saveData = new SaveData
        {
            // TODO: Add saving functionality later
        };

        File.WriteAllText(SlotPath(slot), JsonUtility.ToJson(saveData));
        _sessionStartTime = Time.time;
        currentSlotIdx = slot;
        Debug.Log("New game created in slot " + slot);
    }

    // Helper method for starting a new game
    public int GetFirstEmptySlot()
    {
        for(int i = 0; i < totalSlots; i++)
        {
            if (!SaveSlotExists(i))
            {
                return i;    
            }
        }
        return -1; // No empty slots
    }

    /// <summary>
    /// Saves the current game state to a JSON file. This includes the player's position, health, 
    /// inventory, cleared encounters, and playtime.
    /// </summary>
    public void SaveGame(int saveSlot, string locationName)
    {   
        // TODO: Add saving functionality later
    }

    /// <summary>
    /// Loads the game state from a JSON file. If the file exists, 
    /// it will set the player's position and map boundary to the saved values.
    /// 
    /// returns name of scene to load to
    /// </summary>
    public string LoadGame(int slot)
    {
        // TODO: Add loading game functionality later
    }

    public void SetPendingSpawn(Vector3 position)
    {
        _pendingSpawnPosition = position;
        _hasPendingSpawn = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
}

    // Helper method - used Claude code to get the idea for splitting this code off from
    // LoadGame to fix bug with objects that aren't enabled yet
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Re-enable player controller on scene load
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = true;

            if (_hasPendingSpawn)
            {
                player.transform.position = _pendingSpawnPosition;
                _hasPendingSpawn = false;
            }
        }
        
        // TODO: Refresh any UI classes here after being loaded into a save file
    }

    // Helper property that returns the file path for a given slot index
    private string SlotPath(int slot) => Application.persistentDataPath + "/savefile_" + slot + ".json";
    // Helper property that returns the file path for the current save slot
    public bool SaveSlotExists(int slot) => File.Exists(SlotPath(slot));

    /// <summary>
    /// Helper method that reads the save data from a given slot index. If the file exists, 
    /// it will return a SaveData object deserialized from the JSON file.
    /// </summary>
    /// <param name="slot">The slot index to read from</param>
    /// <returns>The saved game data, or null if no save file exists</returns>
    public SaveData ReadSaveSlot(int slot)
    {
        string path = SlotPath(slot);
        if(File.Exists(path))
        {
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        }
        return null;
    }

    /// <summary>
    /// Helper method that deletes the save file for a given slot index. 
    /// If the file exists, it will be removed from the persistent data path.
    /// </summary>
    /// <param name="saveSlot">The slot index to delete</param>
    public void DeleteSaveSlot(int saveSlot)
    {
        if(File.Exists(SlotPath(saveSlot)))
        {
            File.Delete(SlotPath(saveSlot));
            Debug.Log("Save slot " + saveSlot + " deleted.");
        }
        else
        {
            Debug.Log("No save file found in slot " + saveSlot + " to delete.");
        }
    }

    /// <summary>
    /// Helper method that retrieves the save data for all available slots. 
    /// (For load menu UI)
    /// </summary>
    /// <returns>Array of save data per slot, where a null entry = empty slot</returns>
    public SaveData[] GetAllSlots()
    {
        SaveData[] allSlots = new SaveData[totalSlots]; 
        for(int i = 0; i < totalSlots; i++)
        {
            allSlots[i] = ReadSaveSlot(i);
        }
        return allSlots;
    }
}