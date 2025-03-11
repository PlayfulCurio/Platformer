using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private float _nextPositionX;
    private float _maxPositionX;

    private float _scrollSpeed = 25f;

    private void Awake()
    {
        _maxPositionX = _rectTransform.sizeDelta.x / 2f - Screen.width / 2f;
    }

    private void Update()
    {
        if (_maxPositionX > 0)
        {
            _nextPositionX = _rectTransform.anchoredPosition.x + _scrollSpeed * Time.deltaTime;
            if (_nextPositionX >= _maxPositionX)
            {
                _nextPositionX = _maxPositionX;
                _maxPositionX = -_nextPositionX;
            }
        }
        else
        {
            _nextPositionX = _rectTransform.anchoredPosition.x - _scrollSpeed * Time.deltaTime;
            if (_nextPositionX <= _maxPositionX)
            {
                _nextPositionX = _maxPositionX;
                _maxPositionX = -_nextPositionX;
            }
        }
        _rectTransform.anchoredPosition = new Vector2(_nextPositionX, _rectTransform.anchoredPosition.y);
    }
}
