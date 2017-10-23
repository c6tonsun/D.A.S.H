using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimaton : MonoBehaviour {

	private PlayerMovement _playerMovement;
	private Vector3 newRotation = Vector3.zero;
	private bool _isAngleCalculated = false;
	private SpriteRenderer _spriteRenderer;

	[SerializeField]
	private Animator anim;

	// Use this for initialization
	void Start () {
		_playerMovement = GetComponent<PlayerMovement> ();
		anim = GetComponent<Animator> ();
		_spriteRenderer = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		//Sets current animation (idle, swing, dash, death).
		anim.SetBool ("isDashing", _playerMovement.GetIsDashing());

		//Sets dashing angle and direction.

		if (_playerMovement.GetIsDashing () && !_isAngleCalculated) {
			
			newRotation.z = CalculateAngle();
		} else if (!_playerMovement.GetIsDashing()) {
			_isAngleCalculated = false;
		} 
		transform.eulerAngles = newRotation;
	}

	private float CalculateAngle() {
		float z = Vector3.Angle (transform.position, transform.position + _playerMovement.GetTargetDirection ());
		if(_playerMovement.GetTargetDirection().y < 0) {
			z = z * -1;
			_spriteRenderer.flipX = false;
		}
		if(_playerMovement.GetTargetDirection().x < 0) {
			z = z * -1;
			_spriteRenderer.flipX = true;

		}
		_isAngleCalculated = true;
		Debug.Log (z);
		return z;
	}
}
