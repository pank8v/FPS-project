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
    public Vector3 HandsHipPosition => handsHipPosition;
    public Vector3 HandsAimPosition => handsAimPosition;
    public Quaternion HandsRotation => handsRotation;
    
    public float BobbingSpeedX => bobbingSpeedX;
    public float BobbingAmountX => bobbingAmountX;
    public float BobbingSpeedY => bobbingSpeedY;
    public float BobbingAmountY => bobbingAmountY;
    
    
    
    public float MinimumRecoilX => minimumRecoilX;
    public float MaximumRecoilX => maximumRecoilX;
    public float MinimumRecoilY => minimumRecoilY;
    public float MaximumRecoilY => maximumRecoilY;
    public float Snappiness => snappiness;
    public float ReturnSpeed => returnSpeed;
}
