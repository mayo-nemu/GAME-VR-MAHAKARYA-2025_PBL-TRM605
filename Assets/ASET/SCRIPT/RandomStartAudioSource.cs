using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomStartAudioSource : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Mengambil komponen AudioSource dari game object yang disematkan script ini
        audioSource = GetComponent<AudioSource>();
    }

    // Public function untuk memainkan audio dari titik acak
    public void Play()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            // Mengatur titik mulai acak dalam durasi audio
            float randomStartTime = Random.Range(0f, audioSource.clip.length);
            audioSource.time = randomStartTime;
            
            // Memulai audio dari titik acak
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource atau AudioClip tidak tersedia.");
        }
    }
}
