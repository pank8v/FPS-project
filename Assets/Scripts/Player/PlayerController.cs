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
    [SerializeField] private float crouchMultiplier = 0.6f;
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

    
    [Header("Crouch")]
    private float originalHeight = 2f;
    private float crouchHeight = 1f;
    private Vector3 originalControllerCenter = new Vector3(0, 0f, 0);
    private Vector3 crouchControllerCenter = new Vector3(0, -0.5f, 0);
    private Vector3 originalCameraPosition = new Vector3(0, 0.544f, 0.12f);
    private Vector3 crouchCameraPosition = new Vector3(0, 0.272f, 0.12f);

  
    
    private bool isSliding;


   

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update() {
        HandleMovement();
        HanldeCrouch(); 
        // HandleSlide();
    }

    
    
    private void HandleSpeed() {
        if (playerInputHandler.isJumping) {
            wasSprintingOnJump = playerInputHandler.isSprinting;
        }
        
        if (isGrounded) {
            targetVelocity = playerInputHandler.isSprinting ? moveSpeed * sprintMultiplier : playerInputHandler.isCrouching ? moveSpeed * crouchMultiplier : moveSpeed; 
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



    private void HanldeCrouch() {
        bool isCrouching = playerInputHandler.isCrouching;
        if (isCrouching) {
            characterController.height = crouchHeight;
            characterController.center = crouchControllerCenter;
            cameraHolder.localPosition = crouchCameraPosition;
        }
        else {
            characterController.height = originalHeight;
            characterController.center = originalControllerCenter;
            cameraHolder.localPosition = originalCameraPosition;

        }
    }
    

    private void HandleSlide() {
        if (playerInputHandler.isSliding && isGrounded && characterController.velocity.magnitude > 0.01f) {
            characterController.height -= 0.4f;
            characterController.center = new Vector3(0, 0.8f, 0);
            Debug.Log("sliding");
        }
        else {
            characterController.height = originalHeight;
        }
    }

  
    
}
