using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Awake()
    {
        _musicSlider.value = GameManager.Instance.MusicVolume;
        _sfxSlider.value = GameManager.Instance.SfxVolume;
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    private void ChangeMusicVolume(float value) => GameManager.Instance.SetMusicVolume(value);
    private void ChangeSfxVolume(float value) => GameManager.Instance.SetSfxVolume(value);
}
