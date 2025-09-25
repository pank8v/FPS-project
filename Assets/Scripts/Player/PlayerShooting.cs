using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private WeaponInventory weaponInventory;
    [SerializeField] private WeaponVisual weaponVisual;
    private Weapon currentWeapon;
    private RangeWeapon rangeWeapon;

    
    private void OnEnable() {
        playerInputHandler.OnReloadPressed += HandleReload;
        playerInputHandler.OnFirstWeaponSwitch += SwitchToMainWeapon;
        playerInputHandler.OnSecondWeaponSwitch += SwitchToAdditionalWeapon;
        playerInputHandler.OnThirdWeaponSwitch += SwitchToThirdWeapon;

    }

    private void OnDisable() {
        playerInputHandler.OnReloadPressed -= HandleReload;
        playerInputHandler.OnFirstWeaponSwitch -= SwitchToMainWeapon;
        playerInputHandler.OnSecondWeaponSwitch -= SwitchToAdditionalWeapon;
        playerInputHandler.OnThirdWeaponSwitch -= SwitchToThirdWeapon;
    }

    
    private void SwitchToMainWeapon() => weaponInventory.SwitchWeapon(0);
    private void SwitchToAdditionalWeapon() => weaponInventory.SwitchWeapon(1);
    private void SwitchToThirdWeapon() => weaponInventory.SwitchWeapon(2);

    private void Awake() {
      //   rangeWeapon = currentWeapon as RangeWeapon;
     //   if (rangeWeapon) {
    //        rangeWeapon.SetAmmoProvider(weaponInventory);
    //    }
    }
    
    private void Update() {
        HandleShooting();
        if (currentWeapon) {
            HandleAiming(); 
            HandleSway();
            HandleBobbing();
        }
    }


    public void SetCurrentWeapon(Weapon weapon) {
        currentWeapon = weapon; 
        
        rangeWeapon = currentWeapon as RangeWeapon;
        if (rangeWeapon) {
            rangeWeapon.isReloading = false;
            rangeWeapon.SetAmmoProvider(weaponInventory);
        }
       
    }

    
    private void HandleShooting() {
        if (playerInputHandler.isAttacking) {
            currentWeapon.Fire();
        }
    }

    private void HandleReload() {
        rangeWeapon = currentWeapon as RangeWeapon;
        if (rangeWeapon) {
            rangeWeapon.TryReload();
        }
    }

    private void HandleAiming() {
            currentWeapon.setIsAiming(playerInputHandler.isAiming);
    }

    

    private void HandleSway() {
            weaponVisual.Sway(playerInputHandler.lookDirection);
    }

    private void HandleBobbing() {
            weaponVisual.Bobing(playerInputHandler.moveDirection);
    }
}
