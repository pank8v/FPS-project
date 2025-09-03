using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private CharacterController characterController;


    [SerializeField] private Weapon currentWeapon;
    
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float speedMultiplier = 1.8f;
    private float currentSpeed;
   

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update() {
        HandleMovement();
        HandleShooting();
    }

    
    private void HandleMovement() {
        currentSpeed = playerInputHandler.isSprinting ? moveSpeed * speedMultiplier : moveSpeed;
        Vector3 moveDirection = new Vector3(playerInputHandler.moveDirection.x, 0f, playerInputHandler.moveDirection.y).normalized;
        Vector3 worldMoveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(worldMoveDirection * currentSpeed * Time.deltaTime);
    }


    private void HandleShooting() {
        if (playerInputHandler.isAttacking) {
            currentWeapon.Fire();
        }
    }
    
    
}
