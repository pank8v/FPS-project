using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float attackDistance;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float viewAngle;
    [SerializeField] private float viewDistance;
    [SerializeField] private float attackRate;
    public float AttackDistance => attackDistance;
    public int Health => health;
    public int Damage => damage;
    public float PatrolSpeed => patrolSpeed;
    public float ChaseSpeed => chaseSpeed;
    public float ViewAngle => viewAngle;
    public float ViewDistance => viewDistance;
    public float AttackRate => attackRate;
}
