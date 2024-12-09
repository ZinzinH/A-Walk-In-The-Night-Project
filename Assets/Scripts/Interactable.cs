using UnityEngine;
using TMPro;  // TextMesh Pro namespace
using UnityEngine.SceneManagement; // For scene management

public class Interactable : MonoBehaviour
{
    public string interactionMessage = "Press F to interact"; // Message shown when player is nearby
    public TMP_Text interactionText;  // Reference to the TextMeshPro UI Text element for interaction
    public string interactionResponse = "You interacted with the object";  // Message after interaction

    public bool isKey = false; // Flag to identify if this is the key
    public bool isDoor = false; // Flag to identify if this is the door

    // Reference to the player's inventory (you can expand this in the future)
    public static bool hasKey = false; // Static variable to hold if the player has the key

    public void Interact()
    {
        // If this is the key, add it to the player's inventory and destroy the key object
        if (isKey)
        {
            hasKey = true;  // Player now has the key
            interactionText.text = "You picked up the key!";
            Destroy(gameObject);  // Destroy the key object
        }
        // If this is a door and the player has the key, load the next scene
        else if (isDoor && hasKey)
        {
            interactionText.text = "You unlocked the door!";
            LoadNextScene();
        }
        else if (isDoor && !hasKey)
        {
            interactionText.text = "The door is locked. You need the key!";
        }
        else
        {
            interactionText.text = interactionResponse; // Default interaction response
        }
    }

    void LoadNextScene()
    {
        // Load the next scene. You can modify the scene name or use the build index.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene in build settings
    }
}
