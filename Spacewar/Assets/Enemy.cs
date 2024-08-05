using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround,whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public GameObject projectile;

    public float health;

    public float sightRange, attackRange;
    public bool playerInSightRange,playerInAttackRange;

    private void Awake(){
        player = GameObject.Find("PlayerOnj").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Uodate(){
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling(){
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }
    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z+randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
             walkPointSet = true;
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }
    private void AttackPlayer(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){
            //Attack code here
            Rigidbody rb = Instantiate(projectile,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward*32f,ForceMode.Impulse);
            rb.AddForce(transform.up*8f,ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDamege(int damage){
        health -= damage;

        if(health <=0) Invoke(nameof(DestroyEnemy),0.5f);
    }
    private void DestroyEnemy(){
        Destroy(gameObject);
    }
}   
