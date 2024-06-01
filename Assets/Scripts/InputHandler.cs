using TMPro;
using UnityEngine;

// Script that takes care of the player input

public class InputHandler : MonoBehaviour
{
    // Input Field where the user enters the input
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        // Every time we finish editing in the input field, the function AcceptUserInput will be called
        inputField.onEndEdit.AddListener(AcceptUserInput);
    }

    // Function that checks if any keyword of any Input Action has been entered by the player
    private void AcceptUserInput(string input)
    {
        input = input.ToLower(); // We convert all input to lowercase
        GameManager.Instance.UpdateLogList(input); // We save and show the player the input he has entered
        
        string[] separatedInput = GetSeparatedInput(input); // We obtain the input separated by words
        foreach (InputActionSO inputAction in GameManager.Instance.GetInputActions())
        {
            // We check if the first word of the input is one of the keywords of the available Input Actions
            if (inputAction.keyWord.Equals(separatedInput[0]))
            {
                inputAction.RespondToInput(separatedInput);
            }
        }
        
        InputComplete();
    }

    // Function that reactivates the Input Field and empties its contents
    private void InputComplete()
    {
        inputField.ActivateInputField();
        inputField.text = null;
    }

    // Helper function that separates a given string by words
    private string[] GetSeparatedInput(string input)
    {
        return input.Split(" ");
    }
}
