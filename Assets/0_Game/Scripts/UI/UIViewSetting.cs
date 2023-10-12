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

    public override void Show()
    {
        base.Show();
        CheckSoundAndMusicStatus();
    }

    public void OnCloseButtonClick()
    {
        Hide();
    }

    public void OnMusicButtonClick()
    {
        AudioManager.MusicMuted = !AudioManager.MusicMuted;
        CheckSoundAndMusicStatus();
    }

    public void OnSoundButtonClick()
    {
        AudioManager.SoundMuted = !AudioManager.SoundMuted;
        CheckSoundAndMusicStatus();
    }

    private void CheckSoundAndMusicStatus()
    {
        _muteSoundMark.gameObject.SetActive(!AudioManager.SoundMuted);
        _muteMusicMark.gameObject.SetActive(!AudioManager.MusicMuted);
    }
}
