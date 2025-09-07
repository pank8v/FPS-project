using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private Weapon weapon;

   private IReloadable reloadableWeapon;
   
   private void OnEnable() {
      weapon.OnShoot += PlayShotSound;
      if (weapon is IReloadable ir) {
         ir.OnReload += PlayReloadSound;
      }
      
   }

   private void OnDisable() {
      weapon.OnShoot -= PlayShotSound;
      if (reloadableWeapon != null) {
         reloadableWeapon.OnReload -= PlayReloadSound;
      }
   }

   private void PlayShotSound() {
      audioSource.PlayOneShot(weapon.WeaponData.ShotSound);
   }

   private void PlayReloadSound() {
      audioSource.PlayOneShot(weapon.WeaponData.ReloadSound);
   }
}
