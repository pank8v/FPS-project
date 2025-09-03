using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float attackDistance;

   [SerializeField] private NavMeshAgent navMeshAgent;
    private float distance;
    private void Update() {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < attackDistance) {
            Debug.Log("attck");
            navMeshAgent.isStopped = true;
        }
        else {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = target.position;
        }
    }
}
