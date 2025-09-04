using UnityEngine;

public class EnemyHealth : Health
{
   [SerializeField] private EnemyData enemyData;

   protected void Awake() {
      if (enemyData != null) {
         health = enemyData.Health;
      }
   }

   protected override void Die() {
      Destroy(gameObject);
   }
}
