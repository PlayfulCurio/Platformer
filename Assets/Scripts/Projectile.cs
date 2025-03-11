using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody2D _rigidbody;

    private GameplayManager _gameplayManager;
    private bool _gameOver;

    private void Awake()
    {
        _gameplayManager = GameplayManager.Instance;
        _gameplayManager.OnGameOver += SetGameOver;
    }

    private void OnDestroy()
    {
        if (_gameplayManager != null)
            _gameplayManager.OnGameOver -= SetGameOver;
    }

    private void FixedUpdate()
    {
        if (!_gameOver)
            _rigidbody.MovePosition(_rigidbody.position + (Vector2)transform.up * Time.fixedDeltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DestructibleEntity>(out var entity))
        {
            entity.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void SetGameOver(bool didPlayerWin) => _gameOver = true;
}