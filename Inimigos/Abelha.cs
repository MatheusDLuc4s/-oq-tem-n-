using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Abelha : MonoBehaviour
{
   public NavMeshAgent agent;
   public Transform player;
   public LayerMask whatIsGround, WhatIsPlayer;

   //Patrulhando
   public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;

   //attacking
   public float timebetweenAttacks;
   bool alreadyAttacked;
   public GameObject projectile;

   //States
   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange;

   private void Awake()
   {
     player = GameObject.Find("PlayerObj").transform;
     agent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
        //vê se o jogador está a vista
        playerInSightRange= Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange= Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
        
   }

   private void Patroling()
   {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
    
        //chegou no destino
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
   }

   private void SearchWalkPoint()
   {
    // posiçoes aleatorias para andar
    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    float randomX = Random.Range(-walkPointRange, walkPointRange);

    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
   }

   private void ChasePlayer()
   {
        agent.SetDestination(player.position);
   }

   private void AttackPlayer()
   {
     agent.SetDestination(transform.position);

     transform.LookAt(player);

     if(!alreadyAttacked)
     {
          Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
          rb.AddForce(transform.forward * 32, ForceMode.Impulse);
          
          alreadyAttacked = true;
          Invoke(nameof(ResetAttack), timebetweenAttacks);

     }
   }

   private void ResetAttack()
   {
     alreadyAttacked =false;
   }

   //areas
   private void onDrawGizmosSelected()
   {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(transform.position, attackRange);
     Gizmos.color = Color.yellow;
     Gizmos.DrawWireSphere(transform.position, sightRange);
   }

}
