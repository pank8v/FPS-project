using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
   [SerializeField] private GameObject enemyPrefab;
   [SerializeField] private GameObject target;
   [SerializeField] private Transform[] patrolPoints;
   private int enemyCount = 1;
   

   private void Awake() {
      for (int i = 0; i < enemyCount; i++) {
         var enemyObj  = Instantiate(enemyPrefab, transform.position, quaternion.identity);
         var EnemyMelee = enemyObj.GetComponent<MeleeEnemy>();
         var Enemy = enemyObj.GetComponent<Enemy>();
         EnemyMelee.SetPatrolPoints(patrolPoints);
         Enemy.SetTarget(target);
      }
   }
}
