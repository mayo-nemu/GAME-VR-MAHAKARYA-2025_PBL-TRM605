using UnityEngine;

public class GameObjectWiggling : MonoBehaviour
{
    // Durasi goyangan dalam detik (sekarang tipe float)
    public float WiggleDuration = 1f; 

    // Kekuatan goyangan
    public float WigglePower = 0.1f; 

    // Kecepatan goyangan
    public float WiggleSpeed = 10f; 

    // Posisi awal objek sebelum goyangan
    private Vector3 initialPosition;

    // Timer dan flag untuk mengontrol goyangan
    private float timer;
    private bool isWiggling = false;

    private void Start()
    {
        // Menyimpan posisi awal objek
        initialPosition = transform.position;
    }

    // Fungsi untuk memulai goyangan
    public void Wiggle()
    {
        // Reset timer dan aktifkan flag
        timer = 0f;
        isWiggling = true;
    }

    private void Update()
    {
        // Lakukan goyangan hanya jika flag aktif
        if (isWiggling)
        {
            // Jika timer masih kurang dari durasi goyangan
            if (timer < WiggleDuration)
            {
                // Menambahkan waktu per frame
                timer += Time.deltaTime;

                // Menghitung posisi goyangan dengan mempertimbangkan kecepatan goyangan
                float wiggleX = Mathf.Sin(Time.time * WiggleSpeed) * WigglePower;
                float wiggleY = Mathf.Cos(Time.time * WiggleSpeed) * WigglePower;

                // Goyangkan objek berdasarkan posisi awalnya
                transform.position = new Vector3(initialPosition.x + wiggleX, initialPosition.y + wiggleY, initialPosition.z);
            }
            else
            {
                // Menghentikan goyangan setelah durasi berakhir
                transform.position = initialPosition;
                isWiggling = false; // Matikan flag
            }
        }
    }
}
