using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    private GameObject weaponHolder;
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private WeaponInventory weaponInventory;
    [SerializeField] private WeaponVisual weaponVisual;
    
    
    private void OnEnable() {
        playerInputHandler.OnReloadPressed += HandleReload;
        playerInputHandler.OnMainWeaponSwitch += SwitchToMainWeapon;
        playerInputHandler.OnAdditionalWeaponSwitch += SwitchToAdditionalWeapon;

    }

    private void OnDisable() {
        playerInputHandler.OnReloadPressed -= HandleReload;
        playerInputHandler.OnMainWeaponSwitch -= SwitchToMainWeapon;
        playerInputHandler.OnAdditionalWeaponSwitch -= SwitchToAdditionalWeapon;
    }

    
    private void SwitchToMainWeapon() => weaponInventory.SwitchWeapon(0);
    private void SwitchToAdditionalWeapon() => weaponInventory.SwitchWeapon(1);


    private void Awake() {
        currentWeapon.SetAmmoProvider(weaponInventory);
    }
    
    private void Update() {
        HandleShooting();
        HandleAiming();
        HandleSway();
        HandleBobing();
    }


    public void SetCurrentWeapon(Weapon weapon) {
        if (currentWeapon is RangeWeapon range) {
            range.isReloading = false;
        }

        weaponVisual = weapon.GetComponent<WeaponVisual>();
        currentWeapon = weapon;
        weaponHolder = weapon.gameObject;
        currentWeapon.SetAmmoProvider(weaponInventory);
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

    private void HandleBobing() {
       weaponVisual.Bobing(playerInputHandler.moveDirection);
    }
}
