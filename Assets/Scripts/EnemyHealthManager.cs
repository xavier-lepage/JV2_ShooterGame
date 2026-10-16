using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : HealthManager
{
    [SerializeField] private UnityEvent<Vector3> onDeath;

    public override void Die()
    {
        base.Die();
        onDeath.Invoke(gameObject.transform.position);
    }
}
