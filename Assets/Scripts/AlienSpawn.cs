using UnityEngine;
using UnityEngine.AI;

public class AlienSpawn : MonoBehaviour
{
    private Rigidbody rigidbody;
    private NavMeshAgent agent;
    private bool hasLanded = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    void OnDisable()
    {
        rigidbody.linearVelocity = Vector3.zero;
        agent.enabled = false;
        hasLanded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor") || hasLanded) return;

        agent.enabled = true;
        hasLanded = true;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}
