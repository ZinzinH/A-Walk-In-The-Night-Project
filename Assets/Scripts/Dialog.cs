using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Length == 0)
        {
            Debug.LogWarning("Audio Clips not assigned! Please assign 5 Audio Clips.");
            return;
        }

        StartCoroutine(PlaySequentialSoundAtIntervals());
    }

    private IEnumerator PlaySequentialSoundAtIntervals()
    {
        while (true)
        {
            PlayCurrentSound();

            float interval = 10f; 
            yield return new WaitForSeconds(interval);

            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }
    }

    private void PlayCurrentSound()
    {
        audioSource.clip = audioClips[currentClipIndex];

        audioSource.Play();
    }
}
