using UnityEngine;

public class Health : MonoBehaviour, IHealth {

    [SerializeField]
    private int _initialHealth;
    [SerializeField]
    private int _minHealth;
    
    private int _currentHealth;

    private GameObject _killer;
    private UIManager _UIManager;
    private Rigidbody2D _rb;
    private SoundPlayer _soundPlayer;
    private Collider2D[] _colliders;

    private void Awake()
    {
        _UIManager = FindObjectOfType<UIManager>();
        _rb = GetComponent<Rigidbody2D>();
        _soundPlayer = GetComponent<SoundPlayer>();
        _colliders = GetComponents<Collider2D>();
    }

    private void OnEnable()
    {
        _currentHealth = _initialHealth;
        if (_rb != null)
        {
            _rb.velocity = Vector2.zero;
        }

        foreach (Collider2D collider in _colliders)
        {
            collider.enabled = true;
        }

        if (transform.childCount > 0 &&
            transform.GetChild(0).tag == GameManager.SHIELD_TAG)
        {
            Collider2D[] colliders = transform.GetChild(0).GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }

    private void OnDisable()
    {
        foreach (Collider2D collider in _colliders)
        {
            collider.enabled = false;
        }

        if (transform.childCount > 0 &&
            transform.GetChild(0).tag == GameManager.SHIELD_TAG)
        {
            Collider2D[] colliders = transform.GetChild(0).GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Enemy" && GetIsDead())
        {
            _UIManager.DecreaseEnemyCount();
            _UIManager.ShakeCamera();

            RangeSingle rangeSingle = GetComponent<RangeSingle>();
            if (rangeSingle != null)
            {
                rangeSingle.enabled = false;
            }

            RangeAOE rangeAOE = GetComponent<RangeAOE>();
            if (rangeAOE != null)
            {
                rangeAOE.enabled = false;
            }

            if (transform.childCount > 0 &&
                transform.GetChild(0).tag == GameManager.SHIELD_TAG)
            {
                transform.GetChild(0).GetComponent<Health>().enabled = false;
            }

            // disable this health component
            enabled = false;
        }
        else if (gameObject.tag == "Shield" && GetIsDead())
        {
            transform.parent.GetComponent<Health>().Awake();
            gameObject.SetActive(false);
        }
        else if (gameObject.tag == "Player" && GetIsDead())
        {
            _UIManager.PlayerLost(_killer);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().SetBool("die", true);
            Handheld.Vibrate();
            _soundPlayer.PlayDeathSound();

            // disable this health component
            enabled = false;
        }

        if (GetIsDead() && _killer.tag == "Void")
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsEnemy()
    {
        if (_killer.tag == "Lava" && gameObject.tag == "Enemy")
        {
            return true;
        }
        return false;
    }

    // Decreases health by amount.
    public void DecreaseHealth(int amount)
    {
        if (IsEnemy())
        {
            return;
        }

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
}
