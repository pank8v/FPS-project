using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Patrol")]
    [SerializeField] protected Transform[] patrolPoints;
    [SerializeField] protected float waitingTime = 5f;
    protected int currentPatrolPoint = 0;
    protected float waitingTimer = 0;
    protected bool isWaiting = false;    
    
    
    
    
    protected override void Patrol() {
        agent.isStopped = false;
        if (patrolPoints.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) {
            if (!isWaiting) {
                isWaiting = true;
                waitingTimer = waitingTime;
            }
        }
        if (isWaiting) {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0) {
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                isWaiting = false;
            }
        }
        Debug.Log("Patrol");
    }

    
    
    protected override void Chase() {
        agent.isStopped = false;
        agent.SetDestination(target.position);
        Debug.Log("Chase");
    }

    protected override void Attack() {
        agent.isStopped = true;
        Debug.Log("attack");
    }

    public void SetPatrolPoints(Transform[] points) {
        patrolPoints = points;
    }
    
    
}
