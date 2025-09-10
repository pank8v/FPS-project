using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Weapon weapon;
   
    [SerializeField] private float recoilX = -4f;
    [SerializeField] private float snappiness = 1f;
    [SerializeField] private float returnSpeed = 12f;


    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private void OnEnable() {
        weapon.OnShoot += AddRecoil;
    }

    private void OnDisable() {
        weapon.OnShoot -= AddRecoil;
    }
    private void Update() {
       targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnSpeed);
       currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.fixedDeltaTime * snappiness);
       transform.localRotation = Quaternion.Euler(currentRotation);
    }
    
  

    private void AddRecoil() {
        targetRotation += new Vector3(recoilX, 0, 0);
    }
}
