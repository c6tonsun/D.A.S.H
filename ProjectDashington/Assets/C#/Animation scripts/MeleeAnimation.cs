using UnityEngine;

public class MeleeAnimation : MonoBehaviour {
    
	private Animator _anim;

    private Collider2D _attackCollider;

    public bool isImp;

    private bool _isHitting = false;
    [SerializeField, Tooltip("Time till first hit.")]
    private float _timeToFirstHit;
	private float _attackCooldown;
    private float _attackTime = 1.6f;

    private void OnEnable()
    {
        if (isImp)
        {
            GetComponent<Animator>().runtimeAnimatorController =
                Resources.Load("MeleeImp") as RuntimeAnimatorController;
            GetComponent<Animator>().Play("Imp_idle_final", -1, 0f);
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController =
                Resources.Load("MeleeSnake") as RuntimeAnimatorController;
            GetComponent<Animator>().Play("Snake_idle", -1, 0f);
        }

        _attackCooldown = _timeToFirstHit;
    }

    // Use this for initialization
    private void Start()
    {
        _anim = GetComponent<Animator>();

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                _attackCollider = collider;
            }
        }
    }

    // Update is called once per frame
    private void Update ()
    {
		_anim.SetBool("isHitting", _isHitting);
        _attackCollider.enabled = _isHitting;

        _attackCooldown -= Time.deltaTime;

        // attack is done
        if (_attackCooldown < _attackTime)
        {
            _isHitting = false;
        }

        // start new attack
        if (_attackCooldown < 0f)
        {
			_attackCooldown = 2f;
			_isHitting = true;
		}
	}
}
