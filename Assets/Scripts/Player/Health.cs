using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected float health;
    
     protected virtual void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) Die();
    }
    
    protected abstract void Die();

    public virtual void ApplyDamage(int damage) {
        TakeDamage(damage);
    }
}
