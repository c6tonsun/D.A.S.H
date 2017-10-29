﻿using UnityEngine;

public class DamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField]
    private int _damage;

    private void DealDamage(Collider2D other)
    {
        // If other collider is trigger do nothing.
        if (other.isTrigger && other.gameObject.tag != "Lava")
        {
            return;
        }

        // Try to find others health component.
        Health health = other.gameObject.GetComponent<Health>();

        // If other has health decrease it.
        if (health != null)
        {
            health.SetKiller(this.gameObject);
            health.DecreaseHealth(GetDamage());
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

    private void OnTriggerExit2D(Collider2D other)
    {
        DealDamage(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DealDamage(other.collider);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        DealDamage(other.collider);
    }
}
