using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected int health;

    protected void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) Die();
    }
    
    protected abstract void Die();

    public void ApplyDamage(int damage) {
        TakeDamage(damage);
    }
}
