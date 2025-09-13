using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraHolder;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sprintMultiplier = 1.6f;
    [SerializeField] private float slideMultiplier = 2f;
    private float currentSpeed;
    

    
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravity = -20f;

    private Vector3 originalCenter = new Vector3(0, 0f, 0);
    private Vector3 slideCenter = new Vector3(0, 1f, 0);
    private float originalHeight = 2f;
    private float slideHeight = 1f;
   
   
    private float verticalVelocity;
    private bool isGrounded;
    private bool isSliding;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update() {
        HandleMovement();
        HandleSlide();
    }

    
    private void HandleMovement() {
        isGrounded = characterController.isGrounded;
  
        currentSpeed = playerInputHandler.isSprinting ? moveSpeed * sprintMultiplier : playerInputHandler.isSliding ? moveSpeed * slideMultiplier  : moveSpeed;
        
        Vector3 moveDirection = new Vector3(playerInputHandler.moveDirection.x, 0f, playerInputHandler.moveDirection.y).normalized;
        Vector3 horizontalMoveDirection = transform.TransformDirection(moveDirection) * currentSpeed;
        
        
        if (playerInputHandler.isJumping && isGrounded) {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
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
