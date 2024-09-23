using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MusicDiskView : MonoBehaviour
{
    public event Action<MusicDiskData, int> OnPlayButtonClick;

    [SerializeField] private Button _playButton;
    [SerializeField] private Image _thumbnailImage;
    [SerializeField] private Text _authorText;
    [SerializeField] private Text _musicNameText;

    private MusicDiskData _musicDiskData;
    private CanvasAnimator _canvasAnimator;

    [Inject]
    public void Construct(CanvasAnimator canvasAnimator) =>
        _canvasAnimator = canvasAnimator;

    private void Start() {
        if (_playButton == null)
            throw new NullReferenceException(nameof(_playButton));
        if (_thumbnailImage == null)
            throw new NullReferenceException(nameof(_thumbnailImage));
        if (_authorText == null)
            throw new NullReferenceException(nameof(_authorText));
        if (_musicNameText == null)
            throw new NullReferenceException(nameof(_musicNameText));

    }

    public void Initialize(MusicDiskData musicDiskData, int musicDiskIndex) {
        _musicDiskData = musicDiskData;

        _thumbnailImage.sprite = musicDiskData.Thumbnail;
        _authorText.text = musicDiskData.Author;
        _musicNameText.text = musicDiskData.MusicName;

        _playButton.onClick.AddListener(() => {
            OnPlayButtonClick.Invoke(_musicDiskData, musicDiskIndex);
            _canvasAnimator.SetTrigger(CanvasAnimatorTrigger.PlayMusic);
        });   
    }
}
