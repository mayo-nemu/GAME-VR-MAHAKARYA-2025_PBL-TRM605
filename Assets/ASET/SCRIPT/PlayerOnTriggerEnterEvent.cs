using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnTriggerEnterEvent : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent WhenIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WhenIn.Invoke();
        }
    }
}
