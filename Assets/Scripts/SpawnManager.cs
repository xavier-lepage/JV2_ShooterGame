using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject alienPrefab;
	[SerializeField] private GameObject spawnerPrefab;
	[SerializeField] private Vector3[] spawnersLocation;
	private GameObject[] spawners;
	private GameObject[] aliens;

	[SerializeField] private int maxActiveAliens = 20;
	[SerializeField] private int maxAliensSpawned = 500;
	private int currentAliensSpawned = 0;
	private int activeAliens = 0;

	[SerializeField] private float spawnInterval = 5f;
	[SerializeField] private float spawnTimer = 0f;

	private List<GameObject> activeSpawners = new List<GameObject>();

	public void OnAlienDeath()
	{
		activeAliens = Mathf.Max(activeAliens - 1, 0);
	}

	void Awake()
	{
		spawners = new GameObject[spawnersLocation.Length];
		for (int i = 0; i < spawnersLocation.Length; i++)
		{
			spawners[i] = Instantiate(spawnerPrefab);
			spawners[i].transform.position = spawnersLocation[i];
			activeSpawners.Add(spawners[i]);
		}
		aliens = new GameObject[maxActiveAliens];
		for (int i = 0; i < maxActiveAliens; i++)
		{
			aliens[i] = Instantiate(alienPrefab);
			aliens[i].SetActive(false);
		}
	}

	void Update()
	{
		if (activeSpawners.Count == 0 || currentAliensSpawned >= maxAliensSpawned) return;
		ManageActiveSpawners();

		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0f)
		{
			SpawnAlien();
			spawnTimer = spawnInterval;
		}
	}

	private void ManageActiveSpawners()
	{
		foreach (GameObject spawner in spawners)
		{
			if (!spawner.activeSelf) activeSpawners.Remove(spawner);
		}
	}
	private void SpawnAlien()
	{
		int index = Random.Range(0, activeSpawners.Count);
		GameObject spawner = activeSpawners[index];

		foreach (GameObject alien in aliens)
		{
			if (alien.activeSelf) continue;
				
			AlienSpawn alienSpawn = alien.GetComponent<AlienSpawn>();
			alienSpawn.Spawn(spawner.transform.position);
			currentAliensSpawned++;
			activeAliens++;
			spawnTimer = spawnInterval;
			break;
		}
	}

	public bool IsLevelComplete()
    {
        return activeAliens == 0 && (activeSpawners.Count == 0 || maxAliensSpawned == currentAliensSpawned);
    }
}
