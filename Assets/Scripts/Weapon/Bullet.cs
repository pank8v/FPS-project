using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    public void setDamage(int weaponDamage) {
        damage = weaponDamage;
    }
    
    private void OnCollisionEnter(Collision collision) {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null) {
            health.ApplyDamage(damage);
        }

        Destroy(gameObject);
    }
}
