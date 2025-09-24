using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float nextTimeToAttack = 0f;
    
    protected override void Attack() {
        if (nextTimeToAttack < Time.time) {
            Health health = target.GetComponent<Health>();
            health.ApplyDamage(damage);
            agent.isStopped = true;
            Debug.Log("attack");
            nextTimeToAttack = Time.time + attackRate;
        }

    }
    
    
}
