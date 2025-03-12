using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private AudioMixer _audioMixer;
    [field:SerializeField] public Camera Camera { get; private set; }

    private int _defaultFurthestLevelComplete = 0;
    private float _defaultVolume = .5f;

    public Vector2 HalfWorldScreenSize { get; private set; }
    public int FurthestLevelComplete { get; private set; }
    public float MusicVolume { get; private set; }
    public float SfxVolume { get; private set; }

    public Vector2 ClampToCamera(Vector2 position)
    {
        return new Vector2(Mathf.Clamp(position.x, -HalfWorldScreenSize.x, HalfWorldScreenSize.x), Mathf.Clamp(position.y, -HalfWorldScreenSize.y, HalfWorldScreenSize.y));
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume = value);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolume) * 20f);
    }

    public void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat("SfxVolume", SfxVolume = value);
        _audioMixer.SetFloat("SfxVolume", Mathf.Log10(SfxVolume) * 20f);
    }

    public void SaveLevelCompletion()
    {
        var levelNumber = SceneManager.GetActiveScene().buildIndex;
        if (levelNumber > FurthestLevelComplete)
            PlayerPrefs.SetInt("FurthestLevelComplete", FurthestLevelComplete = levelNumber);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void LoadLevel(int number)
    {
        SceneManager.LoadScene(number);
        Cursor.visible = false;
    }

    public void LoadNextLevel() => SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    public void ReloadThisLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    protected override void Initialize()
    {
        base.Initialize();
        DontDestroyOnLoad(gameObject);
        HalfWorldScreenSize = new Vector2(Camera.orthographicSize * Camera.aspect, Camera.orthographicSize);
        FurthestLevelComplete = (PlayerPrefs.HasKey("FurthestLevelComplete") ? PlayerPrefs.GetInt("FurthestLevelComplete") : _defaultFurthestLevelComplete);
        SetMusicVolume(PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : _defaultVolume);
        SetSfxVolume(PlayerPrefs.HasKey("SfxVolume") ? PlayerPrefs.GetFloat("SfxVolume") : _defaultVolume);
    }
}
