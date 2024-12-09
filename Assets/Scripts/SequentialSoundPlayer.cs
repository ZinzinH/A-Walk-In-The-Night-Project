using System.Collections;
using UnityEngine;

public class SequentialSoundPlayer : MonoBehaviour
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
            Debug.LogWarning("Audio Clips not assigned! Please assign Audio Clips.");
            return;
        }

        // Start the process of playing sounds in sequence with proper delays
        StartCoroutine(PlaySequentialSound());
    }

    // Coroutine to play a sound sequentially, with wait time after each clip
    private IEnumerator PlaySequentialSound()
    {
        while (true)
        {
            // Play the current sound
            PlayCurrentSound();

            // Wait for the current sound to finish playing
            while (audioSource.isPlaying)
            {
                yield return null; // Wait until the sound finishes
            }

            // Wait for 10 seconds before playing the next sound
            yield return new WaitForSeconds(10f);

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
