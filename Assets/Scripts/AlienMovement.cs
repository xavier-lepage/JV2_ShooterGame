using UnityEngine;
using UnityEngine.AI;

public class AlienMovement : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        Vector3 direction = goal.transform.position - transform.position;
        direction.y = 0f;
        float yRotation = Quaternion.LookRotation(direction).eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    void Update()
    {
        if (navMeshAgent != null && navMeshAgent.enabled)
            navMeshAgent.destination = goal.transform.position;
    }
}
