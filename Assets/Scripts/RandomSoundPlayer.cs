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
            Debug.LogWarning("Audio Clips not assigned! Please assign Audio Clips.");
            return;
        }

        // Start the process of playing random sounds at intervals
        StartCoroutine(PlayRandomSoundAtIntervals());
    }

    // Coroutine to play a random sound and wait until it finishes before playing the next one
    private IEnumerator PlayRandomSoundAtIntervals()
    {
        while (true)
        {
            // Play a random sound
            PlayRandomSound();

            // Wait for the current sound to finish before continuing
            while (audioSource.isPlaying)
            {
                yield return null; // Wait until the sound finishes
            }

            // Wait for 10 seconds before playing the next sound
            yield return new WaitForSeconds(10f);
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
