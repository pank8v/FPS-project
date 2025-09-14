using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private WeaponData[] weapons;
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private WeaponVisual weaponVisual;


    

    private void SetCurrentWeapon() {
        currentWeapon.Equipd(weapons[0]);
    }

    private void SwitchCurrentWeapon(int weaponCount) {
        currentWeapon.Equipd(weapons[weaponCount]);
    }


    
    
    private void OnEnable() {
        playerInputHandler.OnReloadPressed += HandleReload;
        playerInputHandler.OnMainWeaponSwitch += () => SwitchCurrentWeapon(0);
        playerInputHandler.OnAdditionalWeaponSwitch += () => SwitchCurrentWeapon(1);

    }

    private void OnDisable() {
        playerInputHandler.OnReloadPressed -= HandleReload;
        playerInputHandler.OnMainWeaponSwitch += () => SwitchCurrentWeapon(0);
        playerInputHandler.OnAdditionalWeaponSwitch += () => SwitchCurrentWeapon(1);
    }
    
    private void Awake() {
        SetCurrentWeapon();
    }
    
    private void Update() {
        HandleShooting();
        HandleAiming();
        HandleSway();
    }

    private void WeaponSwitch() {
        
    }
    
    private void HandleShooting() {
        if (playerInputHandler.isAttacking) {
            currentWeapon.Fire();
        }
    }

    private void HandleReload() {
            if (currentWeapon is IReloadable rangeWeapon) {
                rangeWeapon.Reload();
            }
    }

    private void HandleAiming() {
       currentWeapon.setIsAiming(playerInputHandler.isAiming);
    }

    private void HandleSway() {
        weaponVisual.Sway(playerInputHandler.lookDirection);
    }
}
