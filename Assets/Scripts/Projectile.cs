using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] protected int damage;

	protected Rigidbody rb;

	protected virtual void Start()
	{
		rb = GetComponent<Rigidbody>();
		gameObject.SetActive(false);
	}

	public virtual void Shoot(Vector3 position, Vector3 direction, Quaternion rotation)
	{
		transform.position = position;
		transform.rotation = rotation;

		gameObject.SetActive(true);

		rb.linearVelocity = direction * speed;
	}

	protected virtual void OnCollisionEnter(Collision collision)
	{
		ApplyDamage(collision.gameObject);
		gameObject.SetActive(false);
	}

	protected void ApplyDamage(GameObject target)
	{
		if (target.CompareTag("Alien") || target.CompareTag("Spawner"))
		{
			HealthManager targetHealthManaher = target.GetComponent<HealthManager>();
			if (targetHealthManaher != null) targetHealthManaher.Hurt(damage);
		}
	}
}
