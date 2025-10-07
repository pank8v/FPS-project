using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
   [SerializeField] private PlayerInputHandler playerInputHandler;
   [SerializeField] private InteractableUI interactableUI;
   [SerializeField] private Camera mainCamera;
   [SerializeField] private float maxViewAngle = 90f;
   [SerializeField] private float interactionRadius = 2f;
   [SerializeField] private LayerMask interactableLayer;
   private IInteractor interactor;
   private IInteractable currentInteractable;
   private IInteractable lastInteractable;
   
   

   private void Awake() {
      interactor = GetComponent<IInteractor>();
   }
   
   private void OnEnable() {
      playerInputHandler.OnInteract += Interact;
   }

   private void OnDisable() {
      playerInputHandler.OnInteract -= Interact;
   }
   
   private void Update() {
     FindInteractables();
     if (currentInteractable != lastInteractable) {
        interactableUI.UpdateText(currentInteractable);
        lastInteractable = currentInteractable;
     }
   }


   private void FindInteractables() {
      currentInteractable = null;
      Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius, interactableLayer);
      float minDistance = Mathf.Infinity;
      for (int i = 0; i < hits.Length; i++) {
         IInteractable interactable = hits[i].GetComponent<IInteractable>();
         if (interactable == null) continue;
         Vector3 closestPoint = hits[i].ClosestPoint(transform.position);
         Vector3 dirToObject = (closestPoint - mainCamera.transform.position).normalized;
         float angle = Vector3.Angle(mainCamera.transform.forward, dirToObject);
         if (angle > maxViewAngle / 2f) continue;
         float distance = Vector3.Distance(mainCamera.transform.position, closestPoint);
         if (distance < minDistance) {
            minDistance = distance;
            currentInteractable = interactable;
         }
      }
   }
   
   
   
   private void Interact() {
      if (!GameManager.isPaused) {
         if (currentInteractable != null) {
            currentInteractable.Interact(interactor);
         }
      }
   }
   
   
   
}
