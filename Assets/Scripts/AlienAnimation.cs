using UnityEngine;
using UnityEngine.AI;

public class AlienAnimation : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
    }
}
