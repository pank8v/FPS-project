using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName; 
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private int maxAmmo;
    
    public float FireRate => fireRate;
}
