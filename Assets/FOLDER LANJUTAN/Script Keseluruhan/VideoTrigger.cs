using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoScreen; // Objek layar video (RawImage atau Plane)

    void Start()
    {
        videoScreen.SetActive(false); // Sembunyikan layar saat awal
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoScreen.SetActive(true); // Tampilkan layar
            videoPlayer.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Pause();
            videoScreen.SetActive(false); // Sembunyikan layar lagi
        }
    }
}
