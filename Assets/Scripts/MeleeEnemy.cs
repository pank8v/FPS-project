using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{

    [SerializeField] protected Transform[] patrolPoints;
    [SerializeField] protected float waitingTime = 5f;

    private float waitingTimer = 0;
    private int currentPatrolPoint = 0;
    private bool isWaiting = false;    
    
    
    
    
    protected override void Patrol() {
        if (patrolPoints.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) {
            if (!isWaiting) {
                isWaiting = true;
                waitingTimer = waitingTime;
            }
        }
        if (isWaiting) {
            waitingTimer -= Time.deltaTime;
            Debug.Log(waitingTimer);
            if (waitingTimer <= 0) {
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                isWaiting = false;
            }
        }
    }

    protected override void Chase() {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < attackDistance) {
            agent.isStopped = true;
        }
        else {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        Debug.Log("Chase");
    }

    protected override void Attack() {
        Debug.Log("attack");
    }
    
}
