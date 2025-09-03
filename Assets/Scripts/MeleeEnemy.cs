using UnityEngine;
using UnityEngine.Rendering;

public class MeleeEnemy : Enemy
{
    protected override void Patrol() {
        Debug.Log("Patrol");
    }

    protected override void Chase() {
        Debug.Log("chase");
    }

    protected override void Attack() {
        Debug.Log("attack");
    }
}
