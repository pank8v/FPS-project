using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName; 
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private int maxAmmo;
    
    
    public float FireRate => fireRate;
    public int Damage => damage;
    public float Range => range;
    public int MaxAmmo => maxAmmo;
}
