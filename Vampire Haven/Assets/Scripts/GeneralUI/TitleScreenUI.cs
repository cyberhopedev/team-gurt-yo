using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TitleScreenUI : MonoBehaviour
{
    // Set name of new save
    [SerializeField]
    GameObject setSaveName;
    // Pop up error if you try to start a new game with no more save slots
    [SerializeField]
    GameObject noMoreRoom;

    [SerializeField]
    TMPro.TMP_InputField nameInput;

    private void Start()
    {
        SettingsMenu.Instance.SettingsDoneButton();

        // Hide stuff
        setSaveName.SetActive(false);
        noMoreRoom.SetActive(false);
    }

    // Currently loads to alpha scene
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

    public void OnContinueButton()
    {
        LoadMenu.Instance.ShowMenu();
    }

    // Show settings menu
    public void OnSettingsButton()
    {
        SettingsMenu.Instance.ShowMenu();
    }

    // Exits application when fully built and running
    public void OnExitButton ()
    {
        Application.Quit();
    }

    // Save name of game and start next scene, assumes New Game button
    // has already ensured there is an empty slot available
    public void OnSaveButton()
    {
        if (nameInput.text.Length > 0)
        {
            int slot = SaveController.Instance.GetFirstEmptySlot();
            SaveController.Instance.NewGame(slot, nameInput.text);
            SceneManager.LoadScene("BetaScene");
        }
    }

    // Close button for error pop-up message
    public void OnClosebutton()
    {
        noMoreRoom.SetActive(false);
    }
}