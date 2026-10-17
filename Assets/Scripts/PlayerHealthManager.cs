using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthManager : HealthManager
{
    [SerializeField] private float immunePeriod;
    [SerializeField] private UnityEvent<string> healthEvent;

    private float immunityTimer;

    public override void Hurt(int damage)
    {
        if (immunityTimer > 0.0f) return;

        base.Hurt(damage);
        healthEvent.Invoke(currentHealth.ToString());
        immunityTimer = immunePeriod;
    }

    private void Update()
    {
        ManageImmunePeriod();
    }

    private void ManageImmunePeriod()
    {
        if (immunityTimer > 0.0f) immunityTimer -= Time.deltaTime;
    }
}
