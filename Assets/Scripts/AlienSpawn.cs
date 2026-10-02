using UnityEngine;

public class AlienSpawn : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}
