using UnityEngine;

// Child of ActionResponseSO that allows us to modify a room

[CreateAssetMenu(menuName = "Scriptable Object/Action Responses/Modify Room")]
public class ModifyRoomSO : ActionResponseSO
{
    public RoomSO roomModified; // Modified version of the room
    
    public override bool DoActionResponse()
    {
        // We check if we are in the Room we need to be in for the Action Response to take place (requiredString)
        if (RoomManager.Instance.currentRoom.roomName.Equals(requiredString))
        {
            RoomManager.Instance.ChangeRoom(roomModified);
            return true;
        }

        return false;
    }
}
