using UnityEngine;

public class PickableHeal : MonoBehaviour, IInteractable
{
    [SerializeField] private int healAmount;
    
    public void Interact(IInteractor interactor) {
        if (interactor.WeaponInventory != null) {
            interactor.WeaponInventory.AddHeal(healAmount);
            Destroy(gameObject);
        }
    }
}
