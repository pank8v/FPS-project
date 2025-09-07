using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private CharacterController characterController;
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float speedMultiplier = 1.8f;
    private float currentSpeed;


    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravity = -20f;
    private float verticalVelocity;
    private bool isGrounded;
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update() {
        HandleMovement();
    }

    
    private void HandleMovement() {
        isGrounded = characterController.isGrounded;
        if (isGrounded && verticalVelocity < 0) {
            verticalVelocity = -2f;
        }
        
        currentSpeed = playerInputHandler.isSprinting ? moveSpeed * speedMultiplier : moveSpeed;
        Vector3 moveDirection = new Vector3(playerInputHandler.moveDirection.x, 0f, playerInputHandler.moveDirection.y).normalized;
        Vector3 horizontalMoveDirection = transform.TransformDirection(moveDirection) * currentSpeed;
        
        
        if (playerInputHandler.isJumping && isGrounded) {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalMoveDirection = Vector3.up * verticalVelocity;
        
        characterController.Move((horizontalMoveDirection + verticalMoveDirection)  * Time.deltaTime);

    }

  
    
}
