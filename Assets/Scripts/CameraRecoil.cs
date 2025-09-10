using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Weapon weapon;

    private float minimumRecoilX => weapon.WeaponData.MinimumRecoilX;
   private float maximumRecoilX => weapon.WeaponData.MaximumRecoilX;
   private float minimumRecoilY => weapon.WeaponData.MinimumRecoilY;
   private float maximumRecoilY => weapon.WeaponData.MaximumRecoilY;
   private float snappiness => weapon.WeaponData.Snappiness;
   private float returnSpeed => weapon.WeaponData.ReturnSpeed;


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
        targetRotation += new Vector3(Random.Range(minimumRecoilX, maximumRecoilX), Random.Range(minimumRecoilY, maximumRecoilY), 0);
    }
}
