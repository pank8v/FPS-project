using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    private Weapon currentWeapon;
    
    private float minimumRecoilX => currentWeapon.WeaponData.MinimumRecoilX;
   private float maximumRecoilX => currentWeapon.WeaponData.MaximumRecoilX;
   private float minimumRecoilY => currentWeapon.WeaponData.MinimumRecoilY;
   private float maximumRecoilY => currentWeapon.WeaponData.MaximumRecoilY;
   private float snappiness => currentWeapon.WeaponData.Snappiness;
   private float returnSpeed => currentWeapon.WeaponData.ReturnSpeed;


    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private void Update() {
        HandleRecoil();
    }


    public void SetCurrentWeapon(Weapon newWeapon) {
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
