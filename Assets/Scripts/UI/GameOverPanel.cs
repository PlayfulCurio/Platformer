using System;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _statsText;
    [SerializeField] private TMP_Text _nextButtonText;

    private GameplayManager _gameplayManager;

    private void Awake()
    {
        _gameplayManager = GameplayManager.Instance;
        _gameplayManager.OnGameOver += Show;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _gameplayManager.OnGameOver -= Show;
    }

    private void Show(bool didPlayerWin)
    {
        if (didPlayerWin)
        {
            _titleText.text = "complete";
            _nextButtonText.text = "next";
        }
        else
        {
            _titleText.text = "destroyed";
            _nextButtonText.text = "retry";
        }
        _statsText.text = $"kills:        {_gameplayManager.Kills}\nhits:          {_gameplayManager.Hits}\n" +
            $"upgrades:  {_gameplayManager.Upgrades}\nscore:       {_gameplayManager.Score}";
        gameObject.SetActive(true);
    }
}
