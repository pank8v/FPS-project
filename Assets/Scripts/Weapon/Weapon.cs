using UnityEngine;

public abstract class Weapon : MonoBehaviour
{ 
   [SerializeField] protected WeaponData weaponData;
   protected float nextTimeToFire;
   protected abstract void Shoot();
   protected abstract void Reload();

   public void Fire() {
      if (Time.time >= nextTimeToFire) {
         nextTimeToFire = Time.time + 1f / weaponData.FireRate;
         Shoot();
      }
   }
   public void ReloadWeapon() => Reload();
   
}
