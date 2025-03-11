using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [field:SerializeField] public Camera Camera { get; private set; }

    public Vector2 HalfWorldScreenSize { get; private set; }
    public int FurthestLevelComplete { get; private set; }

    public Vector2 ClampToCamera(Vector2 position)
    {
        return new Vector2(Mathf.Clamp(position.x, -HalfWorldScreenSize.x, HalfWorldScreenSize.x), Mathf.Clamp(position.y, -HalfWorldScreenSize.y, HalfWorldScreenSize.y));
    }

    public void LoadMainMenu() => SceneManager.LoadScene(0);
    public void LoadNextLevel() => SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    public void ReloadThisLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void LoadLevel(int number) => SceneManager.LoadScene(number);

    protected override void Initialize()
    {
        base.Initialize();
        DontDestroyOnLoad(gameObject);
        HalfWorldScreenSize = new Vector2(Camera.orthographicSize * Camera.aspect, Camera.orthographicSize);
    }
}
