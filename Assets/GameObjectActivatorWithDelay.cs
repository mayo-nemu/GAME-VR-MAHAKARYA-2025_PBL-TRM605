using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivatorWithDelay : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent Activated;
    public UnityEngine.Events.UnityEvent Gem;
    public float delayCount = 10f;

    public void activator()
    {
        Activated.Invoke();

        // After a delay, activate the Gem event
        Invoke("ActivateGem", delayCount);
    }

    void ActivateGem()
    {
        Gem.Invoke();
    }
}
