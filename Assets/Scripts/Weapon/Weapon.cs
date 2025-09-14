using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

   [SerializeField] protected WeaponData weaponData;
   [SerializeField] protected GameObject weaponMeshInstance;
   public WeaponData WeaponData => weaponData;
   protected float nextTimeToFire;

   protected GameObject weaponMesh => weaponData.WeaponMesh;
   protected int maxAmmo => weaponData.MaxAmmo;
   protected float fireRate => weaponData.FireRate;
   protected int damage => weaponData.Damage;
   protected float range => weaponData.Range;

   public event Action OnShoot;



   public void Equipd(WeaponData newWeaponData) {
      weaponData = newWeaponData;
      if (weaponMeshInstance != null) {
         Destroy(weaponMeshInstance);
      }
      Instantiate(weaponData.WeaponMesh, transform);
   }
   

   protected virtual bool CanFire() {
      return true;
   }
   
   protected bool isAiming;
   public bool IsAiming => isAiming;
   
   protected abstract void Shoot();

   public virtual void Fire() {
      if (!CanFire()) return;
      if (Time.time >= nextTimeToFire) {
         nextTimeToFire = Time.time + 1f / fireRate;
         Shoot();
         OnShoot?.Invoke();
      }
   }

   public void setIsAiming(bool isAimingTriggered) {
      isAiming = isAimingTriggered;
   }

   

}
