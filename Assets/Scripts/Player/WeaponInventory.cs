using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] private HUD hud;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private PlayerShooting playerShooting; 
    [SerializeField] private Weapon[] weapons;
    
    private void OnEnable() {
        playerInputHandler.OnMainWeaponSwitch += SwitchToMainWeapon;
        playerInputHandler.OnAdditionalWeaponSwitch += SwitchToAdditionalWeapon;
    }

    private void OnDisable() {
        playerInputHandler.OnMainWeaponSwitch -= SwitchToMainWeapon;
        playerInputHandler.OnAdditionalWeaponSwitch -= SwitchToAdditionalWeapon;
    }

    private void SwitchToMainWeapon() => SwitchWeapon(0);
    private void SwitchToAdditionalWeapon() => SwitchWeapon(1);

    private void Awake() {
        SwitchWeapon(0);
        hud.UpdateHUD(weapons[0]);
    }

    private void SwitchWeapon(int weaponIndex) {
        Debug.Log(weaponIndex);
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        playerShooting.SetCurrentWeapon(weapons[weaponIndex]);
        hud.UpdateHUD(weapons[weaponIndex]);
    }
    
    
    
//    private void SwitchCurrentWeapon(int weaponCount) {
      //  currentWeapon.Equipd(weapons[weaponCount]);

    
}
