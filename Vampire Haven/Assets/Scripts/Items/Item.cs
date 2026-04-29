using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;

    [Tooltip("The ScriptableObject that holds this item's name, description, icon, and stats.")]
    public ItemSO itemData;

}
