using System.Collections;
using UnityEngine;

public class PutBackItem : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public float transitionDuration = 2.0f; // Duration of the smooth transition

    void Start()
    {
        // Store the parent's initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Public function to start the smooth transition back to the initial position and rotation
    public void MoveParentBackToInitial()
    {
        StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove()
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionDuration)
        {
            // Calculate the interpolation ratio
            float t = elapsedTime / transitionDuration;

            // Interpolate position and rotation
            transform.position = Vector3.Lerp(startPosition, initialPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, initialRotation, t);

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure the final position and rotation are exactly the initial values
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
