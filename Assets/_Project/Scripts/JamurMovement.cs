using UnityEngine;

public class JamurMovement : MonoBehaviour
{
    public Transform[] targetPoints;  // Daftar titik tujuan
    public float speed = 2f;          // Kecepatan gerak
    public float reachDistance = 0.1f; // Jarak untuk berpindah ke titik berikutnya

    private int currentPointIndex = 0;

    void Update()
    {
        if (targetPoints.Length == 0) return;

        Transform target = targetPoints[currentPointIndex];

        // Gerak menuju titik saat ini
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Hadap ke arah target (opsional)
        Vector3 direction = target.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }

        // Jika sudah dekat dengan titik tujuan, pindah ke titik berikutnya
        if (Vector3.Distance(transform.position, target.position) < reachDistance)
        {
            currentPointIndex++;

            // Ulang dari awal jika sudah sampai titik terakhir
            if (currentPointIndex >= targetPoints.Length)
            {
                currentPointIndex = 0;
            }
        }
    }
}
