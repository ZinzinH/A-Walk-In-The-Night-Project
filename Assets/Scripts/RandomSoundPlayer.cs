using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    // Array to hold 5 different AudioClip references
    public AudioClip[] audioClips;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if audioClips array has been set up
        if (audioClips.Length == 0)
        {
            Debug.LogWarning("Audio Clips not assigned! Please assign 5 Audio Clips.");
            return;
        }

        // Start the process of playing random sounds at intervals
        StartCoroutine(PlayRandomSoundAtIntervals());
    }

    // Coroutine to play a random sound at random intervals
    private IEnumerator PlayRandomSoundAtIntervals()
    {
        while (true)
        {
            // Play a random sound
            PlayRandomSound();

            // Wait for a random interval between 10 to 30 seconds
            float randomInterval = Random.Range(10f, 30f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    // Method to play a random sound from the audioClips array
    private void PlayRandomSound()
    {
        // Get a random index from the audioClips array
        int randomIndex = Random.Range(0, audioClips.Length);

        // Play the random clip
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
    }
}
