using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponData", menuName = "Scriptable Objects/RangeWeaponData")]
public class RangeWeaponData : WeaponData
{
    [Header("Range weapon data")]
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioClip reloadSound;


    public AmmoType AmmoType => ammoType;
    public float ReloadTime => reloadTime;
    public int MaxAmmo => maxAmmo;
    public float BulletSpeed => bulletSpeed;
    public AudioClip ReloadSound => reloadSound;



}
