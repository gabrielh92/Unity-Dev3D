using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 3f;

    Transform target;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead")) {
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if(isProvoked) {
                FaceTarget();
                if(distanceToTarget > navMeshAgent.stoppingDistance) {
                    ChaseTarget();
                } else {
                    AttackTarget();
                }
            } else if(distanceToTarget < chaseRange) {
                isProvoked = true;
            }
        } else {
            navMeshAgent.SetDestination(gameObject.transform.position);
        }
    }

    public void OnDamageTaken() {
        isProvoked = true;
    }

    void ChaseTarget() { 
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget() {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);    
    }
}
