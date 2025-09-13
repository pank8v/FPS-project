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
        Debug.Log(collision.gameObject.name);
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null) {
            health.ApplyDamage(damage);
        }
        Destroy(gameObject);
    }
}
