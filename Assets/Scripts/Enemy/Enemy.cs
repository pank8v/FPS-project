using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour
{
   [SerializeField] protected EnemyData enemyData;
   public EnemyData EnemyData => enemyData;
   [SerializeField] protected NavMeshAgent agent;
   [SerializeField] protected Transform target;
   [SerializeField] protected Animator animator;
   [SerializeField] private GameObject weaponObject;
   [SerializeField] private RagdollController ragdollController;
   
   [Header("Patrol")]
   [SerializeField] protected Transform[] patrolPoints;
   [SerializeField] protected float waitingTime = 5f;
   protected int currentPatrolPoint = 0;
   protected float waitingTimer = 0;
   protected bool isWaiting = false;    


   [Header("EnemyData")]
   protected float attackDistance => enemyData.AttackDistance;
   protected float viewDistance => enemyData.ViewDistance;

   protected int damage => enemyData.Damage;
   protected float patrolSpeed => enemyData.PatrolSpeed;
   protected float chaseSpeed => enemyData.ChaseSpeed;

   protected float distance;
   protected float viewAngle => enemyData.ViewAngle;
   protected float attackRate => enemyData.AttackRate;
   
   
   
   protected enum EnemyState
   {
      Patrol, 
      Chase,
      Attack
   }
   
   protected EnemyState currentState = EnemyState.Patrol;

   
   protected bool CanSeePlayer() {
      
      Vector3 dirToPlayer = (target.position - transform.position).normalized;
      float angle = Vector3.Angle(transform.forward, dirToPlayer);
      if (angle < viewAngle) {
         if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, viewDistance)) {
            if (hit.collider.CompareTag("Player")) {
               return true;
            }
         }
      }
      return false;
   }

   
   
   protected virtual void Update() {
      HandleMovementAnimations();
      
      if (target == null) return;
      distance = Vector3.Distance(transform.position, target.position);
      HandleState();

   }

   protected void HandleState() {
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

   
   protected virtual void Patrol() {
      if (patrolPoints.Length == 0) return;
      agent.isStopped = false;
      agent.speed = patrolSpeed;
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
   }
  
   protected virtual void Chase() {
      agent.isStopped = false;
      agent.speed = chaseSpeed;
      agent.SetDestination(target.position);
   }


   
   protected abstract void Attack();
   
   
   
   
   public void HandleDeath() {
      SpawnWeapon();
      animator.SetBool("Dead", true);
      if (agent) {
         agent.enabled = false;
      }
      ragdollController.SetRagDoll(true);
      
      this.enabled = false;
   }

   
   private void SpawnWeapon() {
      if (weaponObject != null) {
         Instantiate(weaponObject, transform.position, Quaternion.identity);
      }
   }
   
   
   
   public  void SetTarget(Transform playerTransform) {
      target = playerTransform;
   }
   
   public  void OnPlayerDetected() {
      currentState = EnemyState.Chase;
   }
   
   public void SetPatrolPoints(Transform[] points) {
      patrolPoints = points;
   }


   private void HandleMovementAnimations() {
      float currentVelocity = agent.velocity.magnitude;
      animator.SetFloat("Speed", currentVelocity);
   }

}
