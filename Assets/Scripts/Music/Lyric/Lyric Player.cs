using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class LyricPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _lyricText;

    private Animator _lyricTextAnimator;
    private const string Show = nameof(Show);
    private const string Hide = nameof(Hide);
    private const int ANIMATION_COUNT = 3;
    private int _animationIndex = 1;
    private const int ANIMATION_TRANSITION_TIME_IN_MILLISECONDS = 250;

    private List<Lyric> _currentLyrics;
    private int _nextLyricIndex = 0;
    private readonly float _timerIncreaseInterval = 0.1f;
    private int _lyricTextCount;
    private int _nextTimecodeInSeconds;
    private float _timer = 0;

    private bool _isPaused = false;

    private void Start() => _lyricTextAnimator = GetComponent<Animator>();

    public void Play(List<Lyric> lyrics) {
        _currentLyrics = lyrics;
        _lyricTextCount = lyrics.Count;

        if (_lyricTextCount > 0) {
            ResetPlayer();

            _lyricTextAnimator.enabled = true;
            InvokeRepeating(nameof(IncreaseTimer), 0, _timerIncreaseInterval);
        }
    }

    public void Pause() {
        _isPaused = true;
        _lyricTextAnimator.enabled = false;
    }

    public void Continue() {
        _isPaused = false;
        _lyricTextAnimator.enabled = true;
    }

    private async void ShowLyricText() {
        var currentLyricIndex = _nextLyricIndex;
        _nextLyricIndex++;

        if (_nextLyricIndex < _lyricTextCount) {
            var mins = _currentLyrics[_nextLyricIndex].timecode.minutes;
            var secs = _currentLyrics[_nextLyricIndex].timecode.seconds;
            _nextTimecodeInSeconds = mins * 60 + secs;
        }

        if (currentLyricIndex < _lyricTextCount) {
            DoHideAnimation();
            await UniTask.Delay(ANIMATION_TRANSITION_TIME_IN_MILLISECONDS);
            _lyricText.text = _currentLyrics[currentLyricIndex].text;
            DoShowAnimation();
        }

        if (_nextLyricIndex >= _lyricTextCount) {
            ResetPlayer();
            return;
        }
    }

    private void DoShowAnimation() {
        ChooseNextAnimation();
        _lyricTextAnimator.SetTrigger(Show + _animationIndex);
    }

    private void DoHideAnimation() =>
        _lyricTextAnimator.SetTrigger(Hide + _animationIndex);

    private void ChooseNextAnimation() {
        int randIndex;
        do {
            randIndex = Random.Range(1, ANIMATION_COUNT + 1);
        } while (randIndex == _animationIndex);

        _animationIndex = randIndex;
    }

    private void ResetPlayer() {
        _nextLyricIndex = 0;
        _timer = 0;
        _nextTimecodeInSeconds = 0;
        _isPaused = false;
        CancelInvoke(nameof(IncreaseTimer));
    }

    private void IncreaseTimer() {
        if (_isPaused) return;

        _timer += _timerIncreaseInterval;
        if (_timer >= _nextTimecodeInSeconds)
            ShowLyricText();
    }
}
