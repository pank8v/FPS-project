using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] protected int playerHealth;

    protected void Awake() {
        health = playerHealth;
    }

    protected override void Die() {
        Destroy(gameObject);
    }
}
