using UnityEngine;
using UnityEngine.AI;

public class AlienSpawn : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rigidbody;
    private bool hasLanded = false;

    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        agent.enabled = false;
        rigidbody.isKinematic = false;
    }

    void OnDisable()
    {
        hasLanded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Floor") || hasLanded) return;

        agent.enabled = true;
        hasLanded = true;
        rigidbody.isKinematic = true;
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}
