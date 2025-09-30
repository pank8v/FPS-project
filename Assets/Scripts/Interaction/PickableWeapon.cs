using UnityEngine;

public class PickableWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactionText;
    public string InteractionText => interactionText;
    
    [SerializeField] private WeaponData weaponData;
    public void Interact(IInteractor interactor) {
        if (interactor.WeaponInventory != null) {
            interactor.WeaponInventory.SetNewWeapon(weaponData);
            Destroy(gameObject);
        }
    }
}
