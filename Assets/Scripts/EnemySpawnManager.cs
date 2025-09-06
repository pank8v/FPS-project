using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
   [SerializeField] private GameObject enemyPrefab;
   [SerializeField] private Transform target;
   [SerializeField] private Transform[] patrolPoints;
   private int enemyCount = 1;
   

   private void Awake() {
      for (int i = 0; i < enemyCount; i++) {
         var enemyObj  = Instantiate(enemyPrefab, transform.position, quaternion.identity);
         var EnemyMelee = enemyObj.GetComponent<MeleeEnemy>();
         EnemyMelee.SetPatrolPoints(patrolPoints);
         EnemyMelee.SetTarget(target);
      }
   }
}
