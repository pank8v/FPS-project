using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    public Vector2 moveDirection { get; private set; }
    public Vector2 lookDirection { get; private set; }
    public bool isSprinting { get; private set; }
    public bool isAttacking { get; private set; }
    public bool isAiming { get; private set; }
    public bool isJumping { get; private set; }
    public bool isCrouching { get; private set; }

    public event Action OnReloadPressed;
    public event Action OnMainWeaponSwitch;
    public event Action OnAdditionalWeaponSwitch;
    public event Action OnInteract;
    public event Action Heal;

    private void Awake() {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        inputSystem.Player.Move.performed += OnMovePerformed;
        inputSystem.Player.Move.canceled += OnMoveCanceled;
        inputSystem.Player.Look.performed += OnLookPerformed;
        inputSystem.Player.Look.canceled += OnLookCanceled;
        inputSystem.Player.Sprint.performed += OnSprintPerformed;
        inputSystem.Player.Sprint.canceled += OnSprintCanceled;
        inputSystem.Player.Attack.performed += OnAttackPerformed;
        inputSystem.Player.Attack.canceled += OnAttackCanceled;
        inputSystem.Player.Jump.performed += OnJumpPerformed;
        inputSystem.Player.Jump.canceled += OnJumpCanceled;
        inputSystem.Player.Crouch.performed += OnCrouchPerformed;
        inputSystem.Player.Crouch.canceled += OnCrouchingCanceled;
        inputSystem.Player.Reload.performed += OnReloadPerformed;
        inputSystem.Player.Aim.performed += OnAimPerformed;
        inputSystem.Player.Aim.canceled += OnAimCanceled;
        inputSystem.Player.MainWeaponSwitch.performed += OnMainWeaponSwitchPerformed;
        inputSystem.Player.AdditionalWeaponSwitch.performed += OnAdditionalWeaponSwitchPerformed;
        inputSystem.Player.Interact.performed += OnInteractPerformed;
        inputSystem.Player.Heal.performed += OnHealPerformed;

    }


    private void OnMovePerformed(InputAction.CallbackContext context) {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context) {
        moveDirection = Vector2.zero;
    }


    private void OnLookPerformed(InputAction.CallbackContext context) {
        lookDirection = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context) {
        lookDirection = Vector2.zero;
    }

    private void OnSprintPerformed(InputAction.CallbackContext context) {
        isSprinting = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context) {
        isSprinting = false;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context) {
        isAttacking = true;
    }

    private void OnAttackCanceled(InputAction.CallbackContext context) {
        isAttacking = false;
    }
    

    private void OnJumpPerformed(InputAction.CallbackContext context) {
        isJumping = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context) {
        isJumping = false;
    }

    private void OnReloadPerformed(InputAction.CallbackContext context) {
        OnReloadPressed?.Invoke();
    }

    private void OnAimPerformed(InputAction.CallbackContext context) {
        isAiming = true;
    }

    private void OnAimCanceled(InputAction.CallbackContext context) {
        isAiming = false;
    }
    
    private void OnMainWeaponSwitchPerformed(InputAction.CallbackContext context) {
        OnMainWeaponSwitch?.Invoke();
    }
    private void OnAdditionalWeaponSwitchPerformed(InputAction.CallbackContext context) {
        OnAdditionalWeaponSwitch?.Invoke();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) {
        OnInteract?.Invoke();
    }
    
    private void OnCrouchPerformed(InputAction.CallbackContext context) {
        isCrouching = true;
    }

    private void OnCrouchingCanceled(InputAction.CallbackContext context) {
        isCrouching = false;
    }

    private void OnHealPerformed(InputAction.CallbackContext context) {
        Heal?.Invoke();
    }
}
