using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName; 
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private int maxAmmo;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reloadSound;
    
    public float FireRate => fireRate;
    public int Damage => damage;
    public float Range => range;
    public int MaxAmmo => maxAmmo;
    public AudioClip ShotSound => shotSound;
    public AudioClip ReloadSound => reloadSound;
}
