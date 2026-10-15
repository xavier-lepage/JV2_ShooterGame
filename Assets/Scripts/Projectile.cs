using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    protected Rigidbody rb;

    protected virtual void Start()
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
        gameObject.SetActive(false);
    }
}
