using System.Collections;
using UnityEngine;

public class StickmanEnemy : MonoBehaviour,IDamageable
{
    [Header("Visuals")]
    [SerializeField] private Animator enemyAnimator;

    [Header("GUN")]
    [SerializeField] private Gun gun;

    [Header("Enemy Settings")]
    [SerializeField] private int health = 500;
    [SerializeField] private float shootInterval = 0.5f;
    [SerializeField] private float shootPauseDuration = 2f;

    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    [NaughtyAttributes.Button]
    public void InitiateShoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            shootingCoroutine = StartCoroutine(ShootingRoutine());
        }
    }

    private IEnumerator ShootingRoutine()
    {
        while (isShooting)
        {
            // Trigger shoot animation if you want to sync with visuals
            enemyAnimator.SetTrigger("Shoot");

            // Fire the gun
            gun.Shoot();

            // Wait between shots
            yield return new WaitForSeconds(shootInterval);

            // Pause shooting
            yield return new WaitForSeconds(shootPauseDuration);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0) return;

        health -= damage;
        if (health <= 0)
        {
            Died();
        }
    }

    private void Died()
    {
        Debug.Log("Enemy Died");
        isShooting = false;

        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }

        // Optional: trigger death animation
        enemyAnimator.SetTrigger("Die");

        // Disable further functionality, optionally destroy or deactivate object
        gun.enabled = false;
        this.enabled = false;
    }

    public void SetOutlineStatus(bool show)
    {
        
    }
}
