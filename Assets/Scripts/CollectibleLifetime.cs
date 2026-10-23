using UnityEngine;

public class CollectibleLifetime : MonoBehaviour
{
	[SerializeField] private float lifeTime = 15f;
	[SerializeField] private float timer = 0f;
	[SerializeField] private AudioClip spawnSFXClip;

	private AudioSource spawnSFX;
	private Collider collider;
	private Renderer[] renderers;

	public void Spawn(Vector3 position)
	{
		collider = GetComponent<Collider>();
		renderers = GetComponentsInChildren<Renderer>();
		spawnSFX = GetComponent<AudioSource>();
		gameObject.transform.position = position;
		gameObject.SetActive(true);
		spawnSFX.PlayOneShot(spawnSFXClip);
		timer = lifeTime;

		if (collider != null) collider.enabled = true;
		foreach (Renderer renderer in renderers)
		{
			renderer.enabled = true;
		}
	}

	private void Update()
	{
		if (!gameObject.activeSelf) return;
		
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			timer = 0f;
			gameObject.SetActive(false);
		}
	}
}
