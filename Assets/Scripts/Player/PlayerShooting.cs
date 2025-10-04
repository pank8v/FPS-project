using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private WeaponInventory weaponInventory;
    [SerializeField] private WeaponVisual weaponVisual;
    private Weapon currentWeapon;
    private RangeWeapon rangeWeapon;

    [SerializeField] private float scrollCooldown = 0.2f;
    private float lastScrollTime = 0f;
    
    private void OnEnable() {
        playerInputHandler.OnReloadPressed += HandleReload;
        playerInputHandler.OnFirstWeaponSwitch += SwitchToMainWeapon;
        playerInputHandler.OnSecondWeaponSwitch += SwitchToAdditionalWeapon;
        playerInputHandler.OnThirdWeaponSwitch += SwitchToThirdWeapon;
        playerInputHandler.OnScroll += HandleScroll;

    }

    private void OnDisable() {
        playerInputHandler.OnReloadPressed -= HandleReload;
        playerInputHandler.OnFirstWeaponSwitch -= SwitchToMainWeapon;
        playerInputHandler.OnSecondWeaponSwitch -= SwitchToAdditionalWeapon;
        playerInputHandler.OnThirdWeaponSwitch -= SwitchToThirdWeapon;
        playerInputHandler.OnScroll -= HandleScroll;

    }

    
    private void SwitchToMainWeapon() => weaponInventory.SwitchWeapon(0);
    private void SwitchToAdditionalWeapon() => weaponInventory.SwitchWeapon(1);
    private void SwitchToThirdWeapon() => weaponInventory.SwitchWeapon(2);

    private void HandleScroll(float scrollInput) {
        if (Time.time - lastScrollTime < scrollCooldown) {
            return;
        }
        if (scrollInput < 0) {
            weaponInventory.SwitchToNextWeapon();
        }else if (scrollInput > 0) {
            weaponInventory.SwitchToPreviousWeapon();
        }

        lastScrollTime = Time.time;
    }
    
    private void Update() {
        if (!PauseManager.isPaused) {
            if (currentWeapon) {
                HandleShooting();
                HandleAiming(); 
                HandleSway();
                HandleBobbing();
            }
        }
    }


    public void SetCurrentWeapon(Weapon weapon, int _) {
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
        if (!PauseManager.isPaused) {
            rangeWeapon = currentWeapon as RangeWeapon;
            if (rangeWeapon && !PauseManager.isPaused) {
                rangeWeapon.TryReload();
            }
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
