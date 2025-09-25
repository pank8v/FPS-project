using UnityEngine;
using System;
using System.Collections;

public class RangeWeapon : Weapon
{
    protected RangeWeaponData rangeWeaponData => weaponData as RangeWeaponData;
    public RangeWeaponData RangeWeaponData => rangeWeaponData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    protected IAmmoProvider ammoProvider;
   
    // weaponData
    protected int maxAmmo => rangeWeaponData.MaxAmmo;

    protected AmmoType ammoType => rangeWeaponData.AmmoType;

    public AmmoType AmmoType => rangeWeaponData.AmmoType;

    private float reloadTime => rangeWeaponData.ReloadTime;
    protected float bulletSpeed => rangeWeaponData.BulletSpeed;


    public int currentAmmo { get; private set; }
    public bool isReloading = false;


    public event Action OnReload;
    public event Action OnReloadEnd;
    
    protected override bool CanFire() {
        return currentAmmo > 0 && !isReloading;
    }
    
    
    private void Awake() {
        currentAmmo = rangeWeaponData.MaxAmmo;
    }
    
    public void SetAmmoProvider(IAmmoProvider provider) {
        ammoProvider = provider;
    }

    
    public int GetAmmoCount() {
        return ammoProvider.GetAmmoCount(ammoType);
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
        bulletScript.SetDamage(weaponData.Damage, Bullet.DamageSource.Enemy);
        if (rb != null) {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
    
    public void TryReload() {
        int ammoToLoad = maxAmmo - currentAmmo;
        if (currentAmmo < maxAmmo && !isReloading && ammoProvider.HasReloadAmmo(ammoType, ammoToLoad)) {
            StartCoroutine(ReloadWeapon(ammoToLoad));
        }
    }


  
    
    private IEnumerator ReloadWeapon(int ammoToLoad) {
        OnReload?.Invoke();
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo += ammoProvider.AmountCount(ammoType, ammoToLoad);
        OnReloadEnd?.Invoke();
        isReloading = false;
    }
    
    
    

}
