using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
   [SerializeField] private GameObject meleeEnemy;
   [SerializeField] private GameObject rangeEnemy;
   [SerializeField] private Transform target;
   [SerializeField] private Transform[] patrolPoints;
   [SerializeField] private int enemyCount;
   

   private void Awake() {
      for (int i = 0; i < enemyCount; i++) {
         var enemyObj  = Instantiate(rangeEnemy, transform.position, quaternion.identity);
         var EnemyMelee = enemyObj.GetComponent<RangeEnemy>();
         EnemyMelee.SetPatrolPoints(patrolPoints);
         EnemyMelee.SetTarget(target);
      }
   }
}
