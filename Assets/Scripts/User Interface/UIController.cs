using UnityEngine;
using System;
public class UIController : MonoBehaviour
{
    [SerializeField] private WeaponInventory weaponInventory;
    [SerializeField] private PlayerHealth playerHealth;

    public event Action<float> OnHealthChanged;
    public event Action OnAmmoChanged;
    public event Action<int> OnHealChanged;


    private void OnEnable() {
        playerHealth.OnPlayerHealthChanged += HandlePlayerHealthChanged;
        weaponInventory.OnAmmoChanged += HandleAmmoChanged;
        weaponInventory.OnHealAmountChanged += HandleHealAmountChanged;
    }

    private void OnDisable() {
        playerHealth.OnPlayerHealthChanged -= HandlePlayerHealthChanged;
    }
    
    private void HandlePlayerHealthChanged(float health) {
        OnHealthChanged?.Invoke(health);
    }


    private void HandleAmmoChanged() {
        OnAmmoChanged?.Invoke();
    }

    private void HandleHealAmountChanged(int healAmount) {
        OnHealChanged?.Invoke(healAmount);
    }
}
