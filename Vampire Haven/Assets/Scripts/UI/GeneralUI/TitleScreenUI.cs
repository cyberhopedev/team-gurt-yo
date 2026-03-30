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
    GameObject noMoreRoom;

    // Name of the save that can be edited in Unity editor
    [SerializeField]
    TMPro.TMP_InputField nameInput;

    /// <summary>
    /// Calls the SettingsDoneButton method from the SettingsMenu instance and
    /// hides two game objects.
    /// </summary>
    private void Start()
    {
        SettingsMenu.Instance.SettingsDoneButton();

        // Hide stuff
        setSaveName.SetActive(false);
        noMoreRoom.SetActive(false);
    }

    /// <summary>
    /// Checks for an empty save slot and displays a message if there is no
    /// more room, otherwise it prompts the user to set a name for the save.
    /// </summary>
    public void OnNewGameButton()
    {
        int slot = SaveController.Instance.GetFirstEmptySlot();
        if (slot == -1)
        {
            noMoreRoom.SetActive(true);
            return;
        }
        setGameName.SetActive(true);
    }

    /// <summary>
    /// Calls the ShowMenu method of the LoadMenu.Instance.
    /// </summary>
    public void OnContinueButton()
    {
        LoadMenu.Instance.ShowMenu();
    }

    
    /// <summary>
    /// Calls the ShowMenu method of the SettingsMenu instance.
    /// </summary>
    public void OnSettingsButton()
    {
        SettingsMenu.Instance.ShowMenu();
    }
    
    /// <summary>
    /// Closes the application when called.
    /// </summary>
    public void OnExitButton ()
    {
        Application.Quit();
    }

    /// <summary>
    /// Checks if a name input is not empty, gets the first empty slot from
    /// SaveController, creates a new game with the input name in that slot, and then loads the "BetaScene".
    /// </summary>
    public void OnSaveButton()
    {
        if (nameInput.text.Length > 0)
        {
            int slot = SaveController.Instance.GetFirstEmptySlot();
            SaveController.Instance.NewGame(slot, nameInput.text);
            SceneManager.LoadScene("BetaScene");
        }
    }

    /// <summary>
    /// Sets the active state of the noMoreRoom object to false.
    /// </summary>
    public void OnClosebutton()
    {
        noMoreRoom.SetActive(false);
    }
}