using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles user interactions for starting a new game, continuing a game,
/// accessing settings, saving progress, and exiting the application.
/// </summary>
public class TitleScreenUI : MonoBehaviour
{
    // Set name of new save
    [SerializeField]
    GameObject setSaveName;
    // Pop up error if you try to start a new game with no more save slots
    [SerializeField]
    GameObject noMoreSlots;

    // Name of the save that can be edited in Unity editor
    [SerializeField]
    TMPro.TMP_InputField nameInput;

    /// <summary>
    /// Hides uneeded game objects
    /// </summary>
    private void Start()
    {
        // Hide stuff
        setSaveName.SetActive(false);
    }

    /// <summary>
    /// Checks for an empty save slot and displays a message if there is no
    /// more room, otherwise it prompts the user to set a name for the save.
    /// </summary>
    public void OnNewGameButton()
    {   // Will be used when class selection scene is added
        // SceneManager.LoadScene("ClassSelectScene")
        SaveManager.Instance.NewGame(0, "Alpha Save");
    }

    /// <summary>
    /// Calls the ShowMenu method of the LoadSaveUI.Instance.
    /// </summary>
    public void OnContinueButton()
    {
        LoadSaveUI.Instance.ShowMenu();
    }
    
    /// <summary>
    /// Closes the application when called.
    /// </summary>
    public void OnExitButton ()
    {
        Application.Quit();
    }
}