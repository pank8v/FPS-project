using UnityEngine;

public class PickableWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] private WeaponData weaponData;
    public void Interact(GameObject interactor) {
        WeaponInventory inventory = interactor.GetComponent<WeaponInventory>();
        if (inventory != null) {
            inventory.SetNewWeapon(weaponData);
        }
    }

    public void Interact() {
        Debug.Log("Заглузка");
    }
}
