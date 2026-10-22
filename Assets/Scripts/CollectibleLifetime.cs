using UnityEngine;

public class CollectibleLifetime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 15f;
    [SerializeField] private float timer = 0f;

    
    void Update()
    {
        if (!gameObject.activeSelf) return;
        
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            gameObject.SetActive(false);
        }
    }

    public void Spawn(Vector3 position)
    {
        gameObject.transform.position = position;
        gameObject.SetActive(true);
        timer = lifeTime;
    }
}
