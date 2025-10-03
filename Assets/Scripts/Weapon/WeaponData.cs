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
    [SerializeField] private AudioClip shotSound;

    [Header("Position data")]
    [SerializeField] private Vector3 weaponPosition;
    [SerializeField] private Vector3 hipPosition;
    [SerializeField] private Vector3 aimPosition;
    [SerializeField] private Quaternion rotation;

    [Header("Weapon Recoil data")]
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;
    [SerializeField] private float recoilSmooth;
    
    
    [Header("Camera Recoil data")]
    [SerializeField] private float cameraMinimumRecoilX;
    [SerializeField] private float cameraMaximumRecoilX;
    [SerializeField] private float cameraMinimumRecoilY;
    [SerializeField] private float cameraMaximumRecoilY;
    [SerializeField] private float cameraSnappiness;
    [SerializeField] private float cameraReturnSpeed;

    [Header("Bobbing")]
    [SerializeField] private float bobbingSpeedX;

    [SerializeField] private float bobbingAmountX;
    [SerializeField] private float bobbingSpeedY;
    [SerializeField] private float bobbingAmountY;
    
    public GameObject WeaponMesh => weaponMesh;
    public float FireRate => fireRate;
    public int Damage => damage;
    public float Range => range;
    public AudioClip ShotSound => shotSound;
    
    public Vector3 WeaponPosition => weaponPosition;
    public Vector3 HipPosition => hipPosition;
    public Vector3 AimPosition => aimPosition;
    
    public float BobbingSpeedX => bobbingSpeedX;
    public float BobbingAmountX => bobbingAmountX;
    public float BobbingSpeedY => bobbingSpeedY;
    public float BobbingAmountY => bobbingAmountY;
    
    
    
     public float RecoilY => recoilY;
     public float RecoilZ => recoilZ;
     public float RecoilSmooth => recoilSmooth;
    
    public float CameraMinimumRecoilX => cameraMinimumRecoilX;
    public float CameraMaximumRecoilX => cameraMaximumRecoilX;
    public float CameraMinimumRecoilY => cameraMinimumRecoilY;
    public float CameraMaximumRecoilY => cameraMaximumRecoilY;
    public float CameraSnappiness => cameraSnappiness;
    public float CameraReturnSpeed => cameraReturnSpeed;
}
