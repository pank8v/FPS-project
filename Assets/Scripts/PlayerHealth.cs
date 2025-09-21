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

    protected bool AddHealth(float amount) {
        if (health < playerHealth) {
            float needed = playerHealth - health;
          
            if (needed < amount) {
                health = playerHealth;
            }
            else {
                health += amount;
            }
            healthBar.SetHealth(health / playerHealth);
            return true;
        }

        return false;
    }

    public bool ApplyHeal(float amount) {
        return AddHealth(amount);
    }
    
    protected override void Die() {
        Destroy(gameObject);
    }
}
