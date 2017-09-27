using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class SoundManager : Singleton<SoundManager>//: MonoBehaviour
{
    public const string SUPPORTED_FILE_FORMATS = "*.mp3";
    public const float WAIT_BEFORE_PLAY_MUSIC = 2.0f;

    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public AudioClip musicAudioClip;
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    public bool isPlayingMusic;
    public string[] actionMusicList;
    public string[] calmMusicList;
    public string[] mysteryMusicList;
    public string[] bossMusicList;
    public string[] natureMusicList;
    public string[] townMusicList;


    public  override void Awake()
    {
        base.Awake();
        ////Check if there is already an instance of SoundManager
        //if (instance == null)
        //{ 
        //    //if not, set it to this.
        //    instance = this;
        //}
        ////If instance already exists:
        //else if (instance != this)
        //{
        //    //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
        //    Destroy(gameObject);
        //}

        InitializeSongLists();
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //DontDestroyOnLoad(gameObject);

    }


    public void InitializeSongLists()
    {
        //Read all music lists into string lists switch will be kept alive the whole lifetime:
        actionMusicList = PopulateSongList(MusicStyle.Action);
        calmMusicList = PopulateSongList(MusicStyle.Calm);
        mysteryMusicList = PopulateSongList(MusicStyle.Mystery);
        bossMusicList = PopulateSongList(MusicStyle.Boss);
        natureMusicList = PopulateSongList(MusicStyle.Nature);
        townMusicList = PopulateSongList(MusicStyle.Town);
    }


    private string[] GetSongList(MusicStyle style)
    {
        switch (style)
        {
            case MusicStyle.Action:
                return this.actionMusicList;
            case MusicStyle.Boss:
                return this.bossMusicList;
            case MusicStyle.Calm:
                return this.calmMusicList;
            case MusicStyle.Mystery:
                return this.mysteryMusicList;
            case MusicStyle.Nature:
                return this.natureMusicList;
            case MusicStyle.Town:
                return this.townMusicList;
        }

        return null;
    }

    /// <summary>
    /// Returns the song url in format witch the Resources.Load() supports
    /// </summary>
    /// <param name="style"></param>
    /// <returns></returns>
    private string[] PopulateSongList(MusicStyle style)
    {
        // var assetFiles = GetFiles(GetSelectedPathOrFallback()).Where(s => s.Contains(".meta") == false); //Better syntax?

        string myPath = Application.dataPath + "/Resources/Music/" + style.ToString();
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles(SUPPORTED_FILE_FORMATS);
        string[] songlist = new string[info.Length];
        for (int i = 0; i < info.Length; i++)
        {
            songlist[i] = string.Concat("Music/",style.ToString(), "/", info[i].Name.Replace(".mp3", ""));
        }

        return songlist;
    }

    //Used to play single sound clips.
    public void PlaySound(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }


    public void PlayMusic(AudioClip clip)
    {
        //musicSource.clip = clip; //TODO, Coroutine joka odottaa että clippi ehtii feidaa
        StartCoroutine(PlayMusicTrack(WAIT_BEFORE_PLAY_MUSIC, clip));
    }

    /// <summary>
    /// TODO:has a small bug: Overwrites source before the last one can fade
    /// </summary>
    /// <param name="fadeTime"></param>
    public void FadeOutPlayingMusic(float fadeTime)
    {
        StartCoroutine(FadeOut(this.musicSource, fadeTime));
    }


    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }



    private IEnumerator PlayMusicTrack(float waitBeforePlay, AudioClip clip)
    {
        yield return new WaitForSeconds(waitBeforePlay);

        while (musicSource.isPlaying == true)
        {
            yield return null;
        }
        musicSource.clip = clip;

        musicSource.Play();
    }

    public void StopCurrentMusic()
    {
        //If not null and playing then stop audio
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void ActivateMusic(Scene scene)
    {
        AudioClip clip = null;
        if (scene.musicData.musicStyle == MusicStyle.Specific)
        {
            clip = (AudioClip)Resources.Load("Music/" + scene.musicData.spesificFolderAndTrackName);
            if (clip == null)
            {
                throw new NullReferenceException("Could not find music clip from path:" + scene.musicData.spesificFolderAndTrackName);
            }
            PlayMusic(clip);
        }
        else if (scene.musicData.musicStyle == MusicStyle.NoMusic || scene.musicData.musicStyle == MusicStyle.NotSet)
        {
            // Lets not play anything
        }
        else
        {
            clip = PickRandomSongFromStyle(scene.musicData); //throws if clip is null
            PlayMusic(clip);
        }
    }

    public AudioClip PickRandomSongFromStyle(MusicData musicData)
    {
        string[] songList = GetSongList(musicData.musicStyle);
        int randomIndex = GC.rand.rnd.Next(0, songList.Length - 1);

        string path = songList[randomIndex];
        if (musicData.previousTrack == path)
        {
            PickRandomSongFromStyle(musicData);
        }

        AudioClip clip = (AudioClip)Resources.Load(path);
        if (clip == null)
        {
            throw new NullReferenceException("Could not find load clip from path:" + path);
        }

        return clip;
    }

   

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    //public void RandomizeSfx(params AudioClip[] clips)
    //{
    //    //Generate a random number between 0 and the length of our array of clips passed in.
    //    int randomIndex = GC.rand.rnd.Next(0, clips.Length);

    //    //Choose a random pitch to play back our clip at between our high and low pitch ranges.
    //    float randomPitch = GC.rand.rnd.Next(lowPitchRange, highPitchRange);

    //    //Set the pitch of the audio source to the randomly chosen pitch.
    //    efxSource.pitch = randomPitch;

    //    //Set the clip to the clip at our randomly chosen index.
    //    efxSource.clip = clips[randomIndex];

    //    //Play the clip.
    //    efxSource.Play();
    //}



    //public void WWWMusic()
    //{
    //string pathi = "file:///Users/User2/Documents/Unity/projects/demo/Assets/Resources/Music/Calm/Ocllo.wav";
    //WWW www = new WWW(pathi);  //@"C:/Users/User2/Documents/Unity projects/demo/Assets/Resources/Music/Calm/Ocllo.wav");
    //source = GetComponent<AudioSource>();
    //source.clip = www.GetAudioClip(); // WWWAudioExtensions.GetAudioClip(); //www.audioClip
    //source.Play();
    //}
}

/*
 *  #pragma strict
 
 import System.Collections.Generic;
 import System.IO;
 
 static var path : String = "./"; // Is equal to where you have your executable
 static var fileTypes : String[] = ["ogg", "wav"]; // Valid file types
 
 static var files : FileInfo[];
 static var audioSource : AudioSource;
 static var audioClips : List.<AudioClip> = new List.<AudioClip>();
 
 function Start () {
     // If in editor the path is in Assets folder
     if (Application.isEditor)
         path = "Assets/";
     
     // Set an AudioSource to this object
     audioSource = audio;
     if(audioSource == null)
         audioSource = gameObject.AddComponent(AudioSource);
     
     // Find files in directory        
     yield GetFilesInDirectory();
     
     // Play a clip found in directory
     if (audioClips.Count>0) {
         audioSource.clip = audioClips[0];
         audioSource.Play();
     }
 }
 
 function GetFilesInDirectory () {
     var info : DirectoryInfo = new DirectoryInfo(path);
     files = info.GetFiles();
     for (var file : FileInfo in files) {
         var extension : String = Path.GetExtension(file.FullName);
         if (ValidType(extension))
             yield LoadFile(file.FullName);
     }
 }
 
 function ValidType (extension : String) : boolean {
     for (var validExtension : String in fileTypes)
         if (extension.IndexOf(validExtension) > -1)
             return true;
     return false;
 }
 
 function LoadFile (path : String) {
     var www : WWW = new WWW("file://"+path);
     var clip : AudioClip = www.audioClip;
     while (!clip.isReadyToPlay)
         yield;
     clip = www.GetAudioClip(false);
     var parts : String[] = path.Split("\\"[0]);
     clip.name = parts[parts.Length-1];
     audioClips.Add(clip);
 }

 */


//https://forum.unity3d.com/threads/audiosource-cross-fade-component.443257/

//https://forum.unity3d.com/threads/cant-play-audio-when-using-www-getaudioclip.418576/