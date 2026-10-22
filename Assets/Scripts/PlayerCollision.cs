using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private int damage = 1;
    private HealthManager healthManager;

    void Start()
    {
        healthManager = gameObject.GetComponent<HealthManager>();    
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Alien")) return;

        if (!hit.gameObject.GetComponent<AlienCollision>().CheckHeadStomp(hit.point))
        {
            healthManager.Hurt(damage);
        }

        if (hit.gameObject.TryGetComponent<HealthManager>(out HealthManager alien)) alien.Die();
    }
}
