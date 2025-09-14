using System;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform enemyMuzzle;
    private float nextTimeToFire = 0;
    private float rotationSmooth = 20;


  

    protected override void Attack() {
        Vector3 direction = (target.position - enemyMuzzle.position).normalized;
        Debug.Log(direction);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSmooth);

        agent.isStopped = true;
        if (nextTimeToFire <= Time.time) {
            GameObject bullet = Instantiate(bulletPrefab, enemyMuzzle.position, Quaternion.LookRotation(direction));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.setDamage(damage);
            bulletRb.linearVelocity = direction * 7f;
            nextTimeToFire = Time.time + attackRate;
        }

    }

    

}
