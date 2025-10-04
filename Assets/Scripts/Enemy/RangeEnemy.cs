using System;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform enemyMuzzle;
    [SerializeField] protected float bulletSpeed = 7f;
    private float nextTimeToFire = 0;
    private float rotationSmooth = 20;


  

    protected override void Attack() {
        Vector3 direction = (target.position - enemyMuzzle.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSmooth);
        agent.destination = agent.transform.position;
       // agent.isStopped = true;
        if (nextTimeToFire <= Time.time && Vector3.Distance(transform.position, target.position) <= attackDistance) {
            animator.SetTrigger("Shoot");
            enemySoundManager.PlayAttackCliP();
            GameObject bullet = Instantiate(bulletPrefab, enemyMuzzle.position, Quaternion.LookRotation(direction));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDamage(damage, Bullet.DamageSource.Player);
            bulletRb.linearVelocity = direction * bulletSpeed;
            nextTimeToFire = Time.time + attackRate;
        }

    }

    
    

}
