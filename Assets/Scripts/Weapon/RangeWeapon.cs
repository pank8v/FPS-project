using UnityEngine;
using System;

public class RangeWeapon : Weapon, IReloadable
{

    public event Action OnReload;
    public int currentAmmo { get; private set; }
    
    protected void Awake() {
        currentAmmo = weaponData.MaxAmmo;
    }
    
    protected override void Shoot() {
        currentAmmo--;
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
    }

    
    
    protected override bool CanFire() {
        return currentAmmo > 0;
    }

    
    public void Reload() {
        if (currentAmmo < maxAmmo) {
            currentAmmo = maxAmmo;
            OnReload?.Invoke();
        }
    }
    
}
