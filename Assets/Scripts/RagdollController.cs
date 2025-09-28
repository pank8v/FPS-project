using UnityEngine;

public class RagdollController : MonoBehaviour
{
   [SerializeField] private CapsuleCollider mainCollider;
   [SerializeField] private Animator animator;
   private Rigidbody[] rbs;
   private Collider[] cols;



   private void Awake() {
      rbs = GetComponentsInChildren<Rigidbody>();
      cols = GetComponentsInChildren<Collider>();
   }

   private void Start() {
      foreach (var rb in rbs) {
         rb.isKinematic = true;
         rb.useGravity = false;
      }

      foreach (var col in cols) {
         if (col != mainCollider)
            col.enabled = false;
      }
   }

   public void SetRagDoll(bool state) {
      animator.enabled = !state;
      foreach (var rb in rbs) {
         rb.isKinematic = !state ? true : false;
         rb.useGravity = true;
      }

      foreach (var col in cols) {
         if (col != mainCollider)
            col.enabled = state;
      }
      mainCollider.enabled = !state;
   }
   
   
}
