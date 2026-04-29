using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Holds every ItemSO in the game so save/load can look up an item
/// by its name string.
/// </summary>
public class ItemDictionary : MonoBehaviour
{
    // Public instance of ItemDictionary that can be called to other classes
    public static ItemDictionary Instance { get; private set; }

    [Tooltip("Drag every ItemSO asset (potions, weapons, key items) here.")]
    public List<ItemSO> allItems;
    private Dictionary<string, ItemSO> _itemDictionary;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _itemDictionary = new Dictionary<string, ItemSO>();
        foreach(ItemSO item in allItems)
        {
            if (item != null && !_itemDictionary.ContainsKey(item.itemName))
            {
                _itemDictionary.Add(item.itemName, item);
            }
            else
            {
                Debug.LogWarning($"ItemDictionary: duplicate item name '{item.itemName}' — only the first will be registered.");
            }
        }
    }

    public ItemSO GetItemByID(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            return null;
        }
        _itemDictionary.TryGetValue(itemName, out ItemSO item);
        if(item == null)
        {
            Debug.LogWarning($"ItemDictionary: '{itemName}' not registered.");
        }
        return item;
    }
}
