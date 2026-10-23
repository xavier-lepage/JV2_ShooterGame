using UnityEngine;

public class CollectibleLifetime : MonoBehaviour
{
	[SerializeField] private float lifeTime = 15f;
	[SerializeField] private float timer = 0f;
	[SerializeField] private AudioClip spawnSFXClip;

	private AudioSource spawnSFX;
	private Collider collider;
	private Renderer[] renderers;

	void Awake() {
		collider = GetComponent<Collider>();
		renderers = GetComponentsInChildren<Renderer>();
		spawnSFX = GetComponent<AudioSource>();
	}

	void OnEnable()
	{
		spawnSFX.PlayOneShot(spawnSFXClip);
		if (collider != null) collider.enabled = true;
		foreach (Renderer renderer in renderers)
		{
			renderer.enabled = true;
		}
	}

	public void Spawn(Vector3 position)
	{
		gameObject.transform.position = position;
		gameObject.SetActive(true);
		timer = lifeTime;
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
