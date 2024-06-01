using UnityEngine;

// Template of what represents a Room

[CreateAssetMenu(menuName = "Scriptable Object/Room")]
public class RoomSO : ScriptableObject
{
    public string roomName; // Room Name
    [TextArea] public string description; // Room Description
    public Exit[] exits; // Exits of the Room
    public ItemSO[] items; // Exits in the Room
}
