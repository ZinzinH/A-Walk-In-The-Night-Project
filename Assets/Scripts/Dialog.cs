using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // Array to hold 5 different AudioClip references
    public AudioClip[] audioClips;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // To track the current audio clip index
    private int currentClipIndex = 0;

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

        // Start the process of playing sounds in sequence at intervals
        StartCoroutine(PlaySequentialSoundAtIntervals());
    }

    // Coroutine to play a sound at regular intervals
    private IEnumerator PlaySequentialSoundAtIntervals()
    {
        while (true)
        {
            // Play the current sound in sequence
            PlayCurrentSound();

            // Wait for a fixed interval between sounds (you can adjust the time here)
            float interval = 10f; // For example, play a sound every 10 seconds
            yield return new WaitForSeconds(interval);

            // Move to the next sound in the array, looping back to the start if necessary
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }
    }

    // Method to play the current sound from the audioClips array
    private void PlayCurrentSound()
    {
        // Set the clip from the current index
        audioSource.clip = audioClips[currentClipIndex];

        // Play the clip
        audioSource.Play();
    }
}
