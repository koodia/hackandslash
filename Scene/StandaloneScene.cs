
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public sealed class StandaloneScene : MonoBehaviour
{
    [TextArea(10, 10)]
    public string Description;

    [SerializeField]
    public Scene scene;

    //Default base values which are overrided in the end
    public StandaloneScene()
    {
        scene = new Scene();
        scene.Stage = new Stage(1); //TODO notice the 1!!
        //scene.SceneName = sceneName;

        //scene = scene.CalculateContentForThisScenetype(sceneType);
        scene.Scenario = Scenario.Normal;
        scene.SceneType = SceneType.Empty;
        scene.IsPrefabScene = true;


        //Init all the lists here
        scene.SceneCreatures = new List<Creature>(); //saattaa alustaa?
        scene.Obstacles = new List<Obstacle>(); //saattaa alustaa?
        scene.GraveYard = new List<Creature>(); //saattaa alustaa?
        scene.musicData = new MusicData(); //saattaa alustaa?
        SetMusic();
       // scene.Stage.SetBattlefieldSize(1);
    }

    public void Start()
    {
        QuickBuild();
    }

    public Scene Scene
    {
        get { return scene; }
        set { this.scene = value; }
    }

    public void QuickBuild()
    {
        //Dont rebuild stuff if scene is created by builder
        if (scene.IsPrefabScene == true)
        {
            Build_Stage();
        }

       // SetSpawnPoints();

    }

    public Scene GetScene()
    {
        if (scene.IsSceneReady() == false)
        {
            Debug.Log("Standalone Scene is not ready, some values are still defaults or empty");
        }

        return scene;
    }

    //Rescale the Stage elements
    public void Build_Stage()
    {
        if (scene.Stage.stageSize == StageSize.NotSet)
        {
            Debug.Log(" StageSize is NotSet in a Standalone scene");
        }

        if (scene.Stage.stageSize == StageSize.Custom)
        {
            scene.Stage.RescaleCustomSizeStage(4); //4 is max at the moment
           // scene.Stage.RescaleStage(StageSize.Small); //TODO: just temporary
        }
        else
        {

            scene.Stage.RescaleStage(scene.Stage.stageSize);
        }
    }

    //TODO: how will we do this?
    //public void SetSpawnPoints()
    //{
    //    if (scene.SceneType == SceneType.Puzzle)
    //    {
    //        scene.Stage.SetBattlefieldSpawnPoints(false, false, false, false, false);
    //    }
    //}

    public void SetMusic()
    {
        this.scene.musicData.loop = true;
        this.scene.musicData.waitSecondsBeforePlay = 1.0f;
        
    }
}
