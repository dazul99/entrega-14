using UnityEngine;

// Child of InputActionSO that allows us to examine an Item

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Examine")]
public class ExamineSO : InputActionSO
{
    // Function that allows us to examine an item given an existing itemName
    public override void RespondToInput(string[] separatedInput)
    {
        string response = RoomManager.Instance.TryToExamineItem(separatedInput[1]); // It is assumed that separatedInput[1] = itemName
        GameManager.Instance.UpdateLogList(response);
    }
}
