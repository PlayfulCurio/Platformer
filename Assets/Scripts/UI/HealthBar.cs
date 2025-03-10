using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private float _maxWidth;
    private GameplayManager _gameplayManager;

    private void Awake()
    {
        _maxWidth = _rectTransform.sizeDelta.x;
        _gameplayManager = GameplayManager.Instance;
        _gameplayManager.OnPlayerHealthChanged += AdjustWidth;
    }

    private void AdjustWidth(float normalizedHealth)
    {
        _rectTransform.sizeDelta = new Vector2(normalizedHealth * _maxWidth, _rectTransform.sizeDelta.y);
    }
}
