using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int baseHealth = 10;
    [SerializeField] private int currentHealth;
	[SerializeField] private UnityEvent onDeath;

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

    protected virtual void Die()
    {
		onDeath.Invoke();
        gameObject.SetActive(false);
    }
}
