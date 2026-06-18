using UnityEngine;

public class DestroyItSelf : MonoBehaviour
{
    // Public function to destroy the attached game object
    public void Destroy()
    {
        Destroy(gameObject);  // Destroy the game object this script is attached to
    }
}
