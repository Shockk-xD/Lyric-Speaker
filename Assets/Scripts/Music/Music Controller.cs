using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MusicController : MonoBehaviour
{
    [SerializeField] private LyricPlayer _lyricPlayer;
    [SerializeField] private MusicPlayerView _musicPlayerView;
    [SerializeField] private MusicDisksContainer _disksContainer;
    private MusicPlayer _musicPlayer;

    private int _currentPlayingMusicId;
    private bool _isPaused = true;
    private bool _isPlaying = false;

    [Inject]
    public void Construct(MusicPlayer musicPlayer) =>
        _musicPlayer = musicPlayer;

    private void OnEnable() =>
        _musicPlayerView.OnMusicStopped += Stop;

    private void OnDisable() =>
        _musicPlayerView.OnMusicStopped -= Stop;

    public void Play(MusicDiskData musicData, int musicIndex) {
        _isPaused = false;
        _isPlaying = true;

        _musicPlayer.Play(musicData);
        _lyricPlayer.Play(musicData.Lyrics);
        _musicPlayerView.Play(musicData);

        _currentPlayingMusicId = musicIndex;
    }

    public void ContinuePauseToggle() {
        if (!_isPlaying) return;

        _isPaused = !_isPaused;

        if (_isPaused)
            Pause();
        else
            Continue();
    }

    public void SkipToNext() {
        if (_isPaused)
            ContinuePauseToggle();

        var musicDisks = _disksContainer.musicItems;
        var nextMusicId = _currentPlayingMusicId + 1;
        nextMusicId = MathfExtensions.LoopedClamp(nextMusicId, 0, musicDisks.Count - 1);

        var nextMusic = musicDisks[nextMusicId];
        Play(nextMusic, nextMusicId);
    }

    public void SkipToPrevious() {
        if (_isPaused)
            ContinuePauseToggle();

        var musicDisks = _disksContainer.musicItems;
        var previousMusicId = _currentPlayingMusicId - 1;
        previousMusicId = MathfExtensions.LoopedClamp(previousMusicId, 0, musicDisks.Count - 1);

        var previousMusic = musicDisks[previousMusicId];
        Play(previousMusic, previousMusicId);
    }

    private void Continue() {
        _musicPlayer.Continue();
        _lyricPlayer.Continue();
        _musicPlayerView.Continue();
    }

    
    private void Pause() {
        _musicPlayer.Pause();
        _lyricPlayer.Pause();
        _musicPlayerView.Pause();
    }

    private void Stop() =>
        _isPlaying = false;
}
