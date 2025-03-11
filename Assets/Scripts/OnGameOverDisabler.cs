using UnityEngine;

public class OnGameOverDisabler : MonoBehaviour
{
    [SerializeField] private Behaviour[] _behavioursToDisable;

    private GameplayManager _gameplayManager;

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

    private void SetGameOver(bool didPlayerWin)
    {
        foreach (var behaviour in _behavioursToDisable) 
            behaviour.enabled = false;
    }
}
