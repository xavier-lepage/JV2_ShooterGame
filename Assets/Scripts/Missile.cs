using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Missile : Projectile
{
	private const int EXPLOSION_RADIUS = 10;

	[SerializeField] private GameObject rendering;
	[SerializeField] private Explosion explosion;

	Collider collider;

	protected override void Start()
	{
		base.Start();
		collider = GetComponent<SphereCollider>();
	}

	public override void Shoot(Vector3 position, Vector3 direction)
	{
		collider.enabled = true;
		rendering.SetActive(true);

		base.Shoot(position, direction);
	}

	protected override void OnCollisionEnter(Collision collision)
	{
		collider.enabled = false;
		/*	Documentation consultée pour obtenir la OverlapSphere:
			https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Physics.OverlapSphere.html */
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, EXPLOSION_RADIUS);
		foreach (var hitCollider in hitColliders)
		{
			ApplyDamage(hitCollider.gameObject);
		}

		ResetState();

		explosion.Explode();
		StartCoroutine(DisableAfterExplosion());
	}

	private IEnumerator DisableAfterExplosion()
	{
		yield return new WaitWhile(() => explosion.isExploding());

		gameObject.SetActive(false);
	}

	private void ResetState()
	{
		rendering.SetActive(false);
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
}
