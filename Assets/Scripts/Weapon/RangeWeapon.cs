using UnityEngine;

public class RangeWeapon : Weapon
{
    protected override void Shoot() { 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range)) {
            Debug.Log(hit.collider.name);
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null) {
                var health = enemy.GetComponent<Health>();
                health.ApplyDamage(damage);
                enemy.OnPlayerDetected();
            }
        }
        Debug.Log("shoot");
    }

    protected override void Reload() {
        Debug.Log("Reload");
    }
}
