using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected int damage;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    public virtual void Shoot(Vector3 position, Vector3 direction)
    {
        transform.position = position;

        gameObject.SetActive(true);

        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Alien") || collision.gameObject.CompareTag("Spawner"))
        {
            HealthManager target = collision.gameObject.GetComponent<HealthManager>();
            if (target != null) target.Hurt(damage);
        }

        gameObject.SetActive(false);
    }
}
