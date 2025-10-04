using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float nextTimeToAttack = 0f;
    
    protected override void Attack() {
        agent.destination = agent.transform.position;
     
        if (nextTimeToAttack < Time.time && Vector3.Distance(transform.position, target.position) <= attackDistance) {
            animator.SetTrigger("Shoot");
            enemySoundManager.PlayAttackCliP();
            Health health = target.GetComponent<Health>();
            health.ApplyDamage(damage);
            agent.isStopped = true;
            nextTimeToAttack = Time.time + attackRate;
        }

    }
    
    
}
