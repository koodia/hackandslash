using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TownSceneBuilder : ISceneBuilder, ISceneBuilderAdvanced
{
    private Scene scene;
    private const string _sceneName = "Village";

    public TownSceneBuilder(Scene currentScene = null, Scene previousScene = null)
    {
        this.scene = new Scene();

        if (currentScene != null)
        {
            //Overwrite the old
            // scene.PreviousScene = (Scene)currentScene;
        }

        if (previousScene != null)
        {
            //Overwrite the old
            // scene.PreviousScene = (Scene)currentScene;
        }
    }

    public void QuickBuild()
    {
        //Quick way:
        //Scene newEScene = new Scene();
        // newEScene = newEScene.CalculateContentForThisScenetype(SceneType.Empty);
        // this.scene = (Scene)newEScene;

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
    }

    //Tämä johonkin luokkaan
    public void InitScene()
    {
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
        this.scene.SceneType = SceneType.Town;
        this.scene.Scenario = Scenario.Empty;
    }

    public void Build_Stage()
    {
        this.scene.Stage = BFMaker.BuildBattlefieldSizeOf(GC.rand.rnd.Next(1, 5), scene.Stage.stageSize); //Battlefield.BuildBattlefieldSizeOf(UnityEngine.GC.rand.rnd.Next(1, 5));
    }

    public void Choose_BattleSpawnPoints()
    {
        scene.Stage.SetBattlefieldSpawnPoints(false, false, true, false, false);
        scene.Stage.MonsterCountMax = 0;

    }

    public void Build_Enemies()
    {
        this.scene.MonsterCount = 0;
    }

    public void Build_Obstacles()
    {
        this.scene.ItemsOnFloorCount = 1;
    }

    public void Build_GraveYard()
    {
       // this.scene.GraveYardCount = 0;
    }

    public Scene GetScene()
    {
        if (scene.IsSceneReady() == false)
        {
            throw new UnityException("Scene is not fully ready, something is missing!");
        }
        return scene;
    }


    public void Build_NeutralCreatures()
    {
        //this.scene.NeutralCreatureCount = 2;

        for (int ii = 0; ii < 2; ii++)
        {
            //No need to init the list. Use the same scene.SceneCreatures list
            int MagicOrMeleeTag = GC.rand.rnd.Next(0, 1);
            //CreatureType type, CreatureCategory category, CreatureLvl lvl, CreatureTags attack new List<CreatureTags>() { (CreatureTags)MagicOrMeleeTag }
            Creature newCreature = CreatureMaker.BuildCreature(CreatureType.Villager, CreatureCategory.Uncategorized, (CreatureTags)MagicOrMeleeTag, CreatureLvl.Lvl10, Aligment.Neutral);
            this.scene.SceneCreatures.Add(newCreature);
        }
    }

    public void SetStageCameraPosition()
    {
        this.scene.Stage.SetCameraPosition(CameraPositions.Intimate);

    }

    public void SetMusic()
    {
        throw new NotImplementedException();
    }
}

