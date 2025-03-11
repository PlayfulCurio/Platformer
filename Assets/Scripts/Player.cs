using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _invincibilityTime;
    [SerializeField] private DestructibleEntity _destructibleEntity;
    [SerializeField] private Collider2D[] _colliders;

    private WaitForSeconds _invincibilityWait;

    private void Awake()
    {
        _invincibilityWait = new WaitForSeconds(_invincibilityTime);
        _destructibleEntity.OnDeath += FailLevel;
        _destructibleEntity.OnHealthChanged += ReactOnDamaged;
    }

    private void FailLevel()
    {
        GameplayManager.Instance.EndGame(false);
    }

    private void ReactOnDamaged(float changeDelta, float normalizedHealth)
    {
        GameplayManager.Instance.ChangePlayerHealth(changeDelta, normalizedHealth);
        StartCoroutine(BecomeInvincible());
    }

    private IEnumerator BecomeInvincible()
    {
        foreach (var collider in _colliders)
            collider.enabled = false;
        yield return _invincibilityWait;
        foreach (var collider in _colliders)
            collider.enabled = true;
    }
}
