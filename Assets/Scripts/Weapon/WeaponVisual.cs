using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
   [SerializeField] private RangeWeapon rangeWeapon;
   
   [SerializeField] private Transform hipFirePosition;
   [SerializeField] private Transform aimFirePosition;
   [SerializeField] private float recoilSmooth = 1.8f;
   [SerializeField] private float aimSmooth = 0.5f;

   [SerializeField] private float recoilY = 0.002f;
   [SerializeField] private float recoilZ = 0.015f;

   private void OnEnable() {
      rangeWeapon.OnShoot += Recoil;
   }
   
   private Vector3 targetPosition;
   private Vector3 recoilOffset;
   
   private void Update() {
      Vector3 desiredPosition = rangeWeapon.IsAiming ? aimFirePosition.localPosition : hipFirePosition.localPosition; 
      targetPosition = Vector3.Lerp(targetPosition, desiredPosition, Time.deltaTime * aimSmooth);
      recoilOffset = Vector3.Lerp(recoilOffset, Vector3.zero, recoilSmooth * Time.deltaTime);
      transform.localPosition = targetPosition + recoilOffset;

   }
   
   protected void Recoil() {
      recoilOffset -= new Vector3(0, recoilY, recoilZ);
   }
}
