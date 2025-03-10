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

    protected bool _isDead;
    private Coroutine _flickerCoroutine;
    private WaitForSeconds _flickerWait;

    public event Action OnDeath;
    public virtual event Action<float> OnHealthChanged;

    protected virtual void Awake()
    {
        _flickerWait = new WaitForSeconds(_flickerInterval);
    }

    public virtual void TakeDamage(float amount)
    {
        if (!_isDead)
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
            OnHealthChanged?.Invoke(_currentHealth / _maxPossibleHealth);
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

    private void FinishExploding() => Destroy(gameObject);
}
