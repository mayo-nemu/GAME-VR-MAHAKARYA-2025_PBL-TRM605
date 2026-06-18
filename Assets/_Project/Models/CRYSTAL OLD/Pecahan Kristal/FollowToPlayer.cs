using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPlayer : MonoBehaviour
{
    public float speed = 5f;  // Speed at which the object will move towards the player
    private GameObject player;  // Reference to the player object
    private Rigidbody rb;  // Reference to the Rigidbody component
    private Collider collider;  // Reference to the Collider component
    private AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        // Start the coroutine that waits for 2 seconds before starting to fly towards the player
        StartCoroutine(FlyToPlayerAfterDelay());

        // Get the Rigidbody and Collider components to control gravity and collision behavior
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
        
        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator FlyToPlayerAfterDelay()
    {
        yield return new WaitForSeconds(2f);  // Wait for 2 seconds

        // Disable gravity after 2 seconds
        if (rb != null && collider != null)
        {
            rb.useGravity = false;  // Turn off gravity
            collider.isTrigger = true;
        }

        // Play the audio source after the delay
        if (audioSource != null)
        {
            audioSource.Play();  // Start playing the audio
        }

        // Find the player object by its tag
        player = GameObject.FindGameObjectWithTag("Tangan");

        // Check if the player was found
        if (player != null)
        {
            // Move the current object towards the player
            while (true)
            {
                // Calculate the direction to the player
                Vector3 direction = (player.transform.position - transform.position).normalized;

                // Move towards the player
                transform.position += direction * speed * Time.deltaTime;

                // Continue moving in the next frame
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the player has the tag 'Tangan'.");
        }
    }

    // Check for collision with the player
    void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the tag 'Tangan'
        if (other.CompareTag("Tangan"))
        {
            // Stop the audio when colliding with the player
            if (audioSource != null)
            {
                audioSource.Stop();  // Stop the audio
            }

            Destroy(gameObject);  // Destroy this object
        }
    }
}
