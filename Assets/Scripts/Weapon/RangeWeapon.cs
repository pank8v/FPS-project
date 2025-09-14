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
        Vector3 targetPoint;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
       
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else {
            targetPoint = ray.GetPoint(100f);
        }
        Vector3 direction = (targetPoint - muzzle.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.setDamage(weaponData.Damage);
        if (rb != null) {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
    
    public void Reload() {
        if (currentAmmo < maxAmmo) {
            currentAmmo = maxAmmo;
            OnReload?.Invoke();
        }
    }
    
}
