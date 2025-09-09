using UnityEngine;
using System;

public class RangeWeapon : Weapon, IReloadable
{
    [SerializeField] private float recoilSmooth = 1.8f;
    public event Action OnReload;
    public int currentAmmo { get; private set; }

    Vector3 recoilOffset = new Vector3(0, 0.002f, 0.015f);
    Vector3 targetPosition = new Vector3(0.211f, -0.274f, 0.489f);
    
    protected void Awake() {
        currentAmmo = weaponData.MaxAmmo;
    }


    protected void Update() {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * recoilSmooth);
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
        transform.localPosition -= recoilOffset;
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
