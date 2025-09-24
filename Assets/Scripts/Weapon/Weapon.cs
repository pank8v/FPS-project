using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class Weapon : MonoBehaviour
{
   // weaponData
   [SerializeField] protected WeaponData weaponData;
   public WeaponData WeaponData => weaponData;

   protected GameObject weaponMeshInstance;
   protected GameObject weaponMesh => weaponData.WeaponMesh;
   
   protected float fireRate => weaponData.FireRate;
   protected int damage => weaponData.Damage;
   protected float range => weaponData.Range;
   protected float nextTimeToFire;
   
   //aiming
   protected bool isAiming;
   public bool IsAiming => isAiming;
   
   public event Action OnShoot;

 
   protected virtual bool CanFire() {
      return true;
   }
   
 
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
