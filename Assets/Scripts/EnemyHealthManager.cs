using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : HealthManager
{
    [SerializeField] private UnityEvent<GameObject> onDeath;

    public override void Die()
    {
        base.Die();
        onDeath.Invoke(gameObject);
    }
}
