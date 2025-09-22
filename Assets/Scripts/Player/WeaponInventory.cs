using UnityEngine;
using System.Collections.Generic;


public class WeaponInventory : MonoBehaviour, IAmmoProvider, IHealProvider
{
    [SerializeField] private CameraRecoil cameraRecoil;
    [SerializeField] private HUD hud;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private PlayerShooting playerShooting; 
    [SerializeField] private Weapon[] weapons;
    
    [SerializeField] private int RifleAmmo;
    [SerializeField] private int ShotgunAmmo;
    [SerializeField] private int PistolAmmo;
    [SerializeField] private int SmgAmmo;
    private int currentWeaponIndex = 0;
    private HealItem heal = new HealItem();

    private int healAmount = 1;

    private void OnEnable() {
        playerInputHandler.Heal += UseHeal;
    }

    private void Update() {
        Debug.Log("heals" + healAmount);
    }


    public void UseHeal() {
        if (healAmount < 1) return;
        if (heal.Use(gameObject)) {
            healAmount -= 1;
        } 
    }

    public void AddHeal(int amount) {
        healAmount += amount;
    }
    
    private Dictionary<HealItem, int> heals = new Dictionary<HealItem, int>();
    private Dictionary<AmmoType, int> ammoReserve = new Dictionary<AmmoType, int>();

   
    
    public void AddAmmo(AmmoType ammoType, int amount) {
        if (ammoReserve.ContainsKey(ammoType)) {
           ammoReserve[ammoType] += amount;
           hud.UpdateAmmo();
        }
    }

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
    
    
    public int GetAmmoCount(AmmoType ammoType) {
        if (!ammoReserve.ContainsKey(ammoType)) return 0;
        return ammoReserve[ammoType];

    }
    
    private void Awake() {
        SwitchWeapon(0);
        hud.UpdateHUD(weapons[0]);
        cameraRecoil.SetCurrentWeapon(weapons[0]);

        ammoReserve.Add(AmmoType.Rifle, RifleAmmo);
        ammoReserve.Add(AmmoType.Shotgun, ShotgunAmmo);
        ammoReserve.Add(AmmoType.Pistol, PistolAmmo);
        ammoReserve.Add(AmmoType.Smg, SmgAmmo);

    }

    public void SwitchWeapon(int weaponIndex) {
        currentWeaponIndex = weaponIndex;
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        playerShooting.SetCurrentWeapon(weapons[weaponIndex]);
        hud.UpdateHUD(weapons[weaponIndex]);
        cameraRecoil.SetCurrentWeapon(weapons[weaponIndex]);
    }


    public void SetNewWeapon(WeaponData weaponData) {
        weapons[currentWeaponIndex].Equipd(weaponData);
        Debug.Log("new weapon set");
    }
    
    
}
