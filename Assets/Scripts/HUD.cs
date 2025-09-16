using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class HUD : MonoBehaviour
{
    
   [SerializeField] private Weapon currentWeapon;
   [SerializeField] private TextMeshProUGUI ammoText;
    private RangeWeapon rangeWeapon;



    public void UpdateHUD(Weapon weapon) {
        if (rangeWeapon != null)
        {
            rangeWeapon.OnShoot -= UpdateAmmo;
            rangeWeapon.OnReload -= UpdateAmmo;
        }

        currentWeapon = weapon;

        // Если новое оружие дальнобойное, подписываемся
        if (weapon is RangeWeapon rw)
        {
            rangeWeapon = rw;
            rangeWeapon.OnShoot += UpdateAmmo;
            rangeWeapon.OnReload += UpdateAmmo;
        }
        else
        {
            rangeWeapon = null;
        }

        UpdateAmmo(); // сразу обновляем HUD
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
