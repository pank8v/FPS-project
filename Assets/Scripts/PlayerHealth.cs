using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] protected float playerHealth = 100;

    protected void Awake() {
        health = playerHealth;
    }

     protected override void TakeDamage(int damage) {
         health -= damage;
         healthBar.SetHealth(health / playerHealth);
         if(health <= 0) Die();
    }
    
    
    protected override void Die() {
        Destroy(gameObject);
    }
}
