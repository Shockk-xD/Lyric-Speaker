using UnityEngine;

public class MusicPlayer
{
    private readonly AudioSource _musicSource;

    public MusicPlayer(AudioSource musicSource) =>
        _musicSource = musicSource;

    public void Play(MusicDiskData musicData) {
        _musicSource.clip = musicData.MusicClip;
        _musicSource.Play();
    }

    public void Continue() => _musicSource.UnPause();

    public void Pause() => _musicSource.Pause();
}
