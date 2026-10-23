using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private int damage = 1;
    private HealthManager healthManager;

    void Awake()
    {
        healthManager = gameObject.GetComponent<HealthManager>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || !other.gameObject.CompareTag("Alien")) return;

        AlienCollision alienCollision = other.GetComponent<AlienCollision>();
        EnemyHealthManager alienHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();

        if (!alienCollision.CheckHeadStomp())
        {
            healthManager.Hurt(damage);
        }
        alienHealthManager.Die();
    }
}
