using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [SerializeField] private float immunePeriod;

    private float immunityTimer;

    public void Heal()
    {
        currentHealth++;
    }

    public override void Hurt(int damage)
    {
        if (immunityTimer > 0.0f) return;

        base.Hurt(damage);
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
