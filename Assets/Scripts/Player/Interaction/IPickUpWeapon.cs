using UnityEngine;

public interface IPickUpWeapon : IInteractable
{
    public void Interact(WeaponInventory inventory);
}
