using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [Header("Patroling options")]
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private float walkPointRange;
    [Header("Atacking options")]
    [SerializeField] private float timeBetweenAttacks;
    [Header("States options")]
    [SerializeField] private float sightRange, attackRange;

    private Transform player;
    private NavMeshAgent agent;
    private bool walkPointSet;
    private bool alreadyAttacked;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) { Patroling(); }
        if (playerInSightRange && !playerInAttackRange) { ChasePlayer(); }
        if (playerInSightRange && playerInAttackRange) { AtackPlayer(); }
    }

    private void Patroling()
    {
        Debug.Log(gameObject.name + ": Patroluję");
        if (!walkPointSet) { ScherchWalkPoint(); }

        if (walkPointSet) { agent.SetDestination(walkPoint); }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) { walkPointSet = false; }
    }

    private void ScherchWalkPoint()
    {
        Debug.Log(gameObject.name + ": Szukam punktu");
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) { walkPointSet = true; }
    }

    private void ChasePlayer()
    {
        Debug.Log(gameObject.name + ": Śledzę");
        agent.SetDestination(player.position);
    }

    private void AtackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //skrypt ataku
            Debug.Log(gameObject.name + ": Atakuję gracza");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
