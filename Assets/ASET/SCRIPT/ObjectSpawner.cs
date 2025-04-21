using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Public object to spawn
    public GameObject objectToSpawn;

    // Public transform to use as spawn point (position excluding Y axis)
    public Transform spawnPoint;

    // Public function to spawn the object at the given transform, excluding Y axis and rotation
    public void SpawnObject()
    {
        if (objectToSpawn != null && spawnPoint != null)
        {
            // Get the original position of the object
            Vector3 originalPosition = objectToSpawn.transform.position;

            // Set new spawn position using X and Z from spawnPoint, but Y from the original position
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, originalPosition.y, spawnPoint.position.z);

            // Instantiate the object at the modified spawn position and original rotation
            Instantiate(objectToSpawn, spawnPosition, objectToSpawn.transform.rotation);
        }
        else
        {
            Debug.LogError("Object to spawn or spawn point is missing!");
        }
    }
}
