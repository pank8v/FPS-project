using System;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform enemyMuzzle;
    private float nextTimeToFire = 0;


  

    protected override void Attack() {
        agent.isStopped = true;
        if (nextTimeToFire <= Time.time) {
            Vector3 direction = (target.transform.position - enemyMuzzle.position);
            GameObject bullet = Instantiate(bulletPrefab, enemyMuzzle.position, Quaternion.LookRotation(direction));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            Debug.Log("attack");
            bulletScript.setDamage(damage);
            bulletRb.linearVelocity = direction * 50f;
            nextTimeToFire = Time.time + attackRate;
        }

    }
}
