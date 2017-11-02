using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Color _color;

    [SerializeField]
    private float _alphaSpeed;
    private float _minAlpha = 0;
    private float _maxAlpha = 0.8f;
    private bool _grow;

    private PlayerMovement _playerMovement;
    private bool _firstTouch = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (_firstTouch && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TutorialRoutine());
            _firstTouch = false;
        }

        if (_playerMovement.GetIsDashing())
        {
            StopCoroutine(TutorialRoutine());
            _spriteRenderer.enabled = false;
        }
    }

    IEnumerator TutorialRoutine()
    {
        while (true)
        {
            _color = _spriteRenderer.color;

            if (_color.a <= _minAlpha)
            {
                _grow = true;
            }
            else if (_color.a >= _maxAlpha)
            {
                _grow = false;
            }

            if (_grow)
            {
                _color.a += _alphaSpeed * Time.deltaTime;
            }
            else
            {
                _color.a -= _alphaSpeed * Time.deltaTime;
            }

            _spriteRenderer.color = _color;

            yield return null;
        }
    }
}
