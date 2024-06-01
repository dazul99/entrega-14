using UnityEngine;
using System;

// Template of what represents an item interaction

[Serializable]
public class Interaction
{
    public InputActionSO inputAction; // Input Action associated with the interaction
    [TextArea] public string responseDescription; // Description of the Input Action response
    public ActionResponseSO actionResponse; // Action of the Input Action response
}
