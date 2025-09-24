using UnityEngine;

public class MeleeWeapon : Weapon
{
   [SerializeField] private BoxCollider boxCollider;

   private bool isAttacking;
   
   private void Start() {
      boxCollider.enabled = false;
   }
   
   
   protected override void Shoot() {
      boxCollider.enabled = true;
   }


   private void OnCollisionEnter(Collision collision) {
      var enemyHealth = GetComponent<EnemyHealth>();
      if (enemyHealth != null) {
         enemyHealth.ApplyDamage(damage);
      }
   }
   


}
