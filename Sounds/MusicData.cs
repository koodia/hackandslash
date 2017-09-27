using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MusicData
{
    public MusicStyle musicStyle;
    public float waitSecondsBeforePlay;
    public string spesificFolderAndTrackName; //if MusicStyle = Specific
    public bool loop;
    public string previousTrack;
}

public enum MusicStyle
{
     NotSet
    ,Specific
    ,NoMusic
    ,Action
    ,Calm  //Relaxing //"Traveling"
    ,Mystery //Puzzle
    ,Boss
    ,Nature
    ,Town
}
