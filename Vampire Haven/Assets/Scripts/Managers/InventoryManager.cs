using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using static GameConstants.Inventory;

/// <summary>
/// Handles the inventory of the player
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // Public instance of InventoryManager that can be called to other classes
    public static InventoryManager Instance { get; private set; }

    [Tooltip("The ItemDictionary used to resolve item names back to ItemSO assets on load.")]
    [SerializeField]
    private ItemDictionary itemDictionary;

    // List of items in the inventory, used for saving and loading the inventory state.
    
    [HideInInspector] 
    public List<Item> items = new List<Item>();
    
    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds an item to the inventory, used by MerchantUI when a player
    /// buys something
    /// </summary>
    /// <param name="item">The item being added</param>
    /// <returns>True if there was room in the inventory, false if otherwise</returns>
    public bool AddItem(Item item)
    {
        if(item == null)
        {
            return false;
        }
        if(items.Count >= MAX_SLOTS)
        { 
            Debug.Log("Inventory full."); 
            return false;
        }
 
        items.Add(item);
        Debug.Log($"Added '{item.itemName}' ({items.Count}/{MAX_SLOTS}).");
        InventoryUI.Instance?.Refresh();
        return true;
    }

    /// <summary>
    /// Removes the item at a given index.
    /// BattleManager calls this after a consumable is used.
    /// </summary>
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            return;
        }
        Debug.Log($"Removed '{items[index].itemName}'.");
        items.RemoveAt(index);
        InventoryUI.Instance?.Refresh();
    }

    ///<summary>Returns true if at least one more item can be added.</summary>
    public bool HasRoom() => items.Count < MAX_SLOTS;
 
    /// <summary>
    /// Empties the inventory. Called by SaveManager.NewGame() on a fresh run.
    /// </summary>
    public void Clear()
    {
        items.Clear();
        InventoryUI.Instance?.Refresh();
    }

    /// <summary>
    /// Converts the current inventory into a serializable list for SaveManager.
    /// </summary>
    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> saveList = new List<InventorySaveData>();
 
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                saveList.Add(new InventorySaveData
                {
                    itemID    = items[i].itemName,
                    slotIndex = i
                });
            }
        }
 
        return saveList;
    }
 
    /// <summary>
    /// Restores the inventory from a previously saved list.
    /// Called by SaveManager after reading a save file from disk.
    /// </summary>
    public void SetInventoryItems(List<InventorySaveData> savedItems)
    {
        items.Clear();
 
        if (savedItems == null)
        {
            InventoryUI.Instance?.Refresh();
            return;
        }
 
        // Restore in slot order so items appear in the same positions as when saved
        savedItems.Sort((a, b) => a.slotIndex.CompareTo(b.slotIndex));
 
        foreach (InventorySaveData data in savedItems)
        {
            Item item = itemDictionary != null
                ? itemDictionary.GetItemByID(data.itemID)
                : null;
 
            if (item != null)
                items.Add(item);
            else
                Debug.LogWarning($"Could not find item '{data.itemID}' in ItemDictionary.");
        }
 
        InventoryUI.Instance?.Refresh();
    }    

    /// <summary>
    /// Swaps two slots in the items list. Used by ItemDragHandler after a
    /// successful drop so save files reflect the new visual order
    /// </summary>
    public void SwapSlots(int aIdx, int bIdx)
    {
        // Pad the list out with nulls if either index is past the current end
        while (items.Count <= Mathf.Max(aIdx, bIdx))
        {
            items.Add(null);   
        }
        Item tmp     = items[aIdx];
        items[aIdx]  = items[bIdx];
        items[bIdx]  = tmp;
    }
}