using UnityEngine;

public class MeleeWeapon : Weapon
{
    
    private Vector3 halfExtents = new Vector3(0.5f, 0.5f, 1f);
    private float maxDistance = 4f;
    
    

    protected override void Shoot() {
        Vector3 direction = transform.forward;
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, halfExtents, direction, out hit, Quaternion.identity, maxDistance)) {
            var enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth) {
                enemyHealth.ApplyDamage(damage);
            }
        }
    }

}
