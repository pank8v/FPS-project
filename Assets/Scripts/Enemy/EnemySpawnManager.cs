using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemySpawnManager : MonoBehaviour
{
   [SerializeField] private GameObject[] enemies;
   [SerializeField] private Transform target;
   [SerializeField] private Transform[] patrolPoints;
   [SerializeField] private GameObject[] routes;
   [SerializeField] private Transform[] spawnPositions;
   [SerializeField] private int enemyCount;
   

   private void Awake() {
      for (int i = 0; i < enemyCount; i++) {
         var enemyPrefab = enemies[UnityEngine.Random.Range(0, enemies.Length)];
         var enemyObj  = Instantiate(enemyPrefab, spawnPositions[i].position, quaternion.identity);
         var enemy = enemyObj.GetComponent<Enemy>();

         var route = routes[i % routes.Length];
         var patrolPoints = route.GetComponentsInChildren<Transform>().Skip(1).ToArray();
         
         enemy.SetPatrolPoints(patrolPoints);
         enemy.SetTarget(target);
      }
   }
   
   
}
