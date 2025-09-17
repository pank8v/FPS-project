using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
   [SerializeField] private RangeWeapon rangeWeapon;
   [SerializeField] private Transform handsTransform;
   
   [Header("Aim Settings")]
    private Vector3 hipFirePosition => rangeWeapon.WeaponData.HipFirePosition;

   private Vector3 handsHipPosition => rangeWeapon.WeaponData.HandsHipPosition;
   private Vector3 handsAimPosition => rangeWeapon.WeaponData.HandsAimPosition;
   private Vector3 aimFirePosition => rangeWeapon.WeaponData.AimFirePosition;
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
   
   private Vector3 handsTargetPosition;
 //  private Vector3 targetPosition;
   private Vector3 recoilOffset;
   private Vector3 swayPositionOffset;
//   private Quaternion initialRotation;
   private Quaternion handsInitialRotation;


   private void Start() {
   //   initialRotation = transform.localRotation;
      handsInitialRotation = handsTransform.localRotation;
   }
   
   private void OnEnable() {
      rangeWeapon.OnShoot += Recoil;
      
    //  targetPosition = rangeWeapon.IsAiming ? aimFirePosition : hipFirePosition; 
      handsTargetPosition = rangeWeapon.IsAiming ? handsAimPosition : handsHipPosition; 
      recoilOffset = Vector3.zero;
      swayPositionOffset = Vector3.zero;
   //   transform.localPosition = targetPosition + recoilOffset;
   }

   private void OnDisable() {
      rangeWeapon.OnShoot -= Recoil;
   }
   
   

   
   private void Update() {
    //  Vector3 desiredPosition = rangeWeapon.IsAiming ? aimFirePosition : hipFirePosition; 
      Vector3 desiredHandsPosition = rangeWeapon.IsAiming ? handsAimPosition : handsHipPosition; 

    //  targetPosition = Vector3.Lerp(targetPosition, desiredPosition, Time.deltaTime * aimSmooth);
      handsTargetPosition = Vector3.Lerp(handsTargetPosition, desiredHandsPosition, Time.deltaTime * aimSmooth);
      recoilOffset = Vector3.Lerp(recoilOffset, Vector3.zero, recoilSmooth * Time.deltaTime);
      // transform.localPosition = targetPosition + recoilOffset * 1.0f + swayPositionOffset * 1.0f;
      handsTransform.localPosition = handsTargetPosition + recoilOffset+ swayPositionOffset;
   }
   
   private void Recoil() {
      recoilOffset -= new Vector3(0, recoilY, recoilZ);
   }


   public void Sway(Vector3 lookDirection) {
      //rotation
      Quaternion targetRotation = Quaternion.Euler(lookDirection.y * rotationSwayY,
         lookDirection.x * rotationSwayX, -lookDirection.x * rotationSwayX * 0.5f);
      //   transform.localRotation = Quaternion.Slerp(transform.localRotation,initialRotation * targetRotation, Time.deltaTime * rotationSwaySmooth);
         handsTransform.localRotation = Quaternion.Slerp(handsTransform.localRotation,handsInitialRotation * targetRotation, Time.deltaTime * positionSwaySmooth);
         
      //position
         Vector3 desiredSway = new Vector3(-lookDirection.x * swayAmount, -lookDirection.y * swayAmount, 0);
         desiredSway.x = Mathf.Clamp(swayPositionOffset.x, -maxSwayAmount, maxSwayAmount);
         desiredSway.y = Mathf.Clamp(swayPositionOffset.y, -maxSwayAmount, maxSwayAmount);
         swayPositionOffset = Vector3.Lerp(swayPositionOffset, desiredSway, positionSwaySmooth * Time.deltaTime);
   }
}
