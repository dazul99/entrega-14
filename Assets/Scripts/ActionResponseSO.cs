using UnityEngine;

// Template that defines the actions to be performed currently only after indicating that we want to use an object

public abstract class ActionResponseSO : ScriptableObject
{
    public string requiredString; // Corresponds to the roomName of the Room in which we have to be in order to use an object
    public abstract bool DoActionResponse(); // Function to override when creating a child of this class, e.g. ModifyRoomSO
}
