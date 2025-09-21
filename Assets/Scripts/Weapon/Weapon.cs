using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour
{
   [SerializeField] protected WeaponData weaponData;
   protected GameObject weaponMeshInstance;
   
   public WeaponData WeaponData => weaponData;
   protected float nextTimeToFire;
   
   protected GameObject weaponMesh => weaponData.WeaponMesh;
   protected AmmoType ammoType => weaponData.AmmoType;
   protected int maxAmmo => weaponData.MaxAmmo;
   protected float fireRate => weaponData.FireRate;
   protected int damage => weaponData.Damage;
   protected float range => weaponData.Range;
   public AmmoType AmmoType => ammoType;

   
   protected IAmmoProvider ammoProvider;

   public event Action OnShoot;


   private void Start() {
      weaponMeshInstance = Instantiate(weaponData.WeaponMesh, transform);
   }
   
   public virtual void Equipd(WeaponData newWeaponData) {
      weaponData = newWeaponData;
      if (weaponMeshInstance != null) {
         Destroy(weaponMeshInstance);
      }
      weaponMeshInstance = Instantiate(weaponData.WeaponMesh, transform);
      
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

   
   public void SetAmmoProvider(IAmmoProvider provider) {
      ammoProvider = provider;
   }
   
   public int GetAmmoCount() {
      return ammoProvider.GetAmmoCount(ammoType);
   }

}
