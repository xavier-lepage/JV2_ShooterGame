using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BlasterManager : MonoBehaviour
{
    [SerializeField] private int bulletCount;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private float bulletFireRate;
    [SerializeField] private UnityEvent<string> missileEvent;
    [SerializeField] private UnityEvent<string> multishotEvent;
    private Vector3 relativeBlasterPosition = new Vector3(1.4f, 2.6f, 4f);
    private float bulletFireTimer;

    private InputAction shootBulletAction;

    void Start()
    {
        shootBulletAction = InputSystem.actions.FindAction("ShootBullet");

        bullets = new Bullet[bulletCount];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bulletPrefab);
        }
    }

    void Update()
    {
        ManageShootActions();
    }

    private void ManageShootActions()
    {
        if (shootBulletAction.IsPressed() && bulletFireTimer <= 0.0f)
        {
            Bullet bullet = FindAvailableBullet();
            if (bullet != null)
            {
                Transform player = transform.root;
                /*  Documentation consultée pour convertir la position relative du blaster en position absolue:
                    https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Transform.TransformPoint.html */
                Vector3 spawnPos = player.TransformPoint(relativeBlasterPosition);
                bullet.Shoot(spawnPos, player.forward);

                bulletFireTimer = bulletFireRate;
            }
        }

        if (bulletFireTimer > 0.0f) bulletFireTimer -= Time.deltaTime;
    }

    private Bullet FindAvailableBullet()
    {
        foreach (Bullet bullet in bullets)
        {
            if (!bullet.gameObject.activeSelf) return bullet;
        }

        return null;
    }
}
