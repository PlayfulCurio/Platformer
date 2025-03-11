using UnityEngine;

public class AutoScrollingBehaviour : MonoBehaviour
{
    [SerializeField] private float _scrollingSpeed;

    private bool _gameOver;

    private void Awake()
    {
        GameplayManager.Instance.OnGameOver += SetGameOver;
        Physics2D.autoSyncTransforms = true;
    }

    private void FixedUpdate()
    {
        if (!_gameOver)
            transform.position += Vector3.down * Time.fixedDeltaTime * _scrollingSpeed;
    }

    private void SetGameOver(bool didPlayerWin) => _gameOver = true;
}
