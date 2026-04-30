using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using static GameConstants.Save; 

/// <summary>
/// UI connection to load a save when a player chooses a existing
/// game slot
/// </summary>
public class LoadSaveUI : MonoBehaviour
{
    // Public instance of LoadSaveUI to be called by other classes
    public static LoadSaveUI Instance;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Transform  slotContainer;   // parent object that holds GameSlot prefabs
    [SerializeField] private GameObject slotPrefab;      // prefab with a GameSlot component
    private GameSlot[] _slots;
    private int _selectedSlot = -1;

    private void Start()
    {
        Instance = this;
        menuPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        LoadSaveSlots();
    }

    private void LoadSaveSlots()
    {
        // Build slot prefabs once
        if (_slots == null)
        {
            _slots = new GameSlot[MAX_SLOTS];
            for (int i = 0; i < MAX_SLOTS; i++)
                _slots[i] = Instantiate(slotPrefab, slotContainer).GetComponent<GameSlot>();
        }

        // Fill or empty each one
        for (int i = 0; i < MAX_SLOTS; i++)
        {
            SaveData data = SaveManager.Instance.PeekSlot(i);
            if (data != null) _slots[i].FillInSlot(data);
            else              _slots[i].ShowEmpty();
        }
    }

    public void OnSaveSlotPress(int slotIndex)
    {
        // Highlight the chosen slot (un-highlight the rest).
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i == slotIndex) _slots[i].ClickSlot();
            else _slots[i].UnclickSlot();
        }
        _selectedSlot = slotIndex;
    }

    public void OnLoadButton()
    {
        if (_selectedSlot < 0)
        {
            return;
        }
        SaveManager.Instance.LoadGame(_selectedSlot);
    }

    public void OnBackButton()
    {
        menuPanel.SetActive(false);
    }

    public void OnEraseButton()
    {
        if (_selectedSlot < 0)
        {
            return;
        }
        SaveManager.Instance.DeleteSave(_selectedSlot);
        LoadSaveSlots();
    }
}