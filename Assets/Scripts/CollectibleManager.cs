using UnityEngine;
using UnityEngine.Events;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private float dropRarity = 15f;
    [SerializeField] private GameObject[] pickupPrefabs;

    private int arraySize = 10;
    private GameObject[,] pickups;

	public void OnAlienDeath(Vector3 position)
	{
		if (Random.Range(0f, 1f) > (1f / dropRarity)) return;

		int row = Random.Range(0, pickups.GetLength(0));
		for (int i = 0; i < pickups.GetLength(1); i++)
		{
			GameObject collectible = pickups[row, i];
			if (collectible.activeSelf) continue;

			collectible.GetComponent<CollectibleLifetime>().Spawn(position);
			break;
		}
	}

	private void Start()
    {
        pickups = new GameObject[pickupPrefabs.Length, arraySize];
        for (int i = 0; i < pickupPrefabs.Length; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                pickups[i, j] = Instantiate(pickupPrefabs[i]);
                pickups[i, j].SetActive(false);
            }
        }
    }
}
