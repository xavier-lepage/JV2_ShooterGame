using UnityEngine;
using UnityEngine.AI;

public class AlienMovement : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rigidbody;

    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Vector3 direction = goal.transform.position - transform.position;
        direction.y = 0f;
        float yRotation = Quaternion.LookRotation(direction).eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    void OnDisable()
    {
        if (rigidbody == null) return;

        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (navMeshAgent != null && navMeshAgent.enabled)
            navMeshAgent.destination = goal.transform.position;
    }
}
