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
    [SerializeField] private string startingSceneName = "SampleScene";

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
    public void NewGame(int slot, string gameName)
    {
        Debug.Log("Starting new game for Alpha version...");

        // Load starting scene
        SceneManager.LoadScene(startingSceneName);
    }
}