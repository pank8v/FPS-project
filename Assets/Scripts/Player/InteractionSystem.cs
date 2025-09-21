using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
   [SerializeField] private Camera mainCamera;
   [SerializeField] private PlayerInputHandler playerInputHandler;
   [SerializeField] private WeaponInventory weaponInventory;
   [SerializeField] private float maxViewAngle = 90f;
   [SerializeField] private float interactionRadius = 2f;
   [SerializeField] private LayerMask interactableLayer;
   private IInteractable currentInteractable;
   
   
   
   
   private void OnEnable() {
      playerInputHandler.OnInteract += Interact;
   }

   private void OnDisable() {
      playerInputHandler.OnInteract -= Interact;
   }
   
   private void Update() {
     FindInteractables();
   }


   private void FindInteractables() {
      currentInteractable = null;
      Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius, interactableLayer);
      float minDistance = Mathf.Infinity;
      for (int i = 0; i < hits.Length; i++) {
         IInteractable interactable = hits[i].GetComponent<IInteractable>();
         if (interactable == null) continue;
         Vector3 dirToObject = hits[i].transform.position - transform.position;
         float angle = Vector3.Angle(mainCamera.transform.forward, dirToObject);
         if (angle > maxViewAngle / 2f) continue;
         float distance = Vector3.Distance(transform.position, hits[i].transform.position);
         if (distance < minDistance) {
            minDistance = distance;
            currentInteractable = interactable;
         }
      }
      if (currentInteractable != null) {
         Debug.Log(currentInteractable);
      }
      else {
         Debug.Log("No interactable");
      }
  
   }
   
   private void Interact() {
      if (currentInteractable != null) {
         currentInteractable.Interact(gameObject);
      }
   }
   
   
   
}
