using UnityEngine;

public class Birdmusic : MonoBehaviour
{
    public AudioClip sound;
    public float delay = 20f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlaySound", delay);
    }

    private void PlaySound()
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}