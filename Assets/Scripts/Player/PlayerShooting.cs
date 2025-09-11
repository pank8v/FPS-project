using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Weapon currentWeapon;


    private void OnEnable() {
        playerInputHandler.OnReloadPressed += HandleReload;
    }

    private void OnDisable() {
        playerInputHandler.OnReloadPressed -= HandleReload;
    }
    
    private void Update() {
        HandleShooting();
        HandleAiming();
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
}
