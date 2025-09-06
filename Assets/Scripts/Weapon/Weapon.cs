using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
   [SerializeField] protected WeaponData weaponData;
   protected float nextTimeToFire;
   
   protected int maxAmmo => weaponData.MaxAmmo;
   protected float fireRate => weaponData.FireRate;
   protected int damage => weaponData.Damage;
   protected float range => weaponData.Range;
   
   
   protected abstract void Shoot();
   protected abstract void Reload();

   public void Fire() {
      if (Time.time >= nextTimeToFire) {
         nextTimeToFire = Time.time + 1f / fireRate;
         Shoot();
      }
   }
   public void ReloadWeapon() => Reload();
   
}
