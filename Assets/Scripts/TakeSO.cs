using UnityEngine;

// Child of InputActionSO that allows us to take an Item

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Take")]
public class TakeSO : InputActionSO
{
    // Function that allows us to take an item given an existing itemName
    public override void RespondToInput(string[] separatedInput)
    {
        string response = RoomManager.Instance.TryToTakeItem(separatedInput[1]); // It is assumed that separatedInput[1] = itemName
        GameManager.Instance.UpdateLogList(response);
    }
}
