using UnityEngine;

public class EnemyHealth : Health
{
   [SerializeField] private Enemy enemy;

   protected void Awake() {
      health = enemy.EnemyData.Health;
   }

   protected override void Die() {
      Destroy(gameObject);
   }

   public override void ApplyDamage(int damage) {
      TakeDamage(damage);
      enemy.OnPlayerDetected();
   }
}
