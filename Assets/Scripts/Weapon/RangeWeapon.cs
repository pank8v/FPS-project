using UnityEngine;
using System;
using System.Collections;

public class RangeWeapon : Weapon, IReloadable
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    
    private float reloadTime => weaponData.ReloadTime;
    protected float bulletSpeed => weaponData.BulletSpeed;

    private bool isReloading = false;
    
    public int currentAmmo { get; private set; }

    public event Action OnReload;
    public event Action OnReloadEnd;
    
    protected override bool CanFire() {
        return currentAmmo > 0 && !isReloading;
        
    }
    
    private void Awake() {
        currentAmmo = weaponData.MaxAmmo;
    }

   /* public override void Equipd(WeaponData newWeaponData) {
        weaponData = newWeaponData;
        if (weaponMeshInstance != null) {
            Destroy(weaponMeshInstance);
        }
        weaponMeshInstance = Instantiate(weaponData.WeaponMesh, transform);
    }
    */
    
    protected override void Shoot() {
        ammoProvider.GetAmmo(ammoType);
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
        bulletScript.setDamage(weaponData.Damage, Bullet.DamageSource.Enemy);
        if (rb != null) {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
    
    public void Reload() {
        if (currentAmmo < maxAmmo && !isReloading) {
            StartCoroutine(ReloadWeapon());
        }
    }

    private IEnumerator ReloadWeapon() {
        OnReload?.Invoke();
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        OnReloadEnd?.Invoke();
        isReloading = false;
    }
    
}
