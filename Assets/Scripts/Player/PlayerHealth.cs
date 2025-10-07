using System;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] protected float maxHealth = 100;
    public event Action<float> OnPlayerHealthChanged;
    public event Action OnPlayerDeath;
    
    protected void Awake() {
        health = maxHealth;
    }

     protected override void TakeDamage(int damage) {
         health -= damage;
         OnPlayerHealthChanged?.Invoke(health / maxHealth);
         if(health <= 0) Die();
    }

    public bool AddHealth(float amount) {
        if (health < maxHealth) {
            float needed = maxHealth - health;
          
            if (needed < amount) {
                health = maxHealth;
            }
            else {
                health += amount;
            }
            OnPlayerHealthChanged?.Invoke(health / maxHealth);
            return true;
        }

        return false;
    }
    
    
    protected override void Die() {
      // Destroy(gameObject);
        OnPlayerDeath?.Invoke();
    }
}
