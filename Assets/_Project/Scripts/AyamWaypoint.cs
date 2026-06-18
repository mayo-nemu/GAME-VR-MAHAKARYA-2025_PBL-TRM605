using UnityEngine;

public class AyamWaypoint : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 0.2f;
    private int currentIndex = 0;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentIndex];
        Vector3 direction = (target.position - transform.position);
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            // Gerakkan ayam
            Vector3 moveDir = direction.normalized;
            transform.position += moveDir * speed * Time.deltaTime;

            // ROTASI HALUS sesuai arah jalan
            if (moveDir != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            // Set animasi jalan
            animator.SetFloat("Speed", speed);
        }
        else
        {
            // Sampai di titik, ganti ke titik selanjutnya
            currentIndex = (currentIndex + 1) % waypoints.Length;
            animator.SetFloat("Speed", 0f);
        }
    }
}
