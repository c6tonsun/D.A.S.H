using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

    [SerializeField, Range(0, 50)]
    private float _movementSpeed;
    
    private Rigidbody2D _rb;
    
    private Vector3 _targetPosition;
    private Vector3 _targetDirection;

    private Vector3 _pushPosition;
    private float _pushDistance;

    private bool _startDash = false;
    private bool _isDashing = false;
    private bool _isPushed = false;

    private LevelHandler _levelHandler;

    const int TRAP_LAYER = 11;
    const int ENEMY_LAYER = 10;
    const int PLAYER_LAYER = 9;
    const int WALL_LAYER = 8;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _levelHandler = GameObject.Find("Level handler").GetComponent<LevelHandler>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(!_isDashing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetNewTarget();
            }
        }
	}
    
    void FixedUpdate()
    {
        if (_startDash)
        {
            Dash();
        }
        
        if (_isDashing)
        {
            if (_isPushed)
            {
                PushedDistanceCheck();
            }
            else
            {
                StopMovementCheck();
            }
        }


        if(_rb.velocity.magnitude < 0.3f)
        {
            ResetMovement();
        }
    }

    // Sets new target position using raycast.
    private void SetNewTarget()
    {
        // Result of raycast that hits 2D collider behind mouse.
        RaycastHit2D hit = Physics2D.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition),
            Camera.main.transform.forward);

        // If our raycast hit enemy set new target.
        if (hit.collider != null && hit.collider.gameObject.layer == ENEMY_LAYER)
        {
            _targetPosition = hit.collider.transform.position;
            _targetDirection = _targetPosition - transform.position;
            _targetDirection.Normalize();
            _startDash = true;

            StopTarget(hit.collider.gameObject);
        }
    }

    private void StopTarget(GameObject target)
    {
        if (target.GetComponent<Rigidbody2D>() != null)
        {
            target.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    // Starts dash towards target position.
    private void Dash()
    {
        _rb.AddForce(_targetDirection * _movementSpeed, ForceMode2D.Impulse);
        
        _startDash = false;
        _isDashing = true;
        _isPushed = false;

        _levelHandler.IncreaseDashCount(1);
        _levelHandler.UpdateUI();
    }

    public void Pushed(Vector3 pushDirection, float pushDistance, float pushForce)
    {
        ResetMovement();

        _pushPosition = transform.position;
        _pushDistance = pushDistance;
        
        _rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);

        _startDash = false;
        _isDashing = true;
        _isPushed = true;
    }

    private void PushedDistanceCheck()
    {
        if (Vector3.Distance(_pushPosition, transform.position) > _pushDistance)
        {
            ResetMovement();
        }
    }

    // Checks if we reached target.
    private void StopMovementCheck()
    {
        // Where we are on this fixed frame
        Vector3 currentPosition = transform.position;
        float currentToTarget = Vector3.Distance(currentPosition, _targetPosition);

        // Where we could be on the next fixed update
        Vector3 nextPosition = currentPosition +
            _targetDirection * _movementSpeed * Time.fixedDeltaTime;
        float currentToNext = Vector3.Distance(currentPosition, nextPosition);

        if (currentToTarget < currentToNext)
        {
            transform.position = _targetPosition;
            ResetMovement();
        }
    }
    
    // Stops movement
    private void ResetMovement()
    {
        _targetPosition = Vector3.zero;
        _targetDirection = Vector3.zero;

        _pushDistance = 0;
        _pushPosition = Vector3.zero;

        _rb.velocity = Vector3.zero;
        _startDash = false;
        _isDashing = false;
        _isPushed = false;
    }
}
