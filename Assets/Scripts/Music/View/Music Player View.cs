using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MusicPlayerView : MonoBehaviour
{
    public event Action OnMusicStopped;

    [SerializeField] private Slider _progressBar;
    [SerializeField] private Text _elapsedTimeText;
    [SerializeField] private Text _durationText;
    [SerializeField] private TMP_Text _currentlyPlayingMusicNameText;

    [SerializeField] private GameObject _backgroundDecorationsSpawner;

    [SerializeField] private Image _playerButtonImage;
    [SerializeField] private Sprite _playImage;
    [SerializeField] private Sprite _pauseImage;

    private bool _isPaused = false;
    private float _elapsedTime;

    private void Awake() {
        if (_progressBar == null)
            throw new NullReferenceException(nameof(_progressBar));
        if (_elapsedTimeText == null)
            throw new NullReferenceException(nameof(_elapsedTimeText));
        if (_durationText == null)
            throw new NullReferenceException(nameof(_durationText));
        if (_currentlyPlayingMusicNameText == null)
            throw new NullReferenceException(nameof(_currentlyPlayingMusicNameText));
        if (_backgroundDecorationsSpawner == null)
            throw new NullReferenceException(nameof(_backgroundDecorationsSpawner));
        if (_playerButtonImage == null)
            throw new NullReferenceException(nameof(_playerButtonImage));
    }

    public void Play(MusicDiskData musicDiskData) {
        _isPaused = false;

        StopAllCoroutines();
        StartCoroutine(ProgressBarAnimation(musicDiskData));

        _playerButtonImage.sprite = _pauseImage;
        _currentlyPlayingMusicNameText.text = $"{musicDiskData.MusicName}\n" +
                                              $"<size=50>{musicDiskData.Author}</size>";

        _backgroundDecorationsSpawner.SetActive(true);
    }

    public void Stop() {
        StopAllCoroutines();
        _backgroundDecorationsSpawner.SetActive(false);
        _isPaused = false;
        _playerButtonImage.sprite = _playImage;
    }

    public void Continue() {
        _isPaused = false;
        _backgroundDecorationsSpawner.SetActive(true);
        _playerButtonImage.sprite = _pauseImage;
    }

    public void Pause() {
        _isPaused = true;
        _backgroundDecorationsSpawner.SetActive(false);
        _playerButtonImage.sprite = _playImage;
    }

    private IEnumerator ProgressBarAnimation(MusicDiskData musicData) {
        int duration = (int)Mathf.Round(musicData.MusicClip.length);
        _elapsedTime = 0;
        float interval = 0.1f;

        _durationText.text = $"{duration / 60:D2}:{duration % 60:D2}";
        while (_elapsedTime <= duration) {
            if (_isPaused) {
                yield return null;
            } else {
                float t = Mathf.Clamp01(_elapsedTime / duration);
                _elapsedTimeText.text = $"{(int)_elapsedTime / 60:D2}:{(int)_elapsedTime % 60:D2}";
                _progressBar.value = t;

                _elapsedTime += interval;
                yield return new WaitForSeconds(interval);
            }
        }
        _elapsedTimeText.text = $"{duration / 60:D2}:{duration % 60:D2}";

        Stop();
        OnMusicStopped.Invoke();
    }
}
