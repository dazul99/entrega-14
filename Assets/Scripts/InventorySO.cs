using UnityEngine;

// Child of InputActionSO that allows us to see the inventory items

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Inventory")]
public class InventorySO : InputActionSO
{
    // Function that allows us to display the inventory items
    public override void RespondToInput(string[] separatedInput)
    {
        GameManager.Instance.DisplayInventory();
    }
}
