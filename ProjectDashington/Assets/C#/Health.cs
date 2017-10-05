using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth {

    [SerializeField]
    private int _initialHealth;
    [SerializeField]
    private int _minHealth;
    
    private int _currentHealth;

    // Does initialisations.
    private void Awake()
    {
        _currentHealth = _initialHealth;
    }

    private void FixedUpdate()
    {
        if (GetIsDead())
        {
            Destroy(this.gameObject);
        }
    }

    // Decreases health by amount.
    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth < _minHealth)
        {
            _currentHealth = _minHealth;
        }
    }

    // Returns dead state.
    public bool GetIsDead()
    {
        return _currentHealth == _minHealth;
    }
}
