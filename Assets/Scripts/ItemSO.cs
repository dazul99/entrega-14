using UnityEngine;

// Template of what represents an Item

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName; // Item Name
    [TextArea] public string description; // Item Description
    public Interaction[] interactions; // Item Interactions (actions that can be carried out with the item)
}
