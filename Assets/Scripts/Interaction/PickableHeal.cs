using UnityEngine;

public class PickableHeal : MonoBehaviour, IInteractable
{
    [SerializeField] private float healAmount;
    
    public void Interact(GameObject interactor) {
        var inventory = interactor.GetComponent<WeaponInventory>();
        if (inventory != null) {
            inventory.AddHeal(1);
            Destroy(gameObject);
        }
    }
}
