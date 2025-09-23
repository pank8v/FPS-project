using UnityEngine;

public class PickableAmmo : MonoBehaviour, IInteractable
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] private int ammoCount;
    public AmmoType AmmoType => ammoType;
    public int AmmoCount => ammoCount;
    
    public void Interact(IInteractor interactor) {
        if (interactor.WeaponInventory != null) {
            interactor.WeaponInventory.AddAmmo(AmmoType, AmmoCount);
            Destroy(gameObject);
        }
    }
}
