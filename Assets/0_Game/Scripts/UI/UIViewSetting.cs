using System;
using System.Collections;
using System.Collections.Generic;
using JSAM;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewSetting : UIView
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Image _muteMusicMark;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Image _muteSoundMark;


    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _musicButton.onClick.AddListener(OnMusicButtonClick);
        _soundButton.onClick.AddListener(OnSoundButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        _musicButton.onClick.RemoveListener(OnMusicButtonClick);
        _soundButton.onClick.RemoveListener(OnSoundButtonClick);
    }

    public override void Init()
    {
        base.Init();
        CheckSoundAndMusicStatus();
    }

    public override void Show()
    {
        base.Show();
        CheckSoundAndMusicStatus();
    }

    public override void Hide()
    {
        base.Hide();
        DataManager.Instance.Save();
    }

    public void OnCloseButtonClick()
    {
        AudioManager.PlaySound(Sound.Click);
        Hide();
    }

    public void OnMusicButtonClick()
    {
        AudioManager.PlaySound(Sound.Click);
        DataManager.Instance.GameData.isMusicOn = !DataManager.Instance.GameData.isMusicOn;
        CheckSoundAndMusicStatus();
    }

    public void OnSoundButtonClick()
    {
        AudioManager.PlaySound(Sound.Click);
        DataManager.Instance.GameData.isSoundOn = !DataManager.Instance.GameData.isSoundOn;
        CheckSoundAndMusicStatus();
    }

    private void CheckSoundAndMusicStatus()
    {
        var isMusicOn = DataManager.Instance.GameData.isMusicOn;
        var isSoundOn = DataManager.Instance.GameData.isSoundOn;
        AudioManager.MusicMuted = !isMusicOn;
        AudioManager.SoundMuted = !isSoundOn;
        _muteSoundMark.gameObject.SetActive(AudioManager.SoundMuted);
        _muteMusicMark.gameObject.SetActive(AudioManager.MusicMuted);
    }
}
