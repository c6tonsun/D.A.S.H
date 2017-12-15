using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

	private bool _left;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();

        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
	{
		_left = false;
        _anim.runtimeAnimatorController = 
            Resources.Load("Player") as RuntimeAnimatorController;
		_anim.Play("explosion", -1, 0f);
		_anim.SetBool("die", false);
        
    }

    void Update()
    {
        //Sets current animation (idle, swing, dash, death).
        _anim.SetBool("isDashing", _playerMovement.GetIsDashing());
        _anim.SetBool("isPushed", _playerMovement.GetIsPushed());
        _anim.SetBool("swing", _playerMovement.GetIsSwinging());

        // Flips
		if (_rb.velocity.x == 0) 
		{
			if (_left)
			{
				_spriteRenderer.flipX = true;
			} 
			else
			{
				_spriteRenderer.flipX = false;
			}
			_spriteRenderer.flipY = false;
		} 
		else if (_rb.velocity.x < 0)
        {
            _spriteRenderer.flipX = false;
            _spriteRenderer.flipY = true;
			_left = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            _spriteRenderer.flipY = false;
			_left = false;
        }

		if (_playerMovement.GetIsPushed ()) {
			if (_spriteRenderer.flipY) {
				_spriteRenderer.flipY = false;
			} else {
				_spriteRenderer.flipY = true;
			}
		}
    }
}

