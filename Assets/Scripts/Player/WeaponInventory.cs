using System;
using UnityEngine;
using System.Collections.Generic;


public class WeaponInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private Transform[] weaponSlots;
    private Weapon[] weapons = new Weapon[3];
    
    private Weapon currentWeapon;
    private int currentWeaponIndex = 0;
    private IInteractor interactor;

    
    [SerializeField] private HealCount healCount;
    [SerializeField] private int RifleAmmo;
    [SerializeField] private int ShotgunAmmo;
    [SerializeField] private int PistolAmmo;
    [SerializeField] private int SmgAmmo;
    
    private HealItem healItem = new HealItem();


    [SerializeField] private int healAmount;
    private Dictionary<AmmoType, int> ammoReserve = new Dictionary<AmmoType, int>();

    public event Action<Weapon, int> OnWeaponSwitch;
    public event Action OnAmmoChanged;
    public event Action<int> OnHealAmountChanged;
    
    private void OnEnable() {
        playerInputHandler.Heal += UseHeal;
    }

    private void OnDisable() {
        playerInputHandler.Heal -= UseHeal;
    }
    
    private void Awake() { 
        OnHealAmountChanged?.Invoke(healAmount);
        SwitchWeapon(currentWeaponIndex); 
        interactor = GetComponentInParent<IInteractor>();
        
      ammoReserve.Add(AmmoType.Rifle, RifleAmmo);
      ammoReserve.Add(AmmoType.Shotgun, ShotgunAmmo);
      ammoReserve.Add(AmmoType.Pistol, PistolAmmo);
      ammoReserve.Add(AmmoType.Smg, SmgAmmo);
    }

    // weapon    

    public void SwitchToNextWeapon() {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        SwitchWeapon(currentWeaponIndex);
    }


    public void SwitchToPreviousWeapon() {
        currentWeaponIndex = (currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
        SwitchWeapon(currentWeaponIndex);
    }
    
    public void SwitchWeapon(int weaponIndex) {
        currentWeaponIndex = weaponIndex;
        for (int i = 0; i < weaponSlots.Length; i++) {
            weaponSlots[i].gameObject.SetActive(i == weaponIndex);
            currentWeapon = weapons[currentWeaponIndex];
            playerShooting.SetCurrentWeapon(currentWeapon, currentWeaponIndex);
            OnWeaponSwitch?.Invoke(currentWeapon, currentWeaponIndex);
            
        }
      

    }

    public void SetNewWeapon(WeaponData weaponData) {
        if (weapons[currentWeaponIndex]) {
            for (int i = 0; i < weapons.Length; i++) {
                if (weapons[i] == null) {
                    SwitchWeapon(i);
                    break;
                }
        }
        }
        var oldWeapon = weaponSlots[currentWeaponIndex].GetComponentInChildren<Weapon>();
        if (oldWeapon) {
            Destroy(oldWeapon.gameObject);
        }
        GameObject weapon = Instantiate(weaponData.WeaponMesh, weaponSlots[currentWeaponIndex].transform);
        weapons[currentWeaponIndex] = weapon.GetComponent<Weapon>();
        currentWeapon = weapons[currentWeaponIndex];
        playerShooting.SetCurrentWeapon(currentWeapon, currentWeaponIndex);
        OnWeaponSwitch?.Invoke(currentWeapon, currentWeaponIndex);
        
    }
    
    // heal
    
    public void AddHeal(int amount) {
        healAmount += amount;
        OnHealAmountChanged?.Invoke(healAmount);
    }
    
    public void UseHeal() {
        if (!GameManager.isPaused) {
            if (healAmount < 1) return;
            if (healItem.Use(interactor)) {
                healAmount -= 1;
                OnHealAmountChanged?.Invoke(healAmount);
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
           OnAmmoChanged?.Invoke();
        }
    }
    
    
    public int GetAmmoCount(AmmoType ammoType) {
        if (!ammoReserve.ContainsKey(ammoType)) return 0;
        return ammoReserve[ammoType];

    }
    


    
}
