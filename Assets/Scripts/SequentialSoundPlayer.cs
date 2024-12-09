using System.Collections;
using UnityEngine;

public class SequentialSoundPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Length == 0)
        {
            Debug.LogWarning("Audio Clips not assigned! Please assign Audio Clips.");
            return;
        }

        StartCoroutine(PlaySequentialSound());
    }

    private IEnumerator PlaySequentialSound()
    {
        while (true)
        {
            PlayCurrentSound();

            while (audioSource.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(10f);

            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }
    }

    private void PlayCurrentSound()
    {
        audioSource.clip = audioClips[currentClipIndex];

        audioSource.Play();
    }
}
