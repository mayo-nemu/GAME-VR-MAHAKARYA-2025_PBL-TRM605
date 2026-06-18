using UnityEngine;

public class AnimatorChangerByButton : MonoBehaviour
{
    public Animator animator;
    public int startPose; // Integer untuk menyimpan pose awal
    public string[] poseBoolNames; // Array untuk menyimpan nama-nama parameter bool di Animator
    public string idleBoolName = "Idle"; // Nama parameter "Idle" di Animator
    public string dieBoolName = "Die"; // Nama parameter "Die" di Animator
    public float destroyAfterDelay = 5f; // Wait for 5 seconds before destroying the object

    void Start()
    {
        // Set all poses to false, except the starting pose
        for (int i = 0; i < poseBoolNames.Length; i++)
        {
            bool isActive = (i == startPose);
            animator.SetBool(poseBoolNames[i], isActive);
        }

        // Make sure "Idle" is active at the start
        animator.SetBool(idleBoolName, false);
    }

    // Function to change the pose based on the string array index
    public void ChangePose(int index)
    {
        if (index < 0 || index >= poseBoolNames.Length)
        {
            Debug.LogWarning("Index out of bounds. Make sure the index is within the range of the poseBoolNames array.");
            return;
        }

        // Set "Idle" to true first
        animator.SetBool(idleBoolName, true);

        for (int i = 0; i < poseBoolNames.Length; i++)
        {
            bool isActive = (i == index); // Set true for selected pose, false for others
            animator.SetBool(poseBoolNames[i], false);
            // Set "Idle" to true first
            animator.SetBool(idleBoolName, true);
        }

        // Use a coroutine to wait a short moment before setting the next pose
        StartCoroutine(SetPoseAfterIdle(index));
    }

    private System.Collections.IEnumerator SetPoseAfterIdle(int index)
    {
        // Wait for a small delay (adjust the time if needed)
        yield return new WaitForSeconds(0.7f);

        // Set all other bools in the array to false, and the selected one to true
        for (int i = 0; i < poseBoolNames.Length; i++)
        {
            bool isActive = (i == index); // Set true for selected pose, false for others
            animator.SetBool(poseBoolNames[i], isActive);
        }

        // After changing the pose, set "Idle" to false
        animator.SetBool(idleBoolName, false);

        // Check if the selected pose is "Die", if so start destruction sequence
        if (poseBoolNames[index] == dieBoolName)
        {
            StartCoroutine(DestroyAfterDelay(destroyAfterDelay)); 
        }
    }

    // Coroutine to destroy the game object after a delay
    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // Destroy the game object after delay
    }
}
