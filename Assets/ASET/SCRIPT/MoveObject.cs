using UnityEngine;  

public class MoveObject : MonoBehaviour  
{  
    // Public Transform yang menjadi target  
    public Transform targetTransform;  

    // Kecepatan perpindahan  
    public float moveSpeed = 5f;  
    public float rotationSpeed = 5f;  

    // Event yang akan dipanggil ketika objek selesai bergerak  
    public UnityEngine.Events.UnityEvent WhenMoveDone;  

    // Flag untuk mulai bergerak  
    private bool isMoving = false;  

    // Checkbox untuk mengaktifkan getaran  
    public bool isShaking = false;  

    // Radius dan kekuatan getaran  
    public float shakeRadius = 0.009f;  
    public float shakePower = 0.6f;  

    // Fungsi untuk memindahkan posisi dan rotasi objek secara halus ke targetTransform  
    public void Move()  
    {  
        if (targetTransform != null)  
        {  
            isMoving = true;  
        }  
        else  
        {  
            Debug.LogWarning("Target Transform belum diset!");  
        }  
    }  

    void Update()  
    {  
        if (isMoving && targetTransform != null)  
        {  
            // Pindahkan posisi secara smooth dengan kecepatan konstan menggunakan Vector3.MoveTowards  
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);  
            transform.position = targetPosition;  

            // Putar objek secara smooth dengan kecepatan konstan menggunakan Quaternion.RotateTowards  
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetTransform.rotation, rotationSpeed * Time.deltaTime);  

            // Hentikan gerakan ketika posisi dan rotasi mendekati target  
            if (Vector3.Distance(transform.position, targetTransform.position) < 0.01f &&   
                Quaternion.Angle(transform.rotation, targetTransform.rotation) < 0.01f)  
            {  
                isMoving = false;  

                // Reset posisi ke target untuk menghindari offset dari shake  
                transform.position = targetTransform.position;  

                // Invoke event WhenMoveDone ketika pergerakan selesai  
                WhenMoveDone?.Invoke();  
            }  
            else if (isShaking) // Hanya aktifkan getaran jika masih bergerak  
            {  
                Vector3 shakeOffset = Random.insideUnitSphere * shakeRadius * shakePower;  
                transform.position += shakeOffset;  
            }  
        }  
    }  
}