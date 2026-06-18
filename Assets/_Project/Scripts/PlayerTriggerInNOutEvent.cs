using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerInNOutEvent : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent WhenIn;
    public UnityEngine.Events.UnityEvent WhenOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WhenIn.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WhenOut.Invoke();
        }
    }
}
