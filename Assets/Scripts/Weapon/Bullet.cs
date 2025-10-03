using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum DamageSource
    {
        Player,
        Enemy
    }

    private DamageSource damageSource;
    private int lifeTime = 4;
    private int damage;

    private void Start() {
        Destroy(gameObject, lifeTime);
    }
    
    public void SetDamage(int weaponDamage, DamageSource source) {
        damage = weaponDamage;
        damageSource = source;
    }
    
    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null) {
            if (damageSource == DamageSource.Enemy && health is EnemyHealth) {
                health.ApplyDamage(damage);
                Destroy(gameObject);
            }else if (damageSource == DamageSource.Player && health is PlayerHealth) {
                health.ApplyDamage(damage);
                Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
        
       
    }
}
