using UnityEngine;
using TMPro;  // Use TextMesh Pro namespace

public class Interactor : MonoBehaviour
{
    public float interactionDistance = 3f; // Distance to interact with objects
    public KeyCode interactKey = KeyCode.F; // Key for interaction
    public TMP_Text interactionText; // Reference to the TextMeshPro UI Text element for interaction message

    private Camera playerCamera;
    private Interactable currentInteractable; // The currently detected interactable object

    void Start()
    {
        // Get the camera attached to the player (assuming there is one)
        playerCamera = GetComponentInChildren<Camera>();

        // Initially hide the interaction text
        if (interactionText != null)
        {
            interactionText.enabled = false;
        }
    }

    void Update()
    {
        // Perform a raycast to check if we're facing an interactable object
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                // Show the interaction message when near the interactable object
                interactionText.text = interactable.interactionMessage;

                if (interactionText != null && !interactionText.enabled)
                {
                    interactionText.enabled = true;
                }

                // Check for player input to interact
                if (Input.GetKeyDown(interactKey))
                {
                    InteractWithObject();
                }
            }
            else
            {
                HideInteractionText();
            }
        }
        else
        {
            HideInteractionText();
        }
    }

    void InteractWithObject()
    {
        if (currentInteractable != null)
        {
            // Change the message to the post-interaction response
            currentInteractable.Interact();
        }
    }

    void HideInteractionText()
    {
        if (interactionText != null && interactionText.enabled)
        {
            interactionText.enabled = false;
        }
        currentInteractable = null; // Reset the current interactable
    }
}
