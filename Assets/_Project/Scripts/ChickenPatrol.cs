using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenPatrol : MonoBehaviour
{
    public float patrolSpeed = 3f;
    public float rotationSpeed = 5f;
    public Transform[] patrolWaypoints; // Array for waypoint transforms
    private int currentWaypointIndex = 0;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
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
}
