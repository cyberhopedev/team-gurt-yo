using UnityEngine;
using static GameConstants.Save;

/// <summary>
/// In-game save UI shown on the "Save" tab of the pause menu. Lists every
/// save slot with its name/timestamp; clicking a slot OVERWRITES it with
/// the current game state.
/// </summary>
public class SavePanelUI : MonoBehaviour
{
    [SerializeField] private Transform  slotContainer;
    [SerializeField] private GameObject slotPrefab;       // same GameSlot prefab used by LoadSaveUI

    private GameSlot[] _slots;

    /// <summary>
    ///O nEnable runs every time the Save tab becomes visible
    /// </summary>
    private void OnEnable()
    {
        BuildOrRefresh();
    }

    /// <summary>
    /// Builds or refreshes the slots
    /// </summary>
    private void BuildOrRefresh()
    {
        // Lazy-build the slot widgets the first time the tab is shown.
        if (_slots == null)
        {
            _slots = new GameSlot[MAX_SLOTS];
            for (int i = 0; i < MAX_SLOTS; i++)
            {
                GameObject go = Instantiate(slotPrefab, slotContainer);
                _slots[i] = go.GetComponent<GameSlot>();

                // Capture loop variable into a local so the lambda doesn't all
                // close over the same `i` (classic foreach-closure bug).
                int idx = i;
                go.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnSlotClicked(idx));
            }
        }

        for(int i = 0; i < MAX_SLOTS; i++)
        {
            SaveData data = SaveManager.Instance.PeekSlot(i);
            if (data != null)
            {
                _slots[i].FillInSlot(data);
            }
            else
            {
                _slots[i].ShowEmpty();
            }
        }
    }

    /// <summary>
    /// Makes slot actionable, when it is clicked on it is saved to the given slot
    /// </summary>
    /// <param name="slotIndex">The index of the chosen slot to save to</param>
    private void OnSlotClicked(int slotIndex)
    {
        // Overwrite the slot with the current game state.
        SaveManager.Instance.SaveGame(slotIndex);

        // Refresh the UI so the just-saved slot shows the new timestamp.
        BuildOrRefresh();
    }
}