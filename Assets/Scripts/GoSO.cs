using UnityEngine;

// Child of InputActionSO that allows us to change room

[CreateAssetMenu(menuName = "Scriptable Object/Input Actions/Go")]
public class GoSO : InputActionSO
{
    // Function that allows us to change room given a valid direction
    public override void RespondToInput(string[] separatedInput)
    {
        RoomManager.Instance.TryToChangeRoom(separatedInput[1]); // It is assumed that separatedInput[1] = direction
    }
}
