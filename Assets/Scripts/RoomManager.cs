using System.Collections.Generic;
using UnityEngine;

// Script that takes care of the room logic

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    public RoomSO currentRoom; // Room we are in

    private List<ItemSO> itemsInRoom = new List<ItemSO>(); // Items that are in the room, but not in the inventory
    private List<string> itemDescriptionsInRoom = new List<string>(); // List of the descriptions of the items that are in the room, but not in the inventory
    private List<ItemSO> usableItems = new List<ItemSO>(); // List of the usable items 
    

    private Dictionary<string, RoomSO> exitsDictionary = new Dictionary<string, RoomSO>(); // Dictionary that relates directions to rooms
    private Dictionary<string, string> examineDictionary = new Dictionary<string, string>(); // Dictionary that relates item names to items descriptions
    private Dictionary<string, string> takeDictionary = new Dictionary<string, string>(); // Dictionary that relates item names to items take interaction responseDescription
    private Dictionary<string, ActionResponseSO> useDictionary = new Dictionary<string, ActionResponseSO>(); // Dictionary that relates item names to items use interaction ActionResponse


    private void Awake()
    {
        // We make Room Manager a Singleton (a class with a single instance)
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance");
        }

        Instance = this;
    }

    // Function that manages the exits of the currentRoom and returns a list with the descriptions of these exits
    public List<string> GetExitDescriptionsInRoom()
    {
        List<string> exitDescriptions = new List<string>();
        foreach (Exit exit in currentRoom.exits)
        {
            exitDescriptions.Add(exit.description);
            // We set up exitsDictionary
            exitsDictionary.Add(exit.direction, exit.room);
        }

        return exitDescriptions;
    }

    // Function that manages the items of the currentRoom
    private void SetItemsInRoom()
    {
        // We go through all the items in the current room
        foreach (ItemSO item in currentRoom.items)
        {
            // We check if the Item is NOT in the inventory
            if (!Inventory.Instance.IsItemInInventory(item.itemName))
            {
                // We set up itemsInRoom
                itemsInRoom.Add(item);
                // We set up itemDescriptionsInRoom
                itemDescriptionsInRoom.Add(item.description);
            }

            // We go through all the interactions of each item
            foreach (Interaction interaction in item.interactions)
            {
                // We check if the item can be examined
                if (interaction.inputAction.keyWord.Equals("examine"))
                {
                    // We set up examineDictionary
                    examineDictionary.Add(item.itemName, interaction.responseDescription);
                }
                // We check if the item can be taken
                else if (interaction.inputAction.keyWord.Equals("take"))
                {
                    // We set up takeDictionary
                    takeDictionary.Add(item.itemName, interaction.responseDescription);
                }
                // We check if the item can be used
                else if (interaction.inputAction.keyWord.Equals("use"))
                {
                    // We check if the item has not been added yet to usableItems
                    if (!usableItems.Contains(item))
                    {
                        // We set up usableItems
                        usableItems.Add(item);
                    }
                }
            }
        }
    }

    // Function that returns the item descriptions of the currentRoom
    public List<string> GetItemDescriptionsInRoom()
    {
        SetItemsInRoom(); 
        return itemDescriptionsInRoom;
    }

    // Function that checks and manages the change of room
    public void TryToChangeRoom(string direction)
    {
        if (exitsDictionary.TryGetValue(direction, out var room))
        {
            GameManager.Instance.UpdateLogList($"You head to the {direction}");
            ChangeRoom(room);
        }
        else
        {
            GameManager.Instance.UpdateLogList($"There's no path to the {direction}");
        }
    }

    // Function that checks and manages the action of examining an item
    public string TryToExamineItem(string item)
    {
        // TODO (Ejercicio opcional): Comprobar si también está en el inventario y es examinable para también poder examinar los items del inventario
        if (examineDictionary.ContainsKey(item))
        {
            return examineDictionary[item];
        }

        return $"You can't examine {item}";
    }
    
    // Function that checks and manages the action of taking an item
    public string TryToTakeItem(string item)
    {
        if (takeDictionary.ContainsKey(item))
        {
            RemoveItemFromRoom(GetItemInRoomFromName(item));
            Inventory.Instance.AddItemToInventory(item);
            SetUseDictionary();
            return takeDictionary[item];
        }

        return $"You can't take {item}";
    }

    // Function that checks and manages the action of using an item
    public void TryToUseItem(string itemToUse)
    {
        if (Inventory.Instance.IsItemInInventory(itemToUse))
        {
            if (useDictionary.TryGetValue(itemToUse, out ActionResponseSO actionResponse))
            {
                bool actionResult = actionResponse.DoActionResponse();
                if (!actionResult)
                {
                    GameManager.Instance.UpdateLogList("Nothing happens...");
                }
            }
            else
            {
                GameManager.Instance.UpdateLogList($"You can't use the {itemToUse}");
            }
        }
        else
        {
            GameManager.Instance.UpdateLogList($"There's no {itemToUse} in your inventory to use");
        }
    }

    // Function that sets up useDictionary
    private void SetUseDictionary()
    {
        // We go through all the items in the inventory
        foreach (string itemInInventory in Inventory.Instance.GetInventory())
        {
            // We try to obtain the usable itemSO associated to itemName
            ItemSO item = GetUsableItemFromName(itemInInventory);
            if (item == null)
            {
                continue;
            }

            // If we have reached this point, it means that we have a usable item
            // We go through all the interactions of the usable item
            foreach (Interaction interaction in item.interactions)
            {
                // It is assumed that only one interaction (and only one) is supposed to have an actionResponse (associated with the use inputAction)
                if (interaction.actionResponse == null)
                {
                    continue;
                }
                
                // If we have reached this point, it means that we have an Action Response
                if (!useDictionary.ContainsKey(itemInInventory))
                {
                    // We set up useDictionary
                    useDictionary.Add(itemInInventory, interaction.actionResponse);
                }
            }
        }
    }

    // Function that cleans exitsDictionary
    public void ClearExits()
    {
        exitsDictionary.Clear();
    }

    // Function that cleans all collections related to items that need to be empty when entering a new room
    public void ClearItems()
    {
        itemsInRoom.Clear();
        itemDescriptionsInRoom.Clear();
        examineDictionary.Clear();
        takeDictionary.Clear();
    }

    // Function that returns an item in room given an itemName
    private ItemSO GetItemInRoomFromName(string itemName)
    {
        foreach (ItemSO item in currentRoom.items)
        {
            if (itemName.Equals(item.itemName))
            {
                return item;
            }
        }

        return null;
    }
    
    // Function that returns a usable item given an itemName
    private ItemSO GetUsableItemFromName(string itemName)
    {
        foreach (ItemSO item in usableItems)
        {
            if (itemName.Equals(item.itemName))
            {
                return item;
            }
        }

        return null;
    }

    // Function that removes an item from the currentRoom
    private void RemoveItemFromRoom(ItemSO item)
    {
        itemsInRoom.Remove(item);
    }

    // Function which takes care of all the logic of changing room
    public void ChangeRoom(RoomSO newRoom)
    {
        currentRoom = newRoom;
        GameManager.Instance.DisplayFullRoomText();
    }
    
}
