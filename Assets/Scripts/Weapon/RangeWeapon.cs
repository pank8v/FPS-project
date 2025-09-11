using UnityEngine;
using System;

public class RangeWeapon : Weapon, IReloadable
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    public int currentAmmo { get; private set; }
    protected float bulletSpeed => weaponData.BulletSpeed;

    public event Action OnReload;
    
    protected override bool CanFire() {
        return currentAmmo > 0;
    }
    
    protected void Awake() {
        currentAmmo = weaponData.MaxAmmo;
    }
    
    protected override void Shoot() {
        currentAmmo--;
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        Vector3 direction = Camera.main.transform.forward;
        bulletScript.setDamage(weaponData.Damage);
        if (rb != null) {
            rb.linearVelocity = direction * bulletSpeed;
        }
        /*    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range)) {
            Debug.Log(hit.collider.name);
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null) {
                var health = enemy.GetComponent<Health>();
                health.ApplyDamage(damage);
                enemy.OnPlayerDetected();
            }
        } */
    }
    
    public void Reload() {
        if (currentAmmo < maxAmmo) {
            currentAmmo = maxAmmo;
            OnReload?.Invoke();
        }
    }
    
}
