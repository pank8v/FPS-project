using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
   [SerializeField] private Transform[] weaponSlots;
   [SerializeField] private Transform rightHandTransform;
   [SerializeField] private Transform leftHandTransform;
   private int currentWeaponIndex;
   private Weapon currentWeapon;
   private RangeWeapon rangeWeapon;
  
   [Header("Positions")]
   private Vector3 weaponPosition => currentWeapon.WeaponData.WeaponPosition;
   private Vector3 HipPosition => currentWeapon.WeaponData.HipPosition;
   private Vector3 AimPosition => currentWeapon.WeaponData.AimPosition;
   
   [SerializeField] private float aimSmooth = 0.5f;
   
   [Header("Recoil Settings")]
   [SerializeField] private float recoilY = 0.002f;
   [SerializeField] private float recoilZ = 0.015f;
   [SerializeField] private float recoilSmooth = 1.8f;

   [Header("Rotation Sway Settings")]
   [SerializeField] private float rotationSwayX = 1f;
   [SerializeField] private float rotationSwayY= 1f;
   [SerializeField] private float rotationSwaySmooth = 2f;
  
   [Header("Position Sway Settings")]
   [SerializeField] private float maxSwayAmount = 0.2f;
   [SerializeField] private float swayAmount = 0.1f;
   [SerializeField] private float positionSwaySmooth = 2f;


   [Header("Bobing")]
   private float bobbingSpeedX => currentWeapon.WeaponData.BobbingSpeedX;

   private float bobbingAmountX => currentWeapon.WeaponData.BobbingAmountX;
   private float bobbingSpeedY => currentWeapon.WeaponData.BobbingSpeedY;
   private float bobbingAmountY => currentWeapon.WeaponData.BobbingAmountY;
   
   
   private Vector3 targetPosition;
   private Vector3 recoilOffset;
   private Vector3 swayPositionOffset;
   private Quaternion initialRotation;

   private Vector3 bobingOffset;


   private void Start() {
      initialRotation = transform.rotation;
   }

   public void SetCurrentWeapon(Weapon weapon, int weaponIndex) {
      currentWeapon = weapon;
      currentWeaponIndex = weaponIndex;
      rangeWeapon = currentWeapon as RangeWeapon;
      if (rangeWeapon) {        
         rangeWeapon.OnShoot -= Recoil;
         rangeWeapon.OnShoot += Recoil;
         recoilOffset = Vector3.zero;
         swayPositionOffset = Vector3.zero;
      }
      targetPosition = currentWeapon.IsAiming ? AimPosition : HipPosition; 

   }
   
   


   
   
   private void Update() {
      if (currentWeapon) {
         Vector3 desiredHandsPosition = currentWeapon.IsAiming ? AimPosition + bobingOffset : HipPosition + bobingOffset; 
         targetPosition = Vector3.Lerp(targetPosition, desiredHandsPosition, Time.deltaTime * aimSmooth);
         weaponSlots[currentWeaponIndex].transform.localPosition = weaponPosition;
         transform.localPosition = targetPosition + recoilOffset + swayPositionOffset;
         if (rangeWeapon) {
            recoilOffset = Vector3.Lerp(recoilOffset, Vector3.zero, recoilSmooth * Time.deltaTime);
         }
         
      }
   }
   
   private void Recoil() {
      recoilOffset -= new Vector3(0, recoilY, recoilZ);
   }


   public void Sway(Vector3 lookDirection) {
      //rotation
      Quaternion targetRotation = Quaternion.Euler(lookDirection.y * rotationSwayX, lookDirection.x * rotationSwayY, -lookDirection.x * rotationSwayX);
      transform.localRotation = Quaternion.Slerp(transform.localRotation,initialRotation * targetRotation, Time.deltaTime * positionSwaySmooth);
         
      //position
         Vector3 desiredSway = new Vector3(-lookDirection.x * swayAmount, -lookDirection.y * swayAmount, 0);
         desiredSway.x = Mathf.Clamp(swayPositionOffset.x, -maxSwayAmount, maxSwayAmount);
         desiredSway.y = Mathf.Clamp(swayPositionOffset.y, -maxSwayAmount, maxSwayAmount);
         swayPositionOffset = Vector3.Lerp(swayPositionOffset, desiredSway, positionSwaySmooth * Time.deltaTime);
   }

   public void Bobing(Vector3 moveDirection) {
      float bobingY = Mathf.Cos(Time.time * bobbingSpeedY) * moveDirection.y * bobbingAmountY;
      float bobingX = Mathf.Sin(Time.time * bobbingSpeedX) * moveDirection.x * bobbingAmountX;
      bobingOffset = new Vector3(bobingX, bobingY, 0); 
   }
}
