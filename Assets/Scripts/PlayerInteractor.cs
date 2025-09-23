using UnityEngine;

public class PlayerInteractor : MonoBehaviour, IInteractor
{
    [SerializeField] private WeaponInventory weaponInventory;
    [SerializeField] private PlayerHealth playerHealth;

    public WeaponInventory WeaponInventory => weaponInventory;
    public PlayerHealth PlayerHealth => playerHealth;
}
