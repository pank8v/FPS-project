using UnityEngine;
using System.Collections.Generic;


public class WeaponInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private WeaponVisual weaponVisual;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private Transform[] weaponSlots;
    private Weapon[] weapons = new Weapon[3];
    private IInteractor interactor;

    
    
    [SerializeField] private CameraRecoil cameraRecoil;
    [SerializeField] private HUD hud;
    [SerializeField] private HealCount healCount;
    
    [SerializeField] private int RifleAmmo;
    [SerializeField] private int ShotgunAmmo;
    [SerializeField] private int PistolAmmo;
    [SerializeField] private int SmgAmmo;
    
    private int currentWeaponIndex = 0;
    private HealItem healItem = new HealItem();


    [SerializeField] private int healAmount;
    private Dictionary<AmmoType, int> ammoReserve = new Dictionary<AmmoType, int>();
   


    private void OnEnable() {
        playerInputHandler.Heal += UseHeal;
    }

    private void OnDisable() {
        playerInputHandler.Heal -= UseHeal;
    }
    
    private void Awake() {
        SwitchWeapon(0);
        interactor = GetComponentInParent<IInteractor>();
        healCount.UpdateHealCount(healAmount); 
        
      ammoReserve.Add(AmmoType.Rifle, RifleAmmo);
      ammoReserve.Add(AmmoType.Shotgun, ShotgunAmmo);
      ammoReserve.Add(AmmoType.Pistol, PistolAmmo);
      ammoReserve.Add(AmmoType.Smg, SmgAmmo);
    }

    // weapon    
    
    public void SwitchWeapon(int weaponIndex) {
        currentWeaponIndex = weaponIndex;
        for (int i = 0; i < weaponSlots.Length; i++) {
            weaponSlots[i].gameObject.SetActive(i == weaponIndex);
            playerShooting.SetCurrentWeapon(weapons[currentWeaponIndex]);
            cameraRecoil.SetCurrentWeapon(weapons[weaponIndex]);
            hud.UpdateHUD(weapons[weaponIndex]);
            weaponVisual.SetCurrentWeapon(weapons[weaponIndex], currentWeaponIndex);
        }
      

    }


    public void SetNewWeapon(WeaponData weaponData) {
        var oldWeapon = weaponSlots[currentWeaponIndex].GetComponentInChildren<Weapon>();
        if (oldWeapon) {
            Destroy(oldWeapon.gameObject);
        }
        GameObject weapon = Instantiate(weaponData.WeaponMesh, weaponSlots[currentWeaponIndex].transform);
        weapons[currentWeaponIndex] = weapon.GetComponent<Weapon>();
        var currentWeapon = weapons[currentWeaponIndex];
        playerShooting.SetCurrentWeapon(currentWeapon);
        cameraRecoil.SetCurrentWeapon(currentWeapon); 
        hud.UpdateHUD(currentWeapon);
        weaponVisual.SetCurrentWeapon(currentWeapon, currentWeaponIndex);
        
    }
    
    // heal
    
    public void AddHeal(int amount) {
        healAmount += amount;
        healCount.UpdateHealCount(healAmount);
    }
    
    public void UseHeal() {
        if (!PauseManager.isPaused) {
            if (healAmount < 1) return;
            if (healItem.Use(interactor)) {
                healAmount -= 1;
                healCount.UpdateHealCount(healAmount);
            } 
        }
    }
    
    // ammo
    
    public bool HasReloadAmmo(AmmoType ammoType, int amount) {
        return ammoReserve.ContainsKey(ammoType) && ammoReserve[ammoType] > 0;
    }
    
    
    public int AmountCount(AmmoType ammoType, int amount) {
        if (amount <= ammoReserve[ammoType]) {
            ammoReserve[ammoType] -= amount;
            return amount;
        }
        int taken = ammoReserve[ammoType];
        ammoReserve[ammoType] = taken;
        return taken;
    }
    
    
    public void AddAmmo(AmmoType ammoType, int amount) {
        if (ammoReserve.ContainsKey(ammoType)) {
           ammoReserve[ammoType] += amount;
           hud.UpdateAmmo();
        }
    }
    
    
    public int GetAmmoCount(AmmoType ammoType) {
        if (!ammoReserve.ContainsKey(ammoType)) return 0;
        return ammoReserve[ammoType];

    }
    


    
}
