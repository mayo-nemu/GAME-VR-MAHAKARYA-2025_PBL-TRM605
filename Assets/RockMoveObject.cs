using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMoveObject : MonoBehaviour
{
    public Transform target;   // Target posisi tujuan
    public float moveSpeed = 3f;  // Kecepatan perpindahan objek

    private Vector3 startPosition;  // Posisi awal objek
    private bool movingToTarget = true;  // Status pergerakan

    void Start()
    {
        // Simpan posisi awal objek
        startPosition = transform.position;
    }

    void Update()
    {
        // Tentukan posisi yang ingin dicapai
        Vector3 destination = movingToTarget ? target.position : startPosition;

        // Pindahkan objek menuju posisi tujuan
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // Jika objek sudah sampai ke posisi tujuan, ganti arah pergerakan
        if (transform.position == destination)
        {
            movingToTarget = !movingToTarget;
        }
    }
}

