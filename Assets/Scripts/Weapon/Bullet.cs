using UnityEngine;

public class Bullet : MonoBehaviour
{

    private BulletPool bulletPool;
    public enum DamageSource
    {
        Player,
        Enemy
    }

    private DamageSource damageSource;
    private int lifeTime = 4;
    private int damage;
    private float timer;



    private void Update() {
      timer += Time.deltaTime;
        if (timer >= lifeTime) {
            ReturnToPool();
        }
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
                ReturnToPool();
            }else if (damageSource == DamageSource.Player && health is PlayerHealth) {
                health.ApplyDamage(damage);
               ReturnToPool();
            }
        }
        else {
            ReturnToPool();
        }
    }

    public void SetPool(BulletPool pool) {
        bulletPool = pool;
    }

    private void ReturnToPool() {
        bulletPool.ReturnBullet(this);
    }
}
