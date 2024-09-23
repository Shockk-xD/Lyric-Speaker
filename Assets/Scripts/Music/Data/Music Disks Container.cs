using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Music Content", menuName = "Music")]
public class MusicDisksContainer : ScriptableObject
{
    public List<MusicDiskData> musicItems;

    private void OnValidate() {
        var musicsDuplicates = musicItems.GroupBy(item => item.MusicClip)
            .Where(array => array.Count() > 1);

        if (musicsDuplicates.Count() > 0) 
            throw new InvalidOperationException(nameof(musicItems));
    }
}
