using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int lifeTime = 5;
    private int damage;

    private void Update() {
        Destroy(gameObject, lifeTime);
    }
    
    public void setDamage(int weaponDamage) {
        damage = weaponDamage;
    }
    
    private void OnCollisionEnter(Collision collision) {
        Debug.Log(damage);
        Debug.Log(collision.gameObject.name);
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            Health health = enemy.GetComponent<Health>();
            health.ApplyDamage(damage);
            enemy.OnPlayerDetected();
        }
        Destroy(gameObject);
    }
}
