using UnityEngine;
using UnityEngine.AI;

public class AlienSpawn : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool hasLanded = false;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
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
    }

    public void Spawn(Vector3 position)
    {
        agent.enabled = false;
        transform.position = position;
        gameObject.SetActive(true);
    }
}
