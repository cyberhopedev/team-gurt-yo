using UnityEngine;

// Tags every inventory entry. Drives the "Use" button availability,
// the merchant's sellable filter, and the key-icon overlay in InventoryUI.
public enum ItemType { Consumable, Weapon, Armor, KeyStory }

/// <summary>
/// An item in the game. Lives on a prefab GameObject so you can also use
/// the same prefab as a world pickup (sprite + collider + this script).
/// The inventory stores REFERENCES to the prefab itself, not instantiated
/// copies — Item is just the data layer when accessed that way.
/// </summary>
public class Item : MonoBehaviour
{
    [Header("Identity")]
    [Tooltip("Display name AND save-file key. Must be unique across all items.")]
    public string itemName;

    [TextArea]
    [Tooltip("Shown when the player clicks the slot in the inventory UI.")]
    public string itemDescription;

    [Tooltip("Sprite shown in the inventory cell. Auto-pulled from a child SpriteRenderer if left empty.")]
    public Sprite icon;

    [Header("Type & Pricing")]
    public ItemType itemType = ItemType.Consumable;
    public int      goldCost = 0;

    [Header("Stats — Weapons/Armor only")]
    public int attackBonus  = 0;
    public int defenseBonus = 0;

    [Header("Stats — Consumables only")]
    public int healPercent       = 0;   // % of maxHP restored
    public int defenseBuffAmount = 0;   // shield spell
    public int directDamage      = 0;   // bomb
    public int poisonAmount      = 0;   // bottled fog

    // Computed property — no backing field, just a one-line getter.
    // Saves us writing "if (item.itemType == ItemType.KeyStory)" everywhere.
    public bool isKeyItem => itemType == ItemType.KeyStory;

    /// <summary>
    /// Editor-only callback — runs whenever the prefab is loaded or a value is
    /// edited in the Inspector. We use it to auto-fill the icon field from
    /// the SpriteRenderer that's already on the prefab, so you don't have to
    /// drag the same sprite into two fields.
    /// </summary>
    private void OnValidate()
    {
        if (icon == null)
        {
            // GetComponentInChildren walks the GameObject and any nested children,
            // so it works whether the SpriteRenderer is on the root or a child.
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            if (sr != null && sr.sprite != null) icon = sr.sprite;
        }
    }
}