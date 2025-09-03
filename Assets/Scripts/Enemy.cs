using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
   protected abstract void Chase();
   protected abstract void Patrol();
   protected abstract void Attack();
}
