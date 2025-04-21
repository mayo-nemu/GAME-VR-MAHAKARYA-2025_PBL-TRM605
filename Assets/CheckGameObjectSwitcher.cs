using UnityEngine;
using UnityEngine.Events;

public class CheckGameObjectSwitcher : MonoBehaviour
{
    public GameObject[] switcherObjects; // Array of GameObjects with GameObjectSwitcher
    public UnityEvent AllOn;             // Event to invoke when all switches are ON

    private GameObjectSwitcher[] switchers;
    private bool isAllOnInvoked = false; // Tracks if AllOn event has been invoked

    private void Start()
    {
        // Collect all GameObjectSwitcher components from the switcherObjects
        switchers = new GameObjectSwitcher[switcherObjects.Length];
        for (int i = 0; i < switcherObjects.Length; i++)
        {
            switchers[i] = switcherObjects[i].GetComponent<GameObjectSwitcher>();
            if (switchers[i] == null)
            {
                Debug.LogError($"GameObject {switcherObjects[i].name} does not have a GameObjectSwitcher component.");
            }
        }
    }

    private void Update()
    {
        // If event has already been invoked, skip further checks
        if (isAllOnInvoked)
            return;

        // Check if all switches are ON
        bool allSwitchedOn = true;
        foreach (GameObjectSwitcher switcher in switchers)
        {
            if (switcher == null || !switcher.isSwitchedOn)
            {
                allSwitchedOn = false;
                break;
            }
        }

        // Invoke AllOn event if all switches are ON and hasn't been invoked yet
        if (allSwitchedOn)
        {
            Debug.Log("All switches are ON. Invoking AllOn event.");
            AllOn.Invoke();
            isAllOnInvoked = true; // Mark event as invoked
        }
    }
}
