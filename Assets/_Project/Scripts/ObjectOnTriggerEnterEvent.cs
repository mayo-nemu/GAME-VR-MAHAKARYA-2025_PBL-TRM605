using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnTriggerEnterEvent : MonoBehaviour
{
    public GameObject[] gameObjects; // Opsi untuk beberapa objek (jika perlu)
    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek yang masuk ke trigger adalah salah satu dari gameObjects
        foreach (GameObject obj in gameObjects)
        {
            if (other.gameObject == obj)
            {
                // Menghancurkan objek yang masuk ke trigger
                Destroy(other.gameObject);
                break;
            }
        }
    }
}
