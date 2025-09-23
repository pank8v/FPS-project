using UnityEngine;

public interface IInteractor
{
    WeaponInventory WeaponInventory { get; }
    PlayerHealth PlayerHealth { get; }
    
}
