using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class HUD : MonoBehaviour
{ 
    [SerializeField] private WeaponInventory weaponInventory;
   [SerializeField] private Weapon currentWeapon;
   [SerializeField] private TextMeshProUGUI ammoText;
    private RangeWeapon rangeWeapon;



    public void UpdateHUD(Weapon weapon) {
        if (rangeWeapon != null)
        {
            rangeWeapon.OnShoot -= UpdateAmmo;
            rangeWeapon.OnReloadEnd += UpdateAmmo;
        }

        currentWeapon = weapon;
        
        if (weapon is RangeWeapon rw)
        {
            rangeWeapon = rw;
            rangeWeapon.OnShoot += UpdateAmmo;
            rangeWeapon.OnReloadEnd += UpdateAmmo;
        }
        else
        {
            rangeWeapon = null;
        }

        UpdateAmmo(); 
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
        int currentAmmo = rangeWeapon.currentAmmo;
        int maxAmmo = currentWeapon.WeaponData.MaxAmmo;
        ammoText.text = $"<color=#FF6AFF>{currentAmmo}</color> <color=#6AFFFE>/ {maxAmmo}</color>";
    }
}
