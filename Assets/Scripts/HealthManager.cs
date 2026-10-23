using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int baseHealth;
    [SerializeField] protected int currentHealth;

    private void OnEnable()
    {
        currentHealth = baseHealth;
    }

    public virtual void Hurt(int damage)
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
