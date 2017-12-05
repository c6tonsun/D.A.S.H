using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

    // Use this for initialization
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();

        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets current animation (idle, swing, dash, death).
        _anim.SetBool("isDashing", _playerMovement.GetIsDashing());

        // Flips
		if (_rb.velocity.x == 0) 
		{
			// do not flip
		} 
		else if (_rb.velocity.x < 0)
        {
            _spriteRenderer.flipY = true;
        }
        else
        {
            _spriteRenderer.flipY = false;
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

