using UnityEngine;
using UnityEngine.Events;

public class CollectibleCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent onPickup;

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        onPickup.Invoke();
        gameObject.SetActive(false);
    }
}
