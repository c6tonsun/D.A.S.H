using UnityEngine;

public class Tutorial : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Color _color;

    [SerializeField]
    private float _alphaSpeed = 1.3f;
    private float _alphaValue;

    public float factor = 0.77f;

    private PlayerMovement _playerMovement;
    private bool _tutorialRunning;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _color = _spriteRenderer.color;
        _color.a = 0f;
        _spriteRenderer.color = _color;

        _tutorialRunning = false;

        _alphaValue = 0f;
    }

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (!_tutorialRunning && Input.GetMouseButtonDown(0))
        {
            _tutorialRunning = true;
        }

        if (_playerMovement.GetIsDashing())
        {
            gameObject.SetActive(false);
            _tutorialRunning = false;
        }

        if (_tutorialRunning)
        {
            _color = _spriteRenderer.color;

            _color.a = Mathf.Abs(Mathf.Sin(_alphaValue)) * factor;

            _alphaValue += _alphaSpeed * Time.deltaTime;

            _spriteRenderer.color = _color;
        }
    }
}
