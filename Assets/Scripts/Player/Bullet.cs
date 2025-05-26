using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public Vector3 velocity;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
