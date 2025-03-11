using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _statsText;
    [SerializeField] private TMP_Text _nextButtonText;

    private GameplayManager _gameplayManager;
    private bool _didPlayerWin;

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

    public void CallNextButtonPressed()
    {
        if (_didPlayerWin)
            GameManager.Instance.LoadNextLevel();
        else
            GameManager.Instance.ReloadThisLevel();
    }

    public void CallBackButtonPressed()
    {
        GameManager.Instance.LoadMainMenu();
    }

    private void Show(bool didPlayerWin)
    {
        _didPlayerWin = didPlayerWin;
        _statsText.text = $"kills:        {_gameplayManager.Kills}\nhits:          {_gameplayManager.Hits}\n" +
            $"upgrades:  {_gameplayManager.Upgrades}\nscore:       {_gameplayManager.Score}";
        if (didPlayerWin)
        {
            _titleText.text = "complete";
            _nextButtonText.text = "next";
            gameObject.SetActive(true);
        }
        else
        {
            _titleText.text = "destroyed";
            _nextButtonText.text = "retry";
            GameplayManager.Instance.StartCoroutine(SetActiveDelayed());
        }
    }

    private IEnumerator SetActiveDelayed()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(true);
    }
}
