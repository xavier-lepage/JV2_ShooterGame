using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject alien;
    private GameObject[] aliens;

    [SerializeField] private int maxActiveAliens = 20;
    [SerializeField] private int maxAliensSpawned = 500;
    private int currentAliensSpawned = 0;

    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnTimer = 0f;

    private System.Collections.Generic.List<GameObject> activeSpawners = new System.Collections.Generic.List<GameObject>();

    void Start()
    {
        foreach (GameObject spawner in spawners)
        {
            activeSpawners.Add(spawner);
        }
        aliens = new GameObject[maxActiveAliens];
        for (int i = 0; i < maxActiveAliens; i++)
        {
            aliens[i] = Instantiate(alien);
            aliens[i].SetActive(false);
        }
    }

    void Update()
    {
        if (activeSpawners.Count == 0 || currentAliensSpawned >= maxAliensSpawned) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnAlien();
            spawnTimer = spawnInterval;
        }
    }

    public void OnSpawnerDeath(GameObject spawner)
    {
        activeSpawners.Remove(spawner);
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
            spawnTimer = spawnInterval;
            break;
        }
    }
}
