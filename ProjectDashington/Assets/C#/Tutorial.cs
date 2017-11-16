using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Color _color;

    [SerializeField]
    private float _alphaSpeed;
    private float _alphaValue;

    public float factor;

    private PlayerMovement _playerMovement;
    private bool _firstTouch;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnEnable()
    {
        _color = _spriteRenderer.color;
        _color.a = 0f;
        _spriteRenderer.color = _color;

        _firstTouch = true;
    }

    private void Update()
    {
        if (_firstTouch && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TutorialRoutine());
            _firstTouch = false;
        }
        else if (_playerMovement.GetIsDashing())
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

            _color.a = Mathf.Abs(Mathf.Sin(_alphaValue)) * factor;

            _alphaValue += _alphaSpeed * Time.deltaTime;

            _spriteRenderer.color = _color;

            yield return null;
        }
    }
}
