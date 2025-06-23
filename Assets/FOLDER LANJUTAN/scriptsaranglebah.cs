using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptsaranglebah : MonoBehaviour
{
    public float maxHealth = 25f;
    public float currentHealth;
    public int addHealth = 10;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetTrigger("isIdle");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentHealth = 0; // Instantly deactivate the mushroom
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHealth(addHealth); // Add health to player
                Debug.Log("Added " + addHealth + " health to player. Player Health: " + playerHealth.currentHealth);
            }
            ActivateDeathAnimation();
        }
    }

    private void ActivateDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("isDie");
        }

        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}