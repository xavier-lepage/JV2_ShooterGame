using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent onPickup;
	[SerializeField] private AudioClip pickupSFXClip;

	private AudioSource pickupSFX;
	private Collider collider;
	private Renderer[] renderers;

	private void Start()
	{
		pickupSFX = GetComponent<AudioSource>();

		collider = GetComponent<Collider>();
		renderers = GetComponentsInChildren<Renderer>();
	}

	private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        onPickup.Invoke();
		pickupSFX.PlayOneShot(pickupSFXClip);
		StartCoroutine(WaitForPickupSFX());
    }

	private IEnumerator WaitForPickupSFX()
	{
		if (collider != null) collider.enabled = false;
		foreach (Renderer renderer in renderers)
		{
			renderer.enabled = false;
		}

		yield return new WaitForSeconds(pickupSFXClip.length);

		gameObject.SetActive(false);
		StopCoroutine(WaitForPickupSFX());
	}
}
