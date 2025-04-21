using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSequentialDestruction : MonoBehaviour
{
    public GameObject[] gameObjects; // Array GameObject
    public UnityEngine.Events.UnityEvent WhenAllGone; // Event ketika semua GameObject hancur

    private int currentIndex = 0; // Indeks GameObject yang boleh dihancurkan
    private bool hasInvoked = false; // Flag untuk menandai jika WhenAllGone sudah dipanggil

    void Update()
    {
        if (hasInvoked) return; // Jika sudah terpanggil, lewati pengecekan

        // Periksa setiap GameObject dalam daftar
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i] != null)
            {
                // Aktifkan collider hanya untuk GameObject pada currentIndex
                MeshCollider meshCollider = gameObjects[i].GetComponent<MeshCollider>();
                if (meshCollider != null)
                {
                    meshCollider.enabled = (i == currentIndex);
                }
            }
        }

        // Periksa apakah GameObject pada currentIndex sudah dihancurkan
        if (currentIndex < gameObjects.Length && gameObjects[currentIndex] == null)
        {
            currentIndex++; // Beralih ke GameObject berikutnya dalam daftar
        }

        // Jika semua GameObject dalam daftar sudah dihancurkan
        if (currentIndex >= gameObjects.Length)
        {
            WhenAllGone.Invoke();
            hasInvoked = true; // Set flag agar tidak memanggil lagi
        }
    }
}
