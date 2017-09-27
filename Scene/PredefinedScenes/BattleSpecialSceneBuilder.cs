using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BattleSpecialSceneBuilder : ISceneBuilder, ISceneBuilderAdvanced
{
    private Scene scene;
    private const string _sceneName = "Basic battle for levels 1 to 10";

    public BattleSpecialSceneBuilder(Scene currentScene = null, Scene previousScene = null) //WTF
    {
        this.scene = new Scene();
        if (currentScene != null)
        {
            //Overwrites
           // scene.PreviousScene = (Scene)currentScene;
        }

        if (previousScene != null)
        {
            //Overwrites
           // scene.PreviousScene = (Scene)currentScene;
        }
    }

    public void QuickBuild() 
    {
        //Make Scene "Normal way"
        //Scene newEScene = new Scene();
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
      }

    public void InitScene()
    {
        //this.scene = SceneMaker.CalculateContentForThisScenetype(SceneType.Empty);
        scene.SceneName = _sceneName;
        scene.Scenario = Scenario.Normal;
        scene.SceneType = SceneType.Empty;

        //Init lists in one place
        scene.SceneCreatures = new List<Creature>();
        scene.Obstacles = new List<Obstacle>();
        scene.GraveYard = new List<Creature>();
    }

    public void SetSceneDescription()
    {
        this.scene.SceneName = _sceneName;
    }

    public void SetSceneTypeAndScenario()
    { 
        this.scene.SceneType = SceneType.SpecialBattle;
        this.scene.Scenario = Scenario.Ambush;
    }

    public void Build_Stage()
    {
        this.scene.Stage = BFMaker.BuildBattlefieldSizeOf(GC.rand.rnd.Next(1, 5), scene.Stage.stageSize);
    }

    public void Choose_BattleSpawnPoints()
    {
        this.scene.Stage.SetBattlefieldSpawnPoints(true, true, true, true, true);
    }

    //None for now
    public void Build_NeutralCreatures()
    {

        //this.scene.NeutralCreatureCount = UnityEngine.GC.rand.rnd.Next(0, 1);

        //for (int ii = 0; ii < this.scene.NeutralCreatureCount; ii++)
        //{
        //    //No need to init the list. Use the same scene.SceneCreatures list
        //    int MagicOrMeleeTag = UnityEngine.GC.rand.rnd.Next(0, 1);
        //    //CreatureType type, CreatureCategory category, CreatureLvl lvl, CreatureTags attack new List<CreatureTags>() { (CreatureTags)MagicOrMeleeTag }
        //    Creature newCreature = CreatureMaker.BuildCreature(CreatureType.Villager, CreatureCategory.Uncategorized, (CreatureTags)MagicOrMeleeTag, CreatureLvl.Lvl10, Aligment.Neutral);
        //    this.scene.SceneCreatures.Add(newCreature);
        //}
    }

    public void Build_Enemies()
    {
        for (int ii = 0; ii < this.scene.MonsterCount; ii++)
        {
            int MagicOrMeleeTag = GC.rand.rnd.Next(0, 1);
            //CreatureType type, CreatureCategory category, CreatureLvl lvl, CreatureTags attack new List<CreatureTags>() { (CreatureTags)MagicOrMeleeTag }
            Creature newCreature = CreatureMaker.BuildCreature(CreatureType.Blob, CreatureCategory.Uncategorized,(CreatureTags)MagicOrMeleeTag,  CreatureLvl.Lvl10, Aligment.Bad);
            this.scene.SceneCreatures.Add(newCreature);
        }
    }

    public void Build_Obstacles()
    {
        this.scene.ItemsOnFloorCount = 1;
    }

    public void Build_GraveYard()
    {

    }

    public Scene GetScene()
    {
        if (scene.IsSceneReady() == false)
        {
            throw new UnityException("Scene is not fully ready, something is missing!");
        }
        return scene;
    }

    public void SetStageCameraPosition()
    {
        this.scene.Stage.SetCameraPosition(CameraPositions.Random);

    }

    public void SetMusic()
    {
        throw new NotImplementedException();
    }
}

