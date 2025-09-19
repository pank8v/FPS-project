using UnityEngine;
using System.Collections.Generic;


public class WeaponInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField] private CameraRecoil cameraRecoil;
    [SerializeField] private HUD hud;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private PlayerShooting playerShooting; 
    [SerializeField] private Weapon[] weapons;
    
    [SerializeField] private int RifleAmmo;
    [SerializeField] private int ShotgunAmmo;

    private Dictionary<AmmoType, int> ammoReserve = new Dictionary<AmmoType, int>();




    public int AmountCount(AmmoType ammoType, int amount) {
        if (amount <= ammoReserve[ammoType]) {
            ammoReserve[ammoType] -= amount;
            Debug.Log(ammoReserve[ammoType]);
            return amount;
        }

        int taken = ammoReserve[ammoType];
        ammoReserve[ammoType] = taken;
        return taken;
    }
    
    public bool HasReloadAmmo(AmmoType ammoType, int amount) {
        return ammoReserve.ContainsKey(ammoType) && ammoReserve[ammoType] > 0;
    }
    
    
    public void GetAmmo(AmmoType ammoType) {
        Debug.Log(ammoReserve[ammoType]);
        
    }
    
    private void Awake() {
        SwitchWeapon(0);
        hud.UpdateHUD(weapons[0]);
        cameraRecoil.SetCurrentWeapon(weapons[0]);

        ammoReserve.Add(AmmoType.Rifle, RifleAmmo);
        ammoReserve.Add(AmmoType.Shotgun, ShotgunAmmo);

    }

    public void SwitchWeapon(int weaponIndex) {
        Debug.Log(weaponIndex);
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        playerShooting.SetCurrentWeapon(weapons[weaponIndex]);
        hud.UpdateHUD(weapons[weaponIndex]);
        cameraRecoil.SetCurrentWeapon(weapons[weaponIndex]);
    }


    public void SetNewWeapon(WeaponData weapondata) {
    //    weapons[0].WeaponData = weapondata;
    }
    
    
//    private void SwitchCurrentWeapon(int weaponCount) {
      //  currentWeapon.Equipd(weapons[weaponCount]);

    
}
