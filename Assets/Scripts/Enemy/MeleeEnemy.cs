using UnityEngine;

public class MeleeEnemy : Enemy
{
    
    protected override void Attack() {
        Health health = target.GetComponent<Health>();
        health.ApplyDamage(damage);
        agent.isStopped = true;
        Debug.Log("attack");
    }
    
    
}
