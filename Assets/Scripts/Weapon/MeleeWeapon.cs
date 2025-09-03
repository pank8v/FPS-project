using UnityEngine;

public class MeleeWeapon : Weapon
{
   protected override void Shoot() {
      Debug.Log("bum");
   }

   protected override void Reload() {
      Debug.Log("reload??");
   }
}
