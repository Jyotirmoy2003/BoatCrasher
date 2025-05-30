using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float rateOfFire = 5f; // bullets per second
    public Transform firePoint;  // where bullets spawn
    public GameObject bulletPrefab;
    public float bulletVelocity = 20f;

    private bool isShooting = false;
   




    public virtual void Shoot()
    {
        if (!isShooting)
            StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine()
    {
        isShooting = true;
        float delay = 1f / rateOfFire;

        while (isShooting)
        {
            FireBullet();
            yield return new WaitForSeconds(delay);
        }
    }

    protected virtual void FireBullet()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.velocity = firePoint.forward * bulletVelocity;
        }
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    public void ListenToFireStatusChanged(Component sender, object data)
    {
        if ((bool)data)
        {
            Shoot();
        }
        else
        {
            StopShooting();
        }
    }
    






    
}
