using UnityEngine;

public class LiftZipline : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject childObject;

    public void RunLift()
    {

        if (parentObject != null && childObject != null)
        {
            childObject.transform.parent = parentObject.transform;
        }
        else
        {
            Debug.LogError("Parent or child GameObjects are not assigned.");
        }
        
    }

    public void LiftStopped()
    {
        if (parentObject != null && childObject != null)
        {
            childObject.transform.SetParent(null);
        }
    }
}
