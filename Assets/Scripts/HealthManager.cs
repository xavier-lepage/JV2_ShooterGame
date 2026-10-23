using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	[SerializeField] private int baseHealth;
	[SerializeField] protected int currentHealth;

	[SerializeField] private AudioClip deathSFXClip;
	[SerializeField] private AudioClip damageSFXClip;
	private AudioSource healthSFX;

	private Collider collider;
	private Renderer[] renderers;

	private void Start()
	{
		healthSFX = GetComponent<AudioSource>();
		collider = GetComponent<Collider>();
		renderers = GetComponentsInChildren<Renderer>();
	}

	private void OnEnable()
	{
		currentHealth = baseHealth;
		if (collider != null) collider.enabled = true;
		foreach (Renderer renderer in renderers)
		{
			renderer.enabled = true;
		}
	}

	public virtual void Hurt(int damage)
	{
		if (healthSFX != null && damageSFXClip != null) healthSFX.PlayOneShot(damageSFXClip);
		currentHealth -= damage;
		if (isDead())
		{
			Die();
		}
	}

	public virtual void Die()
	{
		if (healthSFX == null || deathSFXClip == null)
		{
			gameObject.SetActive(false);
			return;
		}

		healthSFX.PlayOneShot(deathSFXClip);
		StartCoroutine(WaitForDeathSFX());
	}

	protected bool isDead()
	{
		return currentHealth <= 0;
	}

	private IEnumerator WaitForDeathSFX()
	{
		if (collider != null) collider.enabled = false;
		foreach (Renderer renderer in renderers)
		{
			renderer.enabled = false;
		}

		yield return new WaitForSeconds(deathSFXClip.length);

		gameObject.SetActive(false);
		StopCoroutine(WaitForDeathSFX());
	}
}
