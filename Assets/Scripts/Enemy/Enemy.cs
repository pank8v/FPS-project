using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour
{
   [SerializeField] protected EnemyData enemyData;
   [SerializeField] protected NavMeshAgent agent;
   [SerializeField] protected Transform target;
   protected float attackDistance;
   protected float distance;
   protected float detectionDistance;

   protected enum EnemyState
   {
      Patrol, 
      Chase,
      Attack
   }

   protected EnemyState currentState = EnemyState.Patrol;

   protected void Awake() {
      attackDistance = enemyData.AttackDistance;
      detectionDistance = enemyData.DetectionDistance;
   }
   protected virtual void Update() {
      if(target != null) 
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
   
   public void SetTarget(GameObject playerTransform) {
      target = playerTransform.transform;
   }
   protected abstract void Chase();
   protected abstract void Patrol();
   protected abstract void Attack();
}
