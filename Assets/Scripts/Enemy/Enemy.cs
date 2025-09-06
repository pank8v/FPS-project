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
   
   

   protected enum EnemyState
   {
      Patrol, 
      Chase,
      Attack
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
            if (distance <= detectionDistance)
               currentState = EnemyState.Chase;
            break;
         
         case EnemyState.Chase:
            Chase();
            if(distance <= attackDistance)
            currentState = EnemyState.Attack;
            else if (distance > detectionDistance)
               currentState = EnemyState.Patrol;
            break;
         case EnemyState.Attack:
            Attack();
            if (distance > detectionDistance)
               currentState = EnemyState.Patrol;
            break;
      }
   }
   
   public void SetTarget(Transform playerTransform) {
      target = playerTransform;
   }
   protected abstract void Chase();
   protected abstract void Patrol();
   protected abstract void Attack();
}
