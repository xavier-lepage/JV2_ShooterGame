using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	[SerializeField] private int baseHealth;
	[SerializeField] protected int currentHealth;

	[SerializeField] private GameObject rendering;

	[SerializeField] private AudioClip deathSFXClip;
	[SerializeField] private AudioClip damageSFXClip;
	private AudioSource healthSFX;

	private Collider collider;

	private void Start()
	{
		healthSFX = GetComponent<AudioSource>();
		collider = GetComponent<Collider>();
	}

	private void OnEnable()
	{
		currentHealth = baseHealth;
		if (rendering != null) rendering.gameObject.SetActive(true);
		if (collider != null) collider.enabled = true;
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
		if (rendering != null) rendering.gameObject.SetActive(false);
		if (collider != null) collider.enabled = false;

		yield return new WaitForSeconds(deathSFXClip.length);

		gameObject.SetActive(false);
		StopCoroutine(WaitForDeathSFX());
	}
}
