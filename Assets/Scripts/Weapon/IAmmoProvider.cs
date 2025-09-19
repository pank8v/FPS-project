using UnityEngine;

public interface IAmmoProvider
{

    public bool HasReloadAmmo(AmmoType ammoType, int amount);

    public int AmountCount(AmmoType ammoType, int amount);
    
    public void GetAmmo(AmmoType ammoType);
}
