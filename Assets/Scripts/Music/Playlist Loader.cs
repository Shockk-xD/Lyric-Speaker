using System;
using UnityEngine;
using Zenject;

public class PlaylistLoader : MonoBehaviour
{
    [SerializeField] private MusicDisksContainer _musicDisksContainer;
    [SerializeField] private GameObject _musicDiskViewPrefab;

    private DiContainer _diContainer;
    private MusicController _musicController;

    [Inject]
    public void Construct(DiContainer diContainer, MusicController musicController) {
        _diContainer = diContainer;
        _musicController = musicController;
    }
        

    private void Start() {
        Load();
    }

    private void Load() {
        var musicDisks = _musicDisksContainer.musicItems;
        int musicIndex = 0;

        foreach (var musicDisk in musicDisks) {
            var musicDiskViewPrefab = _diContainer.InstantiatePrefab(_musicDiskViewPrefab,
                                                                     transform);

            if (!musicDiskViewPrefab.TryGetComponent<MusicDiskView>(out var musicDiskView))
                throw new NullReferenceException(nameof(musicDiskView));

            musicDiskView.Initialize(musicDisk, musicIndex);
            musicDiskView.OnPlayButtonClick += _musicController.Play;

            musicIndex++;
        }
    }
}
