using System.Collections;
using UnityEngine;

public class MoveObjectWithPattern : MonoBehaviour
{
    public Transform[] targetTransforms; // Array untuk target transform
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;
    public float delayBetweenMoves = 0f; // Delay di antara perpindahan

    public enum MovePattern { Circle, Reverse }
    public MovePattern movePattern;

    private int currentTargetIndex = 0;
    private bool isReversing = false; // Untuk mode Reverse
    private bool isWaiting = false; // Untuk delay di antara perpindahan

    void Start()
    {
        if (targetTransforms.Length == 0)
        {
            Debug.LogError("No target transforms set.");
        }
    }

    void Update()
    {
        if (targetTransforms.Length > 0 && !isWaiting)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        Transform currentTarget = targetTransforms[currentTargetIndex];

        // Pindahkan posisi objek secara smooth menggunakan Vector3.MoveTowards
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        // Putar objek secara smooth menggunakan Quaternion.RotateTowards
        transform.rotation = Quaternion.RotateTowards(transform.rotation, currentTarget.rotation, rotationSpeed * Time.deltaTime);

        // Jika posisi dan rotasi mendekati target
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f &&
            Quaternion.Angle(transform.rotation, currentTarget.rotation) < 0.01f)
        {
            StartCoroutine(WaitBeforeNextMove()); // Jalankan Coroutine untuk delay
        }
    }

    IEnumerator WaitBeforeNextMove()
    {
        isWaiting = true; // Set waiting state agar tidak bergerak sementara

        // Tunggu sesuai dengan delayBetweenMoves
        yield return new WaitForSeconds(delayBetweenMoves);

        // Setelah delay, lanjutkan perpindahan target
        if (movePattern == MovePattern.Circle)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetTransforms.Length; // Kembali ke 0 setelah mencapai target terakhir
        }
        else if (movePattern == MovePattern.Reverse)
        {
            if (!isReversing)
            {
                currentTargetIndex++;
                if (currentTargetIndex >= targetTransforms.Length - 1)
                {
                    isReversing = true; // Jika sudah mencapai target terakhir, ubah arah
                }
            }
            else
            {
                currentTargetIndex--;
                if (currentTargetIndex <= 0)
                {
                    isReversing = false; // Jika sudah mencapai target pertama, ubah arah lagi
                }
            }
        }

        isWaiting = false; // Setelah delay selesai, reset waiting state
    }
}
