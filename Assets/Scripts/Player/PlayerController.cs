using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraHolder;
    [Header("Velocity")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sprintMultiplier = 1.6f;
    [SerializeField] private float slideMultiplier = 2f;
    [SerializeField] private float speedSmoothTime = 10f;
    private float currentVelocity;
    private float targetVelocity;
    private bool wasSprintingOnJump = false;
    private float verticalVelocity;

    
    [Header("Jump")]
    private bool isJumping = false;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravity = -20f;
    private bool isGrounded;

    
    [Header("Slide")]
    private Vector3 originalCenter = new Vector3(0, 0f, 0);
    private Vector3 slideCenter = new Vector3(0, 1f, 0);
    private float originalHeight = 2f;
    private float slideHeight = 1f;
    private bool isSliding;

   

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update() {
        HandleMovement();
        HandleSlide();
    }

    
    
    private void HandleSpeed() {
        if (playerInputHandler.isJumping) {
            wasSprintingOnJump = playerInputHandler.isSprinting;
        }
        
        if (isGrounded) {
            targetVelocity = playerInputHandler.isSprinting ? moveSpeed * sprintMultiplier : moveSpeed; 
        }
        else {
            targetVelocity = wasSprintingOnJump ? moveSpeed * sprintMultiplier : moveSpeed;
        }

        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, speedSmoothTime * Time.deltaTime);
    }
    

    private void HandleMovement() {
        isGrounded = characterController.isGrounded;
        HandleSpeed();
        
        Vector3 moveDirection = new Vector3(playerInputHandler.moveDirection.x, 0f, playerInputHandler.moveDirection.y).normalized;
        Vector3 horizontalMoveDirection = transform.TransformDirection(moveDirection) * currentVelocity;
        
        // Jump
        
        if (playerInputHandler.isJumping && isGrounded) {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (isGrounded && verticalVelocity < 0) {
            verticalVelocity = -2f;
        }
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalMoveDirection = Vector3.up * verticalVelocity;
        
        characterController.Move((horizontalMoveDirection + verticalMoveDirection)  * Time.deltaTime);

    }


    private void HandleSlide() {
        if (playerInputHandler.isSliding && isGrounded && characterController.velocity.magnitude > 0.01f) {
            characterController.height -= 0.4f;
            characterController.center = new Vector3(0, 0.8f, 0);
            Debug.Log("sliding");
        }
        else {
            characterController.height = originalHeight;
            characterController.center = originalCenter;
        }
    }

  
    
}
