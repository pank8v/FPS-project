using UnityEngine;

public class PickableAmmo : MonoBehaviour, IInteractable
{
    private AmmoType ammoType = AmmoType.Rifle ;
    private int ammoCount = 20;
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
