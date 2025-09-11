using UnityEngine;
using System;

public class RangeWeapon : Weapon, IReloadable
{
    [SerializeField] private Transform hipFirePosition;
    [SerializeField] private Transform aimFirePosition;
    [SerializeField] private float recoilSmooth = 1.8f;
    [SerializeField] private float aimSmooth = 0.5f;
    public event Action OnReload;
    public int currentAmmo { get; private set; }
    
    [SerializeField] private float recoilY = 0.002f;
    [SerializeField] private float recoilZ = 0.015f;
   
    Vector3 hipTargetPosition = new Vector3(0.176f, -0.23f, 0.479f);
    Vector3 aimTargetPosition = new Vector3(0, 0, 0);
    private Vector3 targetPosition;
    
    
    private Vector3 recoilOffset;
    
    protected void Awake() {
        currentAmmo = weaponData.MaxAmmo;
    }


    protected void Update() {
        Vector3 desiredPosition = isAiming ? aimFirePosition.localPosition : hipFirePosition.localPosition; 
        targetPosition = Vector3.Lerp(targetPosition, desiredPosition, Time.deltaTime * aimSmooth);
        recoilOffset = Vector3.Lerp(recoilOffset, Vector3.zero, recoilSmooth * Time.deltaTime);
        
        transform.localPosition = targetPosition + recoilOffset;
    }
    
    protected override void Shoot() {
        Recoil();
        currentAmmo--;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range)) {
            Debug.Log(hit.collider.name);
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null) {
                var health = enemy.GetComponent<Health>();
                health.ApplyDamage(damage);
                enemy.OnPlayerDetected();
            }
        }
    }


    protected void Recoil() {
        recoilOffset -= new Vector3(0, recoilY, recoilZ);
    }
    
    
    
    protected override bool CanFire() {
        return currentAmmo > 0;
    }

    
    
    public void Reload() {
        if (currentAmmo < maxAmmo) {
            currentAmmo = maxAmmo;
            OnReload?.Invoke();
        }
    }
    
}
