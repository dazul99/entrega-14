using System.Collections.Generic;
using UnityEngine;

// Script that takes care of the inventory logic

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    // List of strings with the itemNames of the items we have stored in inventory
    [SerializeField] private List<string> inventory = new List<string>();

    private void Awake()
    {
        // We make Inventory a Singleton (a class with a single instance)
        if (Instance != null)
        {
            Debug.LogError("There's more than one instance");
        }

        Instance = this;
    }

    // Function that returns the inventory
    public List<string> GetInventory()
    {
        return inventory;
    }

    // Function that checks if an item belongs to the inventory
    public bool IsItemInInventory(string item)
    {
        return inventory.Contains(item);
    }

    // Function that adds an item to the inventory
    public void AddItemToInventory(string itemToAdd)
    {
        inventory.Add(itemToAdd);
    }
}
