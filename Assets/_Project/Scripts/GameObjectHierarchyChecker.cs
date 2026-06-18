using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectHierarchyChecker : MonoBehaviour
{
    public GameObject[] gameObjects; // Array gameObject
    public UnityEngine.Events.UnityEvent WhenGone;

    private bool hasInvoked = false; // Flag untuk menandai jika WhenGone sudah dipanggil

    // Update is called once per frame
    void Update()
    {
        if (hasInvoked) return; // Jika sudah terpanggil, lewati pengecekan

        bool allObjectsGone = true;

        // Cek status setiap GameObject dalam array
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null && obj.activeInHierarchy) 
            {
                allObjectsGone = false;
                break;
            }
        }

        // Jika semua objek sudah tidak ada atau tidak aktif
        if (allObjectsGone)
        {
            WhenGone.Invoke();
            hasInvoked = true; // Set flag agar tidak memanggil lagi
        }
    }
}
