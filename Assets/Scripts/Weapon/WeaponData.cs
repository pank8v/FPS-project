using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Basic data")]
    [SerializeField] private GameObject weaponMesh;
    [SerializeField] private string weaponName; 
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime = 3f;
    private int currentAmmo;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip reloadSound;

    [Header("Position data")]
    [SerializeField] private Vector3 hipFirePosition;
    [SerializeField] private Vector3 aimFirePosition;
    [SerializeField] private Vector3 handsHipPosition;
    [SerializeField] private Vector3 handsAimPosition;
    [SerializeField] private Quaternion handsRotation;
    
    [Header("Recoil data")]
    [SerializeField] private float minimumRecoilX;
    [SerializeField] private float maximumRecoilX;
    [SerializeField] private float minimumRecoilY;
    [SerializeField] private float maximumRecoilY;
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    public GameObject WeaponMesh => weaponMesh;
    public float FireRate => fireRate;
    public int Damage => damage;
    public float Range => range;
    public AmmoType AmmoType => ammoType;
    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;
    public float ReloadTime => reloadTime;
    public float BulletSpeed => bulletSpeed;
    public AudioClip ShotSound => shotSound;
    public AudioClip ReloadSound => reloadSound;
    
    public Vector3 HipFirePosition => hipFirePosition;
    public Vector3 AimFirePosition => aimFirePosition;
    public Vector3 HandsHipPosition => handsHipPosition;
    public Vector3 HandsAimPosition => handsAimPosition;
    public Quaternion HandsRotation => handsRotation;
    
    public float MinimumRecoilX => minimumRecoilX;
    public float MaximumRecoilX => maximumRecoilX;
    public float MinimumRecoilY => minimumRecoilY;
    public float MaximumRecoilY => maximumRecoilY;
    public float Snappiness => snappiness;
    public float ReturnSpeed => returnSpeed;
}
