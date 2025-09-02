using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomAddHealthToPlayer : MonoBehaviour
{
    public float maxHealth = 25f;
    public float currentHealth;
    public float patrolSpeed = 3f;
    public float rotationSpeed = 5f;
    public Transform[] patrolWaypoints; // Array for waypoint transforms
    public int addHealth = 10;
    private int currentWaypointIndex = 0;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing. Please attach it to the GameObject.");
        }
        else if (!navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.enabled = true;
        }

        navMeshAgent.speed = patrolSpeed;

        SetNextWaypointDestination();

        if (animator != null)
        {
            animator.SetTrigger("isIdle");
        }
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            navMeshAgent.isStopped = true;

            Vector3 directionToNextWaypoint = (patrolWaypoints[currentWaypointIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToNextWaypoint);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            float angleToNextWaypoint = Quaternion.Angle(transform.rotation, lookRotation);
            if (angleToNextWaypoint < 5f)
            {
                navMeshAgent.isStopped = false;
                SetNextWaypointDestination();
            }
        }

        if (animator != null)
        {
            animator.SetTrigger("isPatrol");
        }
    }

    void SetNextWaypointDestination()
    {
        navMeshAgent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentHealth = 0; // Instantly kill the mushroom
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHealth(addHealth); // Add 10 health to the player
                Debug.Log("Added 10 health to player. Player Health: " + playerHealth.currentHealth);
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

        navMeshAgent.isStopped = true;
        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}