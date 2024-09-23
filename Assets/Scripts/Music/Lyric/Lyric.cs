using UnityEngine;

[System.Serializable]
public class Lyric {
    [TextArea()] public string text;
    public Timecode timecode;
}

[System.Serializable]
public class Timecode {
    [Range(0, 10)] public int minutes;
    [Range(0, 60)] public int seconds;
}