using UnityEngine;

public class EnemyHealth : Health
{
   [SerializeField] private Enemy enemy;

   protected void Awake() {
      health = enemy.EnemyData.Health;
   }

   private void Update() {
      Debug.Log(health);
   }

   protected override void Die() {
      enemy.HandleDeath();
      this.enabled = false;
   }

   public override void ApplyDamage(int damage) {
      TakeDamage(damage);
      enemy.OnPlayerDetected();
   }
}
