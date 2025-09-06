using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float attackDistance;
    [SerializeField] private float detectionDistance;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    public float AttackDistance => attackDistance;
    public float DetectionDistance => detectionDistance;
    public int Health => health;
    public int Damage => damage;
    public float PatrolSpeed => patrolSpeed;
    public float ChaseSpeed => chaseSpeed;
}
