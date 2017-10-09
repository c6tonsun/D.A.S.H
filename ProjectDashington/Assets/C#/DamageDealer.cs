using UnityEngine;

public class DamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField]
    private int _damage;

    private void DealDamage(Collider2D other)
    {
        // If other vollider is trigger do nothing.
        if (other.isTrigger)
        {
            return;
        }

        // Try to find others health component.
        Health health = other.gameObject.GetComponent<Health>();

        // If other has health decrease it.
        if (health != null)
        {
            health.DecreaseHealth(GetDamage());
            health.SetKiller(this.gameObject);
        }
    }

    // Getters and setters

    public int GetDamage()
    {
        return _damage;
    }

    // Collider methods.

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        DealDamage(other.collider);
    }
}
