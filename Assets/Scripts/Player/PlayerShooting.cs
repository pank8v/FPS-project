using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    private GameObject weaponHolder;
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private WeaponVisual weaponVisual;
    
    private void OnEnable() {
        playerInputHandler.OnReloadPressed += HandleReload;

    }

    private void OnDisable() {
        playerInputHandler.OnReloadPressed -= HandleReload;
    }
    

    
    private void Update() {
        HandleShooting();
        HandleAiming();
        HandleSway();
    }


    public void SetCurrentWeapon(Weapon weapon) {
        currentWeapon = weapon;
        weaponHolder = weapon.gameObject;
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
