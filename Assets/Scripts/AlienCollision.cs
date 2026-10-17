using UnityEngine;

public class AlienCollision : MonoBehaviour
{
    [SerializeField] private float coneAngle = 30f;

    public bool CheckHeadStomp(Vector3 contactPoint)
    {
        Vector3 direction = contactPoint - transform.position;
        float angle = Vector3.Angle(Vector3.up, direction);
        Debug.Log("Angle " + angle);

        return Vector3.Angle(Vector3.up, direction) <= coneAngle;
    }
}
