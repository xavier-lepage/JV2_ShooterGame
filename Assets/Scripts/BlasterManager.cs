using UnityEngine;
using UnityEngine.InputSystem;

public class BlasterManager : MonoBehaviour
{
    [SerializeField] private int bulletCount;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private int missileCount;
    [SerializeField] private Missile missilePrefab;
    [SerializeField] private Missile[] missiles;
    [SerializeField] private float bulletFireRate;
    [SerializeField] private float missileFireRate;
    private Vector3 relativeBlasterPosition = new Vector3(1.4f, 2.6f, 4f);
    private float bulletFireTimer;
    private float missileFireTimer;
    private ShootAction shootAction;

    private InputAction shootBulletAction;
    private InputAction shootMissileAction;

    void Start()
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
                ShootProjectile(bullet);
                bulletFireTimer = bulletFireRate;
            }
        }

        if (shootMissileAction.IsPressed() && missileFireTimer <= 0.0f && shootAction != ShootAction.Bullet)
        {
            shootAction = ShootAction.Missile;

            Missile missile = (Missile) FindAvailableProjectile(missiles);
            if (missile != null)
            {
                ShootProjectile(missile);
                missileFireTimer = missileFireRate;
            }
        }

        if (bulletFireTimer > 0.0f) bulletFireTimer -= Time.deltaTime;
        if (missileFireTimer > 0.0f) missileFireTimer -= Time.deltaTime;
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

    private void ShootProjectile(Projectile projectile)
    {
        Transform player = transform.root;
        /*  Documentation consultée pour convertir la position relative du blaster en position absolue:
            https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Transform.TransformPoint.html */
        Vector3 spawnPos = player.TransformPoint(relativeBlasterPosition);
        projectile.Shoot(spawnPos, player.forward, player.rotation);
    }

    private void ResetShootAction()
    {
        shootAction = ShootAction.Idle;
    }
}
