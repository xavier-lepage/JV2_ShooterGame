using UnityEngine;

public class CollectibleRotator : MonoBehaviour
{
    void Update()
    {
        if (!gameObject.activeSelf) return;

        gameObject.transform.Rotate(new Vector3(0f, 45f, 0f) * Time.deltaTime);
    }
}
