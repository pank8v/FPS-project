using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float attackDistance;
    [SerializeField] private float detectionDistance;

    public float AttackDistance => attackDistance;
    public float DetectionDistance => detectionDistance;
    public int Health => health;
}
