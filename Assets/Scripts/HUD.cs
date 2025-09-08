using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class HUD : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private TextMeshProUGUI ammoText;
    private RangeWeapon rangeWeapon;

    
    private void OnEnable() {
        if (rangeWeapon != null) {
            rangeWeapon.OnShoot += UpdateAmmo;
            rangeWeapon.OnReload += UpdateAmmo;
        }
    }

    private void OnDisable() {
        if (rangeWeapon != null) {
            rangeWeapon.OnShoot -= UpdateAmmo;
            rangeWeapon.OnReload -= UpdateAmmo;
        }
    }
    
    
    private void Awake() {
        if (currentWeapon is RangeWeapon rw) {
            rangeWeapon = rw;
        }
        else {
            ammoText.text = "";
        }
    }

    private void Start() {
        UpdateAmmo();
    }
    


    private void UpdateAmmo() {
        ammoText.text = $"{rangeWeapon.currentAmmo} / {currentWeapon.WeaponData.MaxAmmo}";
    }
}
