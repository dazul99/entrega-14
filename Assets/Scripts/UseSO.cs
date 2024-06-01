using UnityEngine;

// Child of InputActionSO that allows us to use an Item

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Use")]
public class UseSO : InputActionSO
{
    // Function that allows us to use an item given an existing itemName
    public override void RespondToInput(string[] separatedInput)
    {
        RoomManager.Instance.TryToUseItem(separatedInput[1]); // It is assumed that separatedInput[1] = itemName
    }
}
