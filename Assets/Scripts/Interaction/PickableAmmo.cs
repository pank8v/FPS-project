using UnityEngine;

public class PickableAmmo : MonoBehaviour, IInteractable
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] private int ammoCount;
    public AmmoType AmmoType => ammoType;
    public int AmmoCount => ammoCount;
    
    public void Interact(GameObject interactor) {
        var provider = interactor.GetComponent<IAmmoProvider>();
        if (provider != null) {
            provider.AddAmmo(AmmoType, AmmoCount);
            Destroy(gameObject);
        }
    }
}
