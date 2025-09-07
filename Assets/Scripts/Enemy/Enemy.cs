using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour
{
   [SerializeField] protected EnemyData enemyData;
   [SerializeField] protected NavMeshAgent agent;
   [SerializeField] protected Transform target;

   protected float attackDistance => enemyData.AttackDistance;
   protected float detectionDistance => enemyData.DetectionDistance;
   protected int damage => enemyData.Damage;
   protected float patrolSpeed => enemyData.PatrolSpeed;
   protected float chaseSpeed => enemyData.ChaseSpeed;

   protected float distance;
   protected float viewAngle => enemyData.ViewAngle;
   
   
   protected enum EnemyState
   {
      Patrol, 
      Chase,
      Attack
   }

   protected bool CanSeePlayer() {
      Vector3 dirToPlayer = (target.position - transform.position).normalized;
      float angle = Vector3.Angle(transform.forward, dirToPlayer);
      if (angle < viewAngle) {
         if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, 100f)) {
            Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.red);
            if (hit.collider.CompareTag("Player")) {
               Debug.Log("zamechen");
               return true;
            }
         }
      }
      
      return false;
   }

   
   protected EnemyState currentState = EnemyState.Patrol;
   
   protected virtual void Update() {
      if (target == null) {
         return;
      }
      
      distance = Vector3.Distance(transform.position, target.position);
      
      switch (currentState) {
         case EnemyState.Patrol:
            Patrol();
            if (CanSeePlayer())
               currentState = EnemyState.Chase;
            break;
         
         case EnemyState.Chase:
            Chase();
            if(distance <= attackDistance)
            currentState = EnemyState.Attack;
            else if (!CanSeePlayer())
               currentState = EnemyState.Patrol;
            break;
         case EnemyState.Attack:
            Attack();
            if (!CanSeePlayer())
               currentState = EnemyState.Patrol;
            else if (distance > attackDistance) {
               currentState = EnemyState.Chase;
            }
            break;
      }
   }
   

   protected abstract void Chase();
   protected abstract void Patrol();
   protected abstract void Attack();
   
   
   public virtual void SetTarget(Transform playerTransform) {
      target = playerTransform;
   }
   
   public virtual void OnPlayerDetected() {
      currentState = EnemyState.Chase;
   }
}
