using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script that takes care of the game logic

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game constants
    public const string NEW_LINE = "\n";
    public const string ASTERISK = "*";
    public const string SPACE = " ";

    // Game introduction
    [TextArea][SerializeField] private string introduction;

    // List that saves the logs (all messages) of the game
    private List<string> logList = new List<string>();

    // TextMeshPro where all the logs of the game are displayed
    [SerializeField] private TextMeshProUGUI displayText;
    
    // Array that stores all the Input Actions that we can perform
    // Every time we develop a child of InputActionSO, we have to add that Scriptable Object to the array (via inspector)
    // There are currently 5: Go, Examine, Take, Inventory and Use
    [SerializeField] private InputActionSO[] inputActionsArray;
    
    private void Awake()
    {
        // We make Game Manager a Singleton (a class with a single instance)
        if (Instance != null) // If an instance already exists, an error will be displayed
        {
            Debug.LogError("There is more than one instance");
        }

        Instance = this; // If no instance exists, the first (that also should be the only) instance will be stored in the Instance variable
    }

    private void Start()
    {
        // TODO (Ejercicio opcional): Crea una función StartGame con la lógica que representa empezar una partida 
        UpdateLogList(introduction); // When we start the game, the first thing we do is to show the introduction
        DisplayFullRoomText(); // Then, the description of the first room is shown
    }

    // Function that returns the array of available Input Actions
    public InputActionSO[] GetInputActions()
    {
        return inputActionsArray;
    }

    // Function that displays all the information of the Room we are in (currentRoom)
    public void DisplayFullRoomText()
    {
        ClearAllCollectionsForNewRoom(); // First we clean all collections
        
        string roomDescription = RoomManager.Instance.currentRoom.description + NEW_LINE; // Room description
        string exitDescriptions = string.Join(NEW_LINE, RoomManager.Instance.GetExitDescriptionsInRoom()); // Room exits
        string itemDescriptions = string.Join(NEW_LINE, RoomManager.Instance.GetItemDescriptionsInRoom()); // Room items
        
        // TODO (Ejercicio opcional): Si no hay items en la habitación, no añadir NEW_LINE al final para que no haya un intro extra
        string fullText = roomDescription + exitDescriptions + NEW_LINE + itemDescriptions;
        UpdateLogList(fullText);
    }
    
    // Function that prepares and displays the message with all inventory contents
    public void DisplayInventory()
    {
        string message = "You look in your bag. Inside you find:" + NEW_LINE;
        string separator = ASTERISK + SPACE;
        message += separator + string.Join(NEW_LINE + separator, Inventory.Instance.GetInventory());
        
        // TODO (Ejercicio opcional): Mostrar un mensaje diferente si no hay items en el inventario
        
        UpdateLogList(message);
    }

    // Helper function that adds a new log and displays it
    public void UpdateLogList(string stringToAdd)
    {
        logList.Add(stringToAdd + NEW_LINE);
        DisplayLoggedText();
    }

    // Helper function that displays all the logs saved in logList
    private void DisplayLoggedText()
    {
        displayText.text = string.Join(NEW_LINE, logList);
    }

    // Helper function that clears all the collections
    private void ClearAllCollectionsForNewRoom()
    {
        RoomManager.Instance.ClearExits();
        RoomManager.Instance.ClearItems();
    }
}
