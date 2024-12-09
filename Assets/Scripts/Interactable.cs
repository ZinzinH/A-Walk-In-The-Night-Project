using UnityEngine;
using TMPro;  
using UnityEngine.SceneManagement; 

public class Interactable : MonoBehaviour
{
    public string interactionMessage = "Press F to interact";
    public TMP_Text interactionText;  
    public string interactionResponse = "You interacted with the object";  

    public bool isKey = false; 
    public bool isDoor = false; 

    public static bool hasKey = false; 

    public void Interact()
    {
        if (isKey)
        {
            hasKey = true;  
            interactionText.text = "You picked up the key!";
            Destroy(gameObject);  
        }
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
            interactionText.text = interactionResponse; 
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}
