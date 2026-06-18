using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDarkSpirit : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;
    public int attackDamage = 2;
    public float attackRange = 2f;
    public float maxChaseRange = 10f;
    public float chaseSpeed = 5f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private Animator animator;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing. Please attach it to the GameObject.");
        }
        else if (!navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.enabled = true;
        }

        navMeshAgent.speed = chaseSpeed; // Set the default speed to chase speed
        initialPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (currentHealth <= 0)
        {
            ActivateDeathAnimation();
        }
        else if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= maxChaseRange)
        {
            ChasePlayer();
        }
        else
        {
            ReturnToInitialPosition();
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);

        if (animator != null)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
        }
    }

    void ReturnToInitialPosition()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(initialPosition);

        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            navMeshAgent.isStopped = true;

            if (animator != null)
            {
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Idle", true);
            }
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (animator != null)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Attack", true);
            }
            Invoke("ApplyDamage", 0.5f); // Delay to sync with the animation
            lastAttackTime = Time.time;
        }
    }

    void ApplyDamage()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Player Health: " + playerHealth.currentHealth);
            }
        }
        // Reset the attack animation
        if (animator != null)
        {
            animator.SetBool("Attack", false);
        }
    }

    public void ActivateDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Die", true);
            navMeshAgent.isStopped = true;
            StartCoroutine(DestroyAfterDeathAnimation());
        }

    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
