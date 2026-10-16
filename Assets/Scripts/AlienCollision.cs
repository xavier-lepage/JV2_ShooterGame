using UnityEngine;

public class AlienCollision : MonoBehaviour
{
    [SerializeField] private float coneAngle = 15f;
    [SerializeField] private int damage;
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
            HealthManager player;
            if (collision.gameObject.TryGetComponent<HealthManager>(out player)) player.Hurt(damage);
        }
        healthManager.Die();
    }

    public bool CheckHeadStomp(Vector3 contactPoint)
    {
        Vector3 direction = contactPoint - transform.position;

        return Vector3.Angle(transform.up, direction) <= coneAngle;
    }
}
