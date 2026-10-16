using UnityEngine;

public class AlienCollision : MonoBehaviour
{
    [SerializeField] private float coneAngle = 15f;
    [SerializeField] private int damage;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (!CheckHeadStomp(collision.GetContact(0).point))
        {
            collision.gameObject.GetComponent<HealthManager>().Hurt(damage);
        }
        gameObject.SetActive(false);
    }

    public bool CheckHeadStomp(Vector3 contactPoint)
    {
        Vector3 direction = contactPoint - transform.position;

        return Vector3.Angle(transform.up, direction) <= coneAngle;
    }
}
