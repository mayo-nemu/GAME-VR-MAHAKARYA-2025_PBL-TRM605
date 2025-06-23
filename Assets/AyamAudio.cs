using UnityEngine;

public class AyamAudio : MonoBehaviour
{
    public AudioSource ayamSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!ayamSound.isPlaying)
            {
                ayamSound.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ayamSound.isPlaying)
            {
                ayamSound.Stop();
            }
        }
    }
}
