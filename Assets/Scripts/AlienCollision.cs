using UnityEngine;

public class AlienCollision : MonoBehaviour
{
    [SerializeField] private float coneAngle = 15f;
    private HealthManager healthManager;

    void Start()
    {
        healthManager = gameObject.GetComponent<HealthManager>();    
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;


        if (!CheckHeadStomp(collision.GetContact(0).point))
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(1);
        }
        healthManager.Die();
    }

    public bool CheckHeadStomp(Vector3 contactPoint)
    {
        Vector3 direction = contactPoint - transform.position;

        return Vector3.Angle(transform.up, direction) <= coneAngle;
    }
}
