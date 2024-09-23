using Zenject;
using UnityEngine;
using System;

public class MusicPlayerInstaller : MonoInstaller
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private MusicPlayerView _musicPlayerView;
    [SerializeField] private LyricPlayer _lyricPlayer;

    public override void InstallBindings() {
        InstallMusicPlayer();
    }

    private void InstallMusicPlayer() {
        if (_musicSource == null)
            throw new NullReferenceException(nameof(_musicSource));

        if (_musicPlayerView == null)
            throw new NullReferenceException(nameof(_musicPlayerView));

        if (_lyricPlayer == null)
            throw new NullReferenceException(nameof(_lyricPlayer));

        MusicPlayer musicPlayer = new(_musicSource);
        Container.
            Bind<MusicPlayer>().
            FromInstance(musicPlayer).
            AsSingle();
    }
}
