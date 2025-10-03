using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private WeaponInventory weaponInventory;
    private Weapon currentWeapon;
    
    private float minimumRecoilX => currentWeapon.WeaponData.CameraMinimumRecoilX;
   private float maximumRecoilX => currentWeapon.WeaponData.CameraMaximumRecoilX;
   private float minimumRecoilY => currentWeapon.WeaponData.CameraMinimumRecoilY;
   private float maximumRecoilY => currentWeapon.WeaponData.CameraMaximumRecoilY;
   private float snappiness => currentWeapon.WeaponData.CameraSnappiness;
   private float returnSpeed => currentWeapon.WeaponData.CameraReturnSpeed;


    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private void OnEnable() {
        weaponInventory.OnWeaponSwitch += SetCurrentWeapon;
    }
    
    private void Update() {
        HandleRecoil();
    }


    public void SetCurrentWeapon(Weapon newWeapon, int _) {
        currentWeapon = newWeapon;
        if (currentWeapon != null) {
            currentWeapon.OnShoot -= AddRecoil;
            currentWeapon.OnShoot += AddRecoil;
        }
    }

    private void HandleRecoil() {
        if (currentWeapon) {
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnSpeed);
            currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.fixedDeltaTime * snappiness);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    private void AddRecoil() {
        targetRotation += new Vector3(Random.Range(minimumRecoilX, maximumRecoilX), Random.Range(minimumRecoilY, maximumRecoilY), 0);
    }
}
