using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class HUD : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private WeaponInventory weaponInventory;
    private Weapon currentWeapon;
    private RangeWeapon rangeWeapon;
    
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI reserveAmmoText;

    private float idleCrosshair = 100f;
    [SerializeField] private float shotOffset = 100f;
    private float targetCrosshair;
    [SerializeField] private float crosshairSmoothing = 15f;
   
    [SerializeField] private float shootKick = 30f;   
    [SerializeField] private float returnSpeed = 5f; 
    private float currentOffset = 0f;



    private void OnEnable() {
        weaponInventory.OnWeaponSwitch += UpdateHUD;
        uiController.OnAmmoChanged += UpdateAmmo;
    }
    
    private void OnDisable() {
        weaponInventory.OnWeaponSwitch -= UpdateHUD;
        uiController.OnAmmoChanged -= UpdateAmmo;
    }
    
    
    public void UpdateHUD(Weapon weapon, int _) {
        if (rangeWeapon != null)
        {
            rangeWeapon.OnShoot -= UpdateAmmo;
            rangeWeapon.OnShoot -= SetCrosshair;
            rangeWeapon.OnReloadEnd -= UpdateAmmo;
        }
        
        
        rangeWeapon = weapon as RangeWeapon;
        if (rangeWeapon) {
            rangeWeapon.OnShoot += UpdateAmmo;
            rangeWeapon.OnShoot += SetCrosshair;
            rangeWeapon.OnReloadEnd += UpdateAmmo;
        }
        UpdateAmmo(); 
    }
    
    
    private void Awake() {
        rangeWeapon = currentWeapon as RangeWeapon;
        if (!rangeWeapon) {
            currentAmmoText.text = "";
            reserveAmmoText.text = "";
        }
    }

    private void Update() {
        currentOffset = Mathf.Lerp(currentOffset, 0f, Time.deltaTime * returnSpeed);
        crosshair.sizeDelta = new Vector2(idleCrosshair + currentOffset, idleCrosshair + currentOffset);
    }
    
    private void SetCrosshair() {
        currentOffset += shootKick;
    }

    public void UpdateAmmo() {
        if (rangeWeapon) {
            int currentAmmo = rangeWeapon.currentAmmo;
            int reserveAmmo = rangeWeapon.GetAmmoCount();
            if (reserveAmmo != null) {
                currentAmmoText.text = $"<color=#FF6AFF>{currentAmmo.ToString("D3")}</color>";
                reserveAmmoText.text = $"<color=#6AFFFE>{reserveAmmo.ToString("D3")}</color>";
            }

        }
        else {
            currentAmmoText.text = "";
            reserveAmmoText.text = "";
        }

    }
}
