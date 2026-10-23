using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthManager : HealthManager
{
    [SerializeField] private float immunePeriod;
    [SerializeField] private UnityEvent<string> healthEvent;
    [SerializeField] private GameObject head;
    private MovePlayer movePlayer;

    private float immunityTimer;

    void Awake()
    {
        movePlayer = gameObject.GetComponent<MovePlayer>();    
    }

    void Start()
    {
        healthEvent.Invoke(currentHealth.ToString());
	}

    protected override void OnEnable()
    {
        head.SetActive(true);
        base.OnEnable();
    }
	
    public void Heal()
    {
        currentHealth++;
        healthEvent.Invoke(currentHealth.ToString());
    }

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

    public override void Die()
    {
        base.Die();
        if (movePlayer != null) movePlayer.disableControl();
    }

    protected override IEnumerator WaitForDeathSFX()
    {
        head.SetActive(false);
        return base.WaitForDeathSFX();
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }
}
