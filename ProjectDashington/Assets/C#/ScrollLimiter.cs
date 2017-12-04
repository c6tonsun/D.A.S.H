using UnityEngine;
using UnityEngine.UI;

public class ScrollLimiter : MonoBehaviour {

    private RectTransform _rectTransform;
    private ScrollRect _scrollRect;
    private float _minY;
    private float _maxY;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _scrollRect = GetComponent<ScrollRect>();
        _minY = -172;
        _maxY = _minY + _rectTransform.sizeDelta.y / 2;
    }

    private void LateUpdate()
    {
        Vector2 newPosition = _rectTransform.anchoredPosition;

        if (newPosition.y < _minY)
        {
            newPosition.y = _minY;
            _scrollRect.StopMovement();
        }
        else if (newPosition.y > _maxY)
        {
            newPosition.y = _maxY;
            _scrollRect.StopMovement();
        }

        _rectTransform.anchoredPosition = newPosition;
    }
}
