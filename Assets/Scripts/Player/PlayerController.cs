using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
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
    private float verticalVelocity;

    [SerializeField] private float movementSmoothTime = 0.5f;
    private bool wasSprintingOnJump = false;
    private Vector3 horizontalMoveDirection;

    
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

   
    
    [SerializeField] private float slideCoolDown = 1;
    [SerializeField] private float slideDuration = 0.5f;
    private bool isSliding;
    private float slideTimer;
    private float slideCoolDownTimer;
    private Vector3 slideDir;

    void Start() {
        slideCoolDownTimer = slideCoolDown;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    
    private void Update() {
        HandleMovement();
        HanldeCrouch();
        HandleSlide();
        
    }

    
    
    private void HandleSpeed() {
        
        if (playerInputHandler.isJumping) {
            wasSprintingOnJump = playerInputHandler.isSprinting;
        }
        
        if (isGrounded) {
            if (playerInputHandler.isSprinting) {
                targetVelocity = moveSpeed * sprintMultiplier;
            }else if (playerInputHandler.isCrouching) {
                targetVelocity = moveSpeed * crouchMultiplier;
            }
            else {
                targetVelocity = moveSpeed;
            }
        }
        else {
            targetVelocity = wasSprintingOnJump ? moveSpeed * sprintMultiplier : moveSpeed;
        }

        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, speedSmoothTime * Time.deltaTime);
    }
    

    private void HandleMovement() {
        isGrounded = characterController.isGrounded;
        HandleSpeed();
        
        Vector3 targetDirection = new Vector3(playerInputHandler.moveDirection.x, 0f, playerInputHandler.moveDirection.y);
        targetDirection = transform.TransformDirection(targetDirection) * currentVelocity;
        horizontalMoveDirection = Vector3.Lerp(horizontalMoveDirection, targetDirection, Time.deltaTime * movementSmoothTime);
       
        // Jump
        if (playerInputHandler.isJumping && isGrounded) {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (isGrounded && verticalVelocity < 0) {
            verticalVelocity = -2f;
        }
        // gravity
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalMoveDirection = Vector3.up * verticalVelocity;
        
        
        characterController.Move((horizontalMoveDirection + verticalMoveDirection)  * Time.deltaTime);
    }



    private void HanldeCrouch() {
        bool isCrouching = playerInputHandler.isCrouching;
        if (isCrouching && isGrounded) {
            if (playerInputHandler.isSprinting && characterController.velocity.magnitude > 1 && slideCoolDownTimer <= 0) {
                if (!isSliding) {
                    StartSlide();
                }
            }
            else {
                Crouch();
                Debug.Log("crouch");
            }

        }
        else {
            characterController.height = originalHeight;
            characterController.center = originalControllerCenter;
            cameraHolder.localPosition = originalCameraPosition;

        }
    }

    private void Crouch() {
        characterController.height = crouchHeight;
        characterController.center = crouchControllerCenter;
        cameraHolder.localPosition = crouchCameraPosition;
    }

    private void StartSlide() {
        slideTimer = slideDuration;
        isSliding = true;
        slideDir = transform.TransformDirection(playerInputHandler.moveDirection.x, 0f, playerInputHandler.moveDirection.y).normalized;
    }

    private void HandleSlide() {
        if(slideCoolDownTimer > 0) slideCoolDownTimer -= Time.deltaTime;
        if (isSliding) {
            Debug.Log("sliding");
            characterController.Move(slideDir * moveSpeed * slideMultiplier * Time.deltaTime);
            slideTimer -= Time.deltaTime;
            characterController.height = crouchHeight;
            characterController.center = crouchControllerCenter;
            cameraHolder.localPosition = crouchCameraPosition;
            if (slideTimer <= 0) {
                isSliding = false;
                slideCoolDownTimer = slideCoolDown;
            }
        }
    }

  
    
}
