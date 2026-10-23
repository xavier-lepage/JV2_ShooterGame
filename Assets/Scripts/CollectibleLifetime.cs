using UnityEngine;

public class CollectibleLifetime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 15f;
    [SerializeField] private float timer = 0f;

	private Collider collider;
	private Renderer[] renderers;

	public void Spawn(Vector3 position)
	{
		gameObject.transform.position = position;
		gameObject.SetActive(true);
		timer = lifeTime;

		if (collider != null) collider.enabled = true;
		foreach (Renderer renderer in renderers)
		{
			renderer.enabled = true;
		}
	}

	private void Start()
	{
		collider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
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
