using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

///<summary>
/// For building the SceneType.Battle type scene and its content
/// <para>************************************************************************</para>
/// <para>PURPOSE   : For builds the SceneType.Battle type scene                  </para>
/// <para>LOGIC     : Design pattern in mind: "builder pattern"                   </para>
/// <para>COMMENTS  : 
///                   
///                                                                               </para>
/// <para>USE       : SceneBuilders                                               </para>
/// ******************************************************************************</summary>
/*******

********/
public class BattleSceneBuilder : ISceneBuilder, ISceneBuilderAdvanced
{
    private Scene scene;
    private const string _sceneName = "Basic battle for levels 1 to 10";
    private const SceneType _sceneType = SceneType.Battle;
  
    public BattleSceneBuilder(string name) 
    {
        this.scene = new Scene();
        scene.SceneName = name;
        this.scene.IsPrefabScene = false;

        /*
        if (currentScene != null)
        {
            //Overwrites
            scene.PreviousScene = (EScene)currentScene;
        }

        if (previousScene != null)
        {
            //Overwrites
            scene.PreviousScene = (EScene)currentScene;
        }
        */
    }

    public void QuickBuild()
    {
        InitScene();
        SetSceneDescription();
        SetSceneTypeAndScenario();
        Build_Stage();
        Choose_BattleSpawnPoints();
        Build_NeutralCreatures();
        Build_Enemies();
        Build_Obstacles();
        Build_GraveYard();
        SetStageCameraPosition();
        SetMusic();
    }

    public void InitScene()
    {
        scene.SceneName = _sceneName;
        //scene = scene.CalculateContentForThisScenetype(_sceneType);
        scene.Scenario = Scenario.Normal;
        scene.SceneType = SceneType.Empty;
        
        //Init all the lists here
        scene.SceneCreatures = new List<Creature>();
        scene.Obstacles = new List<Obstacle>();
        scene.GraveYard = new List<Creature>();
        scene.musicData = new MusicData();
    }

    public void SetSceneDescription()
    {
        this.scene.SceneName = _sceneName;
    }

    public void SetSceneTypeAndScenario()
    {
        this.scene.SceneType = SceneType.Battle;
        this.scene.Scenario = Scenario.Normal;
    }

    public void Build_Stage()
    {
        this.scene.Stage = BFMaker.BuildBattlefieldSizeOf(GC.rand.rnd.Next(1, 5), StageSize.Random);//Battlefield.BuildBattlefieldSizeOf(UnityEngine.GC.rand.rnd.Next(1, 5));
    }

    public Scene GetScene()
    {
        if (scene.IsSceneReady() == false)
        {
            throw new Exception("Scene is not fully ready, something is missing!");
        }
        return scene;
    }

    public void Choose_BattleSpawnPoints()
    {
        this.scene.Stage.SetBattlefieldSpawnPoints(true, true, true, true, true);
    }

    public void Build_Enemies()
    {
        const int MONSTER_COUNT_MIN = 1;
        this.scene.MonsterCount = GC.rand.rnd.Next(MONSTER_COUNT_MIN, scene.Stage.MonsterCountMax);
        
        for (int ii = 0; ii < this.scene.MonsterCount; ii++)
        {
            int MagicOrMeleeTag = GC.rand.rnd.Next(0, 3);
            //CreatureType type, CreatureCategory category, CreatureLvl lvl, CreatureTags attack new List<CreatureTags>() { (CreatureTags)MagicOrMeleeTag }
            Creature newCreature = CreatureMaker.BuildCreature(CreatureType.Blob,  CreatureCategory.Uncategorized, (CreatureTags)MagicOrMeleeTag, CreatureLvl.Lvl10, Aligment.Bad);
            this.scene.SceneCreatures.Add(newCreature);
        }

        if (scene.SceneCreatures.Count == 0)
        {
            Debug.Log("0 creatures in battlescene!");
            Debug.Break();
        }
    }

    public void Build_Obstacles()
    {
        this.scene.ItemsOnFloorCount = GC.rand.rnd.Next(0, 3);

        for (int ii = 0; ii < this.scene.ItemsOnFloorCount; ii++)
        {
            int pillarOrTree = GC.rand.rnd.Next(0, 3);
            Obstacle newObj = ObstacleMaker.BuildObstacle((ObstacleType)pillarOrTree);
            this.scene.Obstacles.Add(newObj);
        }
    }

    //Battle scene does not have the npcs at the moment. This might change later
    public void Build_NeutralCreatures()
    {
        //this.scene.NeutralCreatureCount = 1; //UnityEngine.GC.rand.rnd.Next(0, 1);

        //for (int ii = 0; ii < this.scene.NeutralCreatureCount; ii++)
        //{
        //    //No need to init the list. Use the same scene.SceneCreatures list
        //    int MagicOrMeleeTag = UnityEngine.GC.rand.rnd.Next(0, 1);
        //    //CreatureType type, CreatureCategory category, CreatureLvl lvl, CreatureTags attack new List<CreatureTags>() { (CreatureTags)MagicOrMeleeTag }
        //    Creature newCreature = CreatureMaker.BuildCreature(CreatureType.Villager, CreatureCategory.Uncategorized, (CreatureTags)MagicOrMeleeTag, CreatureLvl.Lvl10, Aligment.Neutral);
        //    this.scene.SceneCreatures.Add(newCreature);
        //}
    }

    public void Build_GraveYard()
    {
       //this.scene.GraveYardCount = 0;
    }

    //public void Build_BattleSpawnPoints(bool top, bool bot, bool mid, bool opposite, bool behind)
    //{
    //    throw new NotImplementedException();
    //}

    public void SetStageCameraPosition()
    {
        this.scene.Stage.SetCameraPosition(CameraPositions.Random);
      
    }

    public void SetMusic()
    {
        this.scene.musicData.loop = true;
        this.scene.musicData.waitSecondsBeforePlay = 1.0f;
        this.scene.musicData.musicStyle = MusicStyle.Action;
    }
}

