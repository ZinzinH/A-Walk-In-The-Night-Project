using UnityEngine;
using TMPro;  // Use TextMesh Pro namespace

public class Interactable : MonoBehaviour
{
    public string objectName;  // Name of the object (key, box, etc.)
    public string interactionMessage = "Press F to interact"; // Message shown when player is nearby
    public TMP_Text interactionText;  // Reference to the TextMeshPro UI Text element for interaction

    // This will define what the object says when interacted with (custom messages)
    public string interactionResponse = "You interacted with the object";

    public void Interact()
    {
        if (interactionText != null)
        {
            // Update the interaction text to show the unique message after interaction
            interactionText.text = interactionResponse;
        }

        // Optionally, you can add further logic here, like picking up items, opening doors, etc.
        Debug.Log($"Interacted with {objectName}: {interactionResponse}");
    }
}
