using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
