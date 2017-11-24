using UnityEngine;

public class MeleeAnimation : MonoBehaviour {
    
	private Animator _anim;

    private Collider2D _attackCollider;

    private bool _isHitting = false;
    [SerializeField, Tooltip("Time till first hit.")]
    private float _timeToFirstHit;
	private float _attackCooldown;
    private float _attackTime = 1.6f;

    // Use this for initialization
    private void Start()
    {
        _anim = GetComponent<Animator>();

        ResetAnimation();

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

    public void ResetAnimation()
    {
        _attackCooldown = _timeToFirstHit;
        GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load("MeleeImp") as RuntimeAnimatorController;
    }
}
