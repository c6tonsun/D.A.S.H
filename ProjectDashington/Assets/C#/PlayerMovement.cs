using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

    [SerializeField, Range(0, 50)]
    private float _movementSpeed;
    
    private Rigidbody2D _rb;
    
    private Vector3 _targetPosition;
    private Vector3 _targetDirection;

    private bool _startDash = false;
    private bool _isDashing = false;

    const int ENEMY_LAYER = 10;
    const int PLAYER_LAYER = 9;
    const int WALL_LAYER = 8;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetMouseButtonDown(0) && !_isDashing)
        {
            SetNewTarget();
        }
	}
    
    void FixedUpdate()
    {
        if (_startDash)
        {
            Dash();
            _startDash = false;
            _isDashing = true;
        }

        if (_isDashing)
        {
            StopMovementCheck();
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
            _targetPosition = hit.point;
            _targetDirection = new Vector3(hit.point.x, hit.point.y) - transform.position;
            _targetDirection.Normalize();
            _startDash = true;
        }
    }

    // Starts dash towards target position.
    private void Dash()
    {
        _rb.AddForce(_targetDirection * _movementSpeed, ForceMode2D.Impulse);
    }

    // Checks if we reached target.
    private void StopMovementCheck()
    {
        // Where we are on this fixed frame
        Vector3 currentPosition = transform.position;
        float currentDistance = Vector3.Distance(currentPosition, _targetPosition);
        
        // Where we could be on the next fixed update
        Vector3 nextPosition = currentPosition + 
            _targetDirection * _movementSpeed * Time.fixedDeltaTime;
        float nextDistance = Vector3.Distance(nextPosition, _targetPosition);

        if (currentDistance < nextDistance)
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
        _rb.velocity = Vector3.zero;
        _isDashing = false;
    }

    // Getters and setters.

    // Collider methods.
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == ENEMY_LAYER)
        {
            Destroy(other.gameObject);
        }
    }
    /* Collision methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == WALL_LAYER)
        {
            ResetMovement();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == WALL_LAYER)
        {
            // ResetMovement();
        }
    }
    */
}
