using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
   [SerializeField] protected WeaponData weaponData;
   public WeaponData WeaponData => weaponData;

   public event Action OnShoot;
   
   
   protected float nextTimeToFire;
   
   protected int maxAmmo => weaponData.MaxAmmo;
   protected float fireRate => weaponData.FireRate;
   protected int damage => weaponData.Damage;
   protected float range => weaponData.Range;



   protected virtual bool CanFire() {
      return true;
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
   
}
