using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path by spawning an object at intervals.
    // The object moves along the path and gets destroyed after 2 seconds.
    public class PathFollowerBySpawn : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public GameObject objectToFollow;  // GameObject to spawn
        public float speed = 5f;
        public float spawnInterval = 2f;  // Interval for spawning object
        public float destroyTime = 5f;
        private float distanceTravelled;
        private float spawnTimer;

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            spawnTimer = spawnInterval;  // Initialize spawn timer
        }

        void Update()
        {
            // Spawn objects at regular intervals
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnObject();
                spawnTimer = spawnInterval;  // Reset spawn timer
            }
        }

        // Method to spawn and move the object along the path
        void SpawnObject()
        {
            if (pathCreator != null && objectToFollow != null)
            {
                // Instantiate the object
                GameObject spawnedObject = Instantiate(objectToFollow, pathCreator.path.GetPointAtDistance(0), Quaternion.identity);
                // Start the movement coroutine
                StartCoroutine(MoveObjectAlongPath(spawnedObject));
                // Destroy the object after 2 seconds
                Destroy(spawnedObject, destroyTime);
            }
        }

        // Coroutine to move the spawned object along the path
        System.Collections.IEnumerator MoveObjectAlongPath(GameObject obj)
        {
            float distance = 0f;
            while (distance < pathCreator.path.length)
            {
                distance += speed * Time.deltaTime;
                obj.transform.position = pathCreator.path.GetPointAtDistance(distance, endOfPathInstruction);
                obj.transform.rotation = pathCreator.path.GetRotationAtDistance(distance, endOfPathInstruction);
                yield return null;
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}
