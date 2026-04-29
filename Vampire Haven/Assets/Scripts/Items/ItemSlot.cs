using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{

    [Header("Display References")]
    [Tooltip("Image component that shows the item's icon sprite.")]
    public Image itemIcon;
 
    [Tooltip("Text label that shows the item's name.")]
    public TextMeshProUGUI itemNameText;
 
    // The ItemSO currently displayed in this slot (null if empty)
    public ItemSO currentItem { get; private set; }
     
    /// <summary>
    /// Populates this slot with an item's icon and name.
    /// </summary>
    public void SetItem(ItemSO item)
    {
        currentItem = item;
 
        if (itemIcon != null)
        {
            itemIcon.sprite  = item.icon;
            // Make the icon fully visible (alpha = 1)
            itemIcon.color   = Color.white;
            itemIcon.enabled = item.icon != null;
        }
 
        if (itemNameText != null)
        {
            itemNameText.text = item.itemName;   
        }
    }
 
    /// <summary>
    /// Clears this slot back to an empty visual state.
    /// </summary>
    public void ClearItem()
    {
        currentItem = null;
 
        if (itemIcon != null)
        {
            itemIcon.sprite  = null;
            // Hide the icon image when the slot is empty so the background shows through
            itemIcon.color   = new Color(1f, 1f, 1f, 0f);
            itemIcon.enabled = false;
        }
 
        if (itemNameText != null)
        {
            itemNameText.text = "";
        }
    }

}