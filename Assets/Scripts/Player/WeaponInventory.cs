using UnityEngine;
using System.Collections.Generic;


public class WeaponInventory : MonoBehaviour, IAmmoProvider
{
    [SerializeField] private CameraRecoil cameraRecoil;
    [SerializeField] private HUD hud;
    [SerializeField] private HealCount healCount;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private PlayerShooting playerShooting; 
    [SerializeField] private Weapon[] weapons;
    
    [SerializeField] private int RifleAmmo;
    [SerializeField] private int ShotgunAmmo;
    [SerializeField] private int PistolAmmo;
    [SerializeField] private int SmgAmmo;
    private int currentWeaponIndex = 0;
    private HealItem healItem = new HealItem();


    [SerializeField] private int healAmount;
    private Dictionary<AmmoType, int> ammoReserve = new Dictionary<AmmoType, int>();
   
    private IInteractor interactor;


    private void OnEnable() {
        playerInputHandler.Heal += UseHeal;
    }
    
    private void Awake() {
        SwitchWeapon(0);
        hud.UpdateHUD(weapons[0]);
        healCount.UpdateHealCount(healAmount);
        cameraRecoil.SetCurrentWeapon(weapons[0]);
        interactor = GetComponentInParent<IInteractor>();

        ammoReserve.Add(AmmoType.Rifle, RifleAmmo);
        ammoReserve.Add(AmmoType.Shotgun, ShotgunAmmo);
        ammoReserve.Add(AmmoType.Pistol, PistolAmmo);
        ammoReserve.Add(AmmoType.Smg, SmgAmmo);

    }


    private void Update() {
        Debug.Log("heals" + healAmount);
    }


    
    public void AddHeal(int amount) {
        healAmount += amount;
        healCount.UpdateHealCount(healAmount);
    }
    
    public void UseHeal() {
        if (healAmount < 1) return;
        if (healItem.Use(interactor)) {
            healAmount -= 1;
            healCount.UpdateHealCount(healAmount);
        } 
    }


    
    
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
    }
    
    
}
