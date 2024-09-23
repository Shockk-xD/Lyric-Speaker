using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music Disk", menuName = "Music")]
public class MusicDiskData : ScriptableObject
{
    [field: SerializeField] public AudioClip MusicClip { get; private set; }
    [field: SerializeField] public Sprite Thumbnail { get; private set; }
    [field: SerializeField] public string MusicName { get; private set; }
    [field: SerializeField] public string Author { get; private set; }
    [field: SerializeField] public List<Lyric> Lyrics { get; private set; }
}
