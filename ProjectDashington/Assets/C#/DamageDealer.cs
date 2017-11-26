using UnityEngine;

public class DamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField]
    private int _damage;

    private PlayerMovement _playerMovement;
    private bool _canDoDamage;
    private bool _isPlayer;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        if (_playerMovement == null)
        {
            _canDoDamage = true;
            _isPlayer = false;
        }
        else
        {
            _isPlayer = true;
        }
    }
    
    private void Update()
    {
        if (_isPlayer)
        {
            _canDoDamage = _playerMovement.GetIsDashing();
        }
    }
    
    private void DealDamage(Collider2D other)
    {
        if (!_canDoDamage)
        {
            return;
        }
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
