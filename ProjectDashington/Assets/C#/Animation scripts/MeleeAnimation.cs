using UnityEngine;

public class MeleeAnimation : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rb;
    private Animator _anim;
	private bool _isHitting = false;
	float _attackTimer = 0f;

    // Use this for initialization
    void Start()
    {
		_rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
		_anim.SetBool("isHitting", _isHitting);
		if (_attackTimer <= 0f) {
			_attackTimer = 2f;
			_isHitting = false;
		}
		if (_attackTimer > 0f) {
			_attackTimer -= Time.deltaTime;
				if(_attackTimer <= 0f){
					Attack();
				}
		}
			
	}
	void Attack() {
		_attackTimer = 0f;
		_isHitting = true;
	}

}
