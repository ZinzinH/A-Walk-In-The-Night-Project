using UnityEngine;
using UnityEngine.SceneManagement; // To manage scene transitions

public class RetryButton : MonoBehaviour
{
    // Method to load the "Map" scene
    public void RetryGame()
    {
        SceneManager.LoadScene("Map");
    }
}
