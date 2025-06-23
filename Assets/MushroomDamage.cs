using System.Collections;
using UnityEngine;

public class MushroomDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 1f;
    private bool canDamage = true;

    private void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHealth(damageAmount);
                Debug.Log("Player hit by mushroom. Current Health: " + playerHealth.currentHealth);
                StartCoroutine(DamageCooldown());
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
