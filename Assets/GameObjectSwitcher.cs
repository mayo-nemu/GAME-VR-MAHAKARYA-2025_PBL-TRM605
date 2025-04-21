using UnityEngine;
using UnityEngine.Events;

public class GameObjectSwitcher : MonoBehaviour
{
    public GameObject[] targetObjects; // Public array of GameObjects
    public bool isSwitchedOn = false;  // Switch status
    public UnityEvent WhenIn;          // Event triggered when an object enters

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is in the targetObjects array
        foreach (GameObject obj in targetObjects)
        {
            if (other.gameObject == obj)
            {
                isSwitchedOn = true; // Switch ON
                Debug.Log($"{obj.name} entered, switch ON.");

                // Invoke the event
                WhenIn.Invoke();

                // Destroy the object
                Destroy(other.gameObject);
                break;
            }
        }
    }
}
