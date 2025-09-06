using UnityEngine;

public class RangeWeapon : Weapon
{
    protected override void Shoot() { 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range)) {
            Debug.Log(hit.collider.name);
            var health = hit.collider.GetComponent<Health>();
            if (health != null) {
                health.ApplyDamage(damage);
            }
        }
        Debug.Log("shoot");
    }

    protected override void Reload() {
        Debug.Log("Reload");
    }
}
