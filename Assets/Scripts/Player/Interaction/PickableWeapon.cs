using UnityEngine;

public class PickableWeapon : MonoBehaviour, IPickUpWeapon
{
    [SerializeField] private WeaponData weaponData;
    public void Interact(WeaponInventory inventory) {
        inventory.SetNewWeapon(weaponData);
    }

    public void Interact() {
        Debug.Log("Заглузка");
    }
}
