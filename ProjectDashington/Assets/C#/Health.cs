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
    private WorldManager _worldManager;
    
    private void Awake()
    {
        _worldManager = GameObject.Find("World manager").GetComponent<WorldManager>();
    }

    private void OnEnable()
    {
        _currentHealth = _initialHealth;
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Enemy" && GetIsDead())
        {
            _worldManager.DecreaseEnemyCount();
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Shield" && GetIsDead())
        {
            transform.parent.GetComponent<Health>().Awake();
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Player" && GetIsDead())
        {
            _worldManager.SetPlayerKiller(_killer);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.SetActive(false);
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
