using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private Weapon currentWeapon;

   private RangeWeapon rangeWeapon;
   
   private void OnEnable() {
      currentWeapon.OnShoot += PlayShotSound;
      rangeWeapon = currentWeapon as RangeWeapon;
      if (rangeWeapon) {
         rangeWeapon.OnReload += PlayReloadSound;
      }
      
   }

   private void OnDisable() {
      currentWeapon.OnShoot -= PlayShotSound;
      rangeWeapon = currentWeapon as RangeWeapon;
      if (rangeWeapon) {
         rangeWeapon.OnReload -= PlayReloadSound;
      }
   }

   private void PlayShotSound() {
      audioSource.PlayOneShot(currentWeapon.WeaponData.ShotSound);
   }

   private void PlayReloadSound() {
      audioSource.PlayOneShot(rangeWeapon.RangeWeaponData.ReloadSound);
   }
}
