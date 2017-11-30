using UnityEngine;

public class JumpMovement : MonoBehaviour {

    private Rigidbody2D _rb;
    
    // Set this set to public to check animation and movement sync.
    private float animationLength = 0.833f;
    private float startJump = 0.32f;
    private float endJump = 0.66f;
    private float _jumpTimer;

    public float jumpSpeed;

    public Transform[] jumpPath;
    private int _jumpPathIndex;
    private Vector3 _targetPosition;
    private Vector3 _targetDirection;

    public bool loopPath;
    private bool _growIndex = true;

    public int maxJumps;
    private int _jumpCount;

    public float idleTime;
    private float _idleTimer;
    private bool _isIdle = false;

    private void OnEnable()
    {
        ResetAnimation();

        _jumpPathIndex = 0;
        _growIndex = true;
        SetNextTarget();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isIdle)
        {
            _idleTimer -= Time.fixedDeltaTime;

            if (_idleTimer < 0)
            {
                StartJumping();
            }
        }
        else
        {
            // run timer
            _jumpTimer += Time.fixedDeltaTime;
        }

        // reset
        if (_jumpTimer > animationLength)
        {
            ResetAnimation();
            _jumpTimer = 0;
        }

        // jump or stop
        if (_jumpTimer > startJump && _jumpTimer < endJump)
        {
            Jump();
        }
        else
        {
            Stop();
        }
    }

    private void ResetAnimation()
    {
        GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load("Wrestler") as RuntimeAnimatorController;
        GetComponent<Animator>().Play("Wrestler_jump", -1, 0f);
    }

    private void Jump()
    {
        if (_rb.velocity.magnitude == 0)
        {
            _jumpCount++;

            if (_jumpCount >= maxJumps)
            {
                _jumpCount = 0;
                StopAndIdle();
                return;
            }

            _rb.AddForce(_targetDirection * jumpSpeed, ForceMode2D.Impulse);

            if (_targetDirection.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        CheckTargetReached();
    }

    private void Stop()
    {
        _rb.velocity = Vector2.zero;
    }

    private void CheckTargetReached()
    {
        float currentToTarget = Vector3.Distance(_targetPosition, transform.position);

        float currentToNext = (_targetDirection * jumpSpeed * Time.fixedDeltaTime).magnitude;

        if (currentToTarget < currentToNext)
        {
            Stop();
            SetNextTarget();
        }
    }

    private void SetNextTarget()
    {
        _targetPosition = jumpPath[_jumpPathIndex].position;
        _targetDirection = _targetPosition - transform.position;
        _targetDirection.Normalize();

        // If path loops
        if (loopPath)
        {
            _jumpPathIndex++;

            if (_jumpPathIndex == jumpPath.Length)
            {
                _jumpPathIndex = 0;
            }
        }
        // If path goes back and forth.
        else
        {
            // Goes forth
            if (_growIndex)
            {
                _jumpPathIndex++;

                if (_jumpPathIndex == jumpPath.Length - 1)
                {
                    _growIndex = false;
                }
            }
            // Goes back.
            else
            {
                _jumpPathIndex--;

                if (_jumpPathIndex == 0)
                {
                    _growIndex = true;
                }
            }
        }
    }

    // public methods

    public void StopAndIdle()
    {
        Stop();
        GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load("Wrestler") as RuntimeAnimatorController;
        GetComponent<Animator>().Play("Wrestler_jump", -1, 0f);

        _isIdle = true;
        _idleTimer = idleTime;

        _jumpTimer = 0;
    }

    public void StartJumping()
    {
        ResetAnimation();
        _isIdle = false;
    }
}
