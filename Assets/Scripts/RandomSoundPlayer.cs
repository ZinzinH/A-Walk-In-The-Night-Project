using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Length == 0)
        {
            Debug.LogWarning("Audio Clips not assigned! Please assign Audio Clips.");
            return;
        }

        StartCoroutine(PlayRandomSoundAtIntervals());
    }

    private IEnumerator PlayRandomSoundAtIntervals()
    {
        while (true)
        {
            PlayRandomSound();

            while (audioSource.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(10f);
        }
    }

    private void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, audioClips.Length);

        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
    }
}
