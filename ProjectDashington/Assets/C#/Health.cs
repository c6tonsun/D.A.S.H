using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth {

    [SerializeField]
    private int _initialHealth;
    [SerializeField]
    private int _minHealth;
    
    private int _currentHealth;

    private GameObject _killer;
    private LevelHandler _levelHandler;

    // Does initialisations.
    private void Awake()
    {
        _currentHealth = _initialHealth;
        _levelHandler = GameObject.Find("Level handler").GetComponent<LevelHandler>();
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Enemy" && GetIsDead())
        {
            _levelHandler.DecreaseEnemyCount(1);
            _levelHandler.UpdateUI();
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

    // Getters and setters.

    public bool GetIsDead()
    {
        return _currentHealth == _minHealth;
    }
    
    public void SetKiller(GameObject killer)
    {
        _killer = killer;
    }

    public GameObject GetKiller()
    {
        return _killer;
    }
}
