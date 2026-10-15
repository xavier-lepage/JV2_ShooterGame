using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private float immunePeriod;

    private float immunityTimer;

    private void Update()
    {
        ManageImmunePeriod();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void Hurt(int damage)
    {
        if (immunityTimer > 0.0f) return;

        immunityTimer = immunePeriod;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        onDeath.Invoke();
    }

    private void ManageImmunePeriod()
    {
        if (immunityTimer > 0.0f) immunityTimer -= Time.deltaTime;
    }
}
