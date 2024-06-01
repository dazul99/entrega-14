using UnityEngine;

// Template that defines the input actions that the player can perform

public abstract class InputActionSO : ScriptableObject
{
    public string keyWord; // Keyword that the player has to indicate if he wants to perform the input action
    public abstract void RespondToInput(string[] separatedInput); // Function to override when creating a child of this class, e.g. GoSO, ExamineSO...
}
