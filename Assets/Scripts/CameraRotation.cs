using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    [SerializeField] private float tiltAngle = 10f;
    [SerializeField] private float tiltSpeed = 10f;
    private float currentTilt = 0;
    
    private void Update() {
        HandleRotation();
        CameraTilt();
    }
    
    private void HandleRotation() {
        Vector2 lookInput = playerInputHandler.lookDirection;
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void CameraTilt() {
        float targetTilt = -playerInputHandler.moveDirection.x * tiltAngle;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentTilt);
    }
}
