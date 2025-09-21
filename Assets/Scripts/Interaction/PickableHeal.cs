using UnityEngine;

public class PickableHeal : MonoBehaviour, IInteractable
{
    [SerializeField] private float healAmount;
    
    public void Interact(GameObject interactor) {
        var health = interactor.GetComponent<PlayerHealth>();
        if (health != null) {
            if (health.ApplyHeal(healAmount)) {
                Destroy(gameObject);
            }
        }
    }
}
