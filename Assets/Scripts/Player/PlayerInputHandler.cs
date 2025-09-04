using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    public Vector2 moveDirection { get; private set; }
    public Vector2 lookDirection { get; private set; }
    public bool isSprinting { get; private set; }
    public bool isAttacking { get; private set; }

    private void Awake() {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        inputSystem.Player.Move.performed += OnMovePerformed;
        inputSystem.Player.Move.canceled += OnMoveCanceled;
        inputSystem.Player.Look.performed += onLookPerformed;
        inputSystem.Player.Look.canceled += onLookCanceled;
        inputSystem.Player.Sprint.performed += OnSprintPerformed;
        inputSystem.Player.Sprint.canceled += OnSprintCanceled;
        inputSystem.Player.Attack.performed += OnAttackPerformed;
        inputSystem.Player.Attack.canceled += OnAttackCanceled;

    }


    private void OnMovePerformed(InputAction.CallbackContext context) {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context) {
        moveDirection = Vector2.zero;
    }


    private void onLookPerformed(InputAction.CallbackContext context) {
        lookDirection = context.ReadValue<Vector2>();
    }

    private void onLookCanceled(InputAction.CallbackContext context) {
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
    
}
