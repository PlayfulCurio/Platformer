using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleEntity : MonoBehaviour
{
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected SpriteRenderer _flickerSpriteRenderer;
    [SerializeField] private Animator _animator;

    protected static float _maxPossibleHealth = 1000f;
    private float _flickerTime = .6f, _flickerInterval = .15f;

    private GameplayManager _gameplayManager;
    protected bool _isDead;
    protected bool _gameOver;
    private Coroutine _flickerCoroutine;
    private WaitForSeconds _flickerWait;

    public event Action OnDeath;
    public virtual event Action<float, float> OnHealthChanged;

    protected virtual void Awake()
    {
        _gameplayManager = GameplayManager.Instance;
        _gameplayManager.OnGameOver += SetGameOver;
        _flickerWait = new WaitForSeconds(_flickerInterval);
    }

    private void OnDestroy()
    {
        if (_gameplayManager != null)
        _gameplayManager.OnGameOver -= SetGameOver;
    }

    public virtual void TakeDamage(float amount)
    {
        if (!_isDead && !_gameOver)
        {
            if (_flickerCoroutine != null)
                StopCoroutine(_flickerCoroutine);
            if ((_currentHealth -= amount) > 0)
            {
                _flickerCoroutine = StartCoroutine(Flicker());
            }
            else
            {
                _currentHealth = 0f;
                _isDead = true;
                _flickerSpriteRenderer.color = Color.clear;
                _animator.SetTrigger("Explode");
                OnDeath?.Invoke();
            }
            OnHealthChanged?.Invoke(-amount, _currentHealth / _maxPossibleHealth);
        }
    }

    private IEnumerator Flicker()
    {
        int ticks = (int)(_flickerTime / _flickerInterval);
        for (int i = 0; i < ticks; i++)
        {
            _flickerSpriteRenderer.color = (i % 2 == 1 ? Color.clear : Color.white);
            yield return _flickerWait;
        }
        _flickerSpriteRenderer.color = Color.clear;
    }

    protected void CallOnHealthChanged(float changeDelta, float normalizedHealth) => OnHealthChanged?.Invoke(changeDelta, normalizedHealth);

    protected virtual void SetGameOver(bool didPlayerWin) => _gameOver = true;

    private void FinishExploding() => Destroy(gameObject);
}
