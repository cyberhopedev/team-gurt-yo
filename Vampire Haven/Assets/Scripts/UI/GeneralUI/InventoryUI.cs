using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using static GameConstants.Inventory;

/// <summary>
/// User interface layer for InventoryManager
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [Tooltip("Root panel shown/hidden when the player presses Y.")]
    [SerializeField] 
    private GameObject inventoryMenuPanel;
 
    [Tooltip("Layout container (e.g. Grid Layout Group) where slot prefabs spawn.")]
    [SerializeField]
    private Transform inventoryUIPanel;
 
    [Tooltip("Prefab with a Slot component — one is created per MAX_SLOTS.")]
    [SerializeField]
    private GameObject slotPrefab;

    private bool       _isOpen = false;
    private List<ItemSlot> _slots  = new List<ItemSlot>();

        private void Awake()
    {
        Instance = this;
 
        _isOpen = false;
        if (inventoryMenuPanel != null)
        {
            inventoryMenuPanel.SetActive(false);
        }
    }
 
    /// <summary>
    /// Build fixed slot GameObjects once. They stay alive for the scene's
    /// uptime
    /// </summary>
    private void Start()
    {
        BuildSlotUI();
    }

    private void OnDestroy()
    {
        // Clear the static reference when this scene unloads so InventoryManager's
        // null-conditional calls (Instance?.Refresh()) don't hold a dead reference.
        if (Instance == this)
            Instance = null;
    }
    
    /// <summary>
    /// Check if the Y key was pressed, if it was toggle the
    /// inventory panel
    /// </summary>
    private void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            Toggle();
        }
    }

    /// <summary>
    /// Opens the inventory panel and refreshes slot visuals.
    /// Can also be called externally (e.g. from a pause menu button).
    /// </summary>
    public void Open()
    {
        _isOpen = true;
        inventoryMenuPanel.SetActive(true);
        Time.timeScale = 0f;   // freeze game while inventory is open
        Refresh();
    }
 
    /// <summary>
    /// Closes the inventory panel and resumes normal time
    /// </summary>
    public void Close()
    {
        _isOpen = false;
        inventoryMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
 
    /// <summary>
    /// Toggles the inventory open or closed
    /// </summary>
    public void Toggle()
    {
        if(_isOpen){
            Close();
        }
        else {
            Open();
        }
    }

    /// <summary>
    /// Syncs every slot's visual with the current InventoryManager.items list.
    /// </summary>
    public void Refresh()
    {
        if (_slots == null)
        {
            return;
        }
 
        // Read the live item list from the data layer
        var items = InventoryManager.Instance?.items;
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i] == null){
                continue;
            }
 
            if (items != null && i < items.Count && items[i] != null)
            {
                _slots[i].SetItem(items[i]);
            }
            else
            {
                _slots[i].ClearItem();
            }
        }
    }

    /// <summary>
    /// Instantiates exactly MAX_SLOTS slot prefabs under InventoryUIPanel.
    /// Fixed-count slots avoid the layout flicker that comes from destroying
    /// and recreating GameObjects whenever the inventory changes.
    /// </summary>
    private void BuildSlotUI()
    {
        if (inventoryUIPanel == null || slotPrefab == null)
        {
            Debug.LogError("InventoryUI: inventoryUIPanel or slotPrefab is not assigned.");
            return;
        }

        _slots.Clear();
        // Remove any leftovers
        foreach (Transform child in inventoryUIPanel)
        {
            Destroy(child.gameObject);
        }
 
        for(int i = 0; i < GameConstants.Inventory.MAX_SLOTS; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, inventoryUIPanel);
            ItemSlot slot = slotGO.GetComponent<ItemSlot>();
            if (slot != null)
            {
                _slots.Add(slot);
            }
            else
            {
                Debug.LogError("InventoryUI: slotPrefab is missing a Slot component.");
            }
        }     
        // Do an initial refresh in case items were already loaded (e.g. continue game)
        Refresh();
    }
}