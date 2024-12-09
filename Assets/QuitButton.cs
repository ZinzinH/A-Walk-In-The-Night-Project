using UnityEngine;
using UnityEngine.SceneManagement; // To manage scene transitions

public class QuitButton : MonoBehaviour
{
    // Method to load the "MainMenu" scene
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
