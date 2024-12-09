using UnityEngine;
using TMPro;  
public class Interactor : MonoBehaviour
{
    public float interactionDistance = 3f; 
    public KeyCode interactKey = KeyCode.F;
    public TMP_Text interactionText; 

    private Camera playerCamera;
    private Interactable currentInteractable;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();

        if (interactionText != null)
        {
            interactionText.enabled = false;
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactionText.text = interactable.interactionMessage;

                if (interactionText != null && !interactionText.enabled)
                {
                    interactionText.enabled = true;
                }

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
            currentInteractable.Interact();
        }
    }

    void HideInteractionText()
    {
        if (interactionText != null && interactionText.enabled)
        {
            interactionText.enabled = false;
        }
        currentInteractable = null; 
    }
}
