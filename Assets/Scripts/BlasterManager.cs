using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BlasterManager : MonoBehaviour
{
	private const uint MISSILE_CRATE_SIZE = 5;
	private const float MULTISHOT_DURATION_INCREMENT = 10.0f;
	private float[] MULTISHOT_ANGLES = { -45.0f, 45.0f };

	[SerializeField] private int bulletCount;
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private Bullet[] bullets;

	[SerializeField] private int missileCount;
	[SerializeField] private Missile missilePrefab;
	[SerializeField] private Missile[] missiles;

	[SerializeField] private UnityEvent<string> missileEvent;
    [SerializeField] private UnityEvent<string> multishotEvent;

	[SerializeField] private float bulletFireRate;
	[SerializeField] private float missileFireRate;

	private float bulletFireTimer;
	private float missileFireTimer;
	private Vector3 relativeBlasterPosition = new Vector3(1.4f, 2.6f, 4f);

	private uint missileStock = 0;

	private ShootAction shootAction;
	private InputAction shootBulletAction;
	private InputAction shootMissileAction;

	private float multishotTimer = 0.0f;

	public void OnPickupMultishot()
	{
		multishotTimer = Mathf.Max(MULTISHOT_DURATION_INCREMENT, multishotTimer + MULTISHOT_DURATION_INCREMENT);
	}

	public void OnPickupMissiles()
	{
		missileStock += MISSILE_CRATE_SIZE;
	}

	void Awake()
	{
		shootBulletAction = InputSystem.actions.FindAction("ShootBullet");
		shootMissileAction = InputSystem.actions.FindAction("ShootMissile");

		bullets = new Bullet[bulletCount];
		for (int i = 0; i < bullets.Length; i++)
		{
			bullets[i] = Instantiate(bulletPrefab);
		}

		missiles = new Missile[missileCount];
		for (int i = 0; i < missiles.Length; i++)
		{
			missiles[i] = Instantiate(missilePrefab);
		}
	}

	void Start()
    {
        missileEvent.Invoke(missileStock.ToString());
        multishotEvent.Invoke(Mathf.Max(0, Mathf.Ceil(multishotTimer)).ToString());
    }

	void Update()
	{
		ManageShootActions();
		ManageReleaseShootActions();
	}

	private void ManageShootActions()
	{
		if (shootBulletAction.IsPressed() && bulletFireTimer <= 0.0f && shootAction != ShootAction.Missile)
		{
			shootAction = ShootAction.Bullet;
			Bullet bullet = (Bullet) FindAvailableProjectile(bullets);
			if (bullet != null)
			{
				ShootProjectile(bullet, transform.forward);
				bulletFireTimer = bulletFireRate;

				if (multishotTimer > 0.0f) Multishot();
			}
		}

		if (shootMissileAction.IsPressed() && missileStock > 0 && missileFireTimer <= 0.0f && shootAction != ShootAction.Bullet)
		{
			shootAction = ShootAction.Missile;

			Missile missile = (Missile) FindAvailableProjectile(missiles);
			if (missile != null)
			{
				ShootProjectile(missile, transform.forward);
				missileFireTimer = missileFireRate;
				missileStock--;
			}
		}

		if (bulletFireTimer > 0.0f) bulletFireTimer -= Time.deltaTime;
		if (missileFireTimer > 0.0f) missileFireTimer -= Time.deltaTime;
		if (multishotTimer > 0.0f) multishotTimer -= Time.deltaTime;
	}

	private void ManageReleaseShootActions()
	{
		switch (shootAction)
		{
			case ShootAction.Bullet:
				if (shootBulletAction.WasReleasedThisFrame())
				{
					ResetShootAction();
				}
				break;
			case ShootAction.Missile:
				if (shootMissileAction.WasReleasedThisFrame())
				{
					missileFireTimer = 0.0f;
					ResetShootAction();
				}
				break;
			default:
				break;
		}
	}

	private Projectile FindAvailableProjectile(Projectile[] projectiles)
	{
		foreach (Projectile projectile in projectiles)
		{
			if (!projectile.gameObject.activeSelf) return projectile;
		}

		return null;
	}

	private void ShootProjectile(Projectile projectile, Vector3 direction)
	{
		Transform player = transform.root;
		/*  Documentation consult�e pour convertir la position relative du blaster en position absolue:
			https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Transform.TransformPoint.html */
		Vector3 spawnPos = player.TransformPoint(relativeBlasterPosition);
		projectile.Shoot(spawnPos, direction);
	}

	private void Multishot()
	{
		foreach (float angle in MULTISHOT_ANGLES)
		{
			Bullet bullet = (Bullet)FindAvailableProjectile(bullets);
			if (bullet == null) return;

			/*	Documentation consult�e pour appliquer un angle de rotation sur un axe:
				https://docs.unity3d.com/6000.0/Documentation/Manual/class-Quaternion.html
				https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Quaternion.AngleAxis.html */
			Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * transform.forward;
			ShootProjectile(bullet, direction);
		}
	}

	private void ResetShootAction()
	{
		shootAction = ShootAction.Idle;
	}
}
