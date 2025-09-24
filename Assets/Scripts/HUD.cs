using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class HUD : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI reserveAmmoText;
    private Weapon currentWeapon;
    private RangeWeapon rangeWeapon;

    
    public void UpdateHUD(Weapon weapon) {
        if (rangeWeapon != null)
        {
            rangeWeapon.OnShoot -= UpdateAmmo;
            rangeWeapon.OnReloadEnd -= UpdateAmmo;
        }
        
        
        rangeWeapon = weapon as RangeWeapon;
        if (rangeWeapon) {
            rangeWeapon.OnShoot += UpdateAmmo;
            rangeWeapon.OnReloadEnd += UpdateAmmo;
            UpdateAmmo(); 
        }
    }
    
    
    private void Awake() {
        rangeWeapon = currentWeapon as RangeWeapon;
        if (!rangeWeapon) {
            currentAmmoText.text = "";
            reserveAmmoText.text = "";
        }
    }

    private void Start() {
        if (rangeWeapon) {
            UpdateAmmo();
        }
    }
    


    public void UpdateAmmo() {
        int currentAmmo = rangeWeapon.currentAmmo;
        currentAmmoText.text = $"<color=#FF6AFF>{currentAmmo.ToString("D3")}</color>";
        reserveAmmoText.text = $"<color=#6AFFFE>{rangeWeapon.GetAmmoCount().ToString("D3")}</color> <color=#6AFFFE></color>";
    }
}
