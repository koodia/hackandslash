using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Specialized;

public static class SceneMaker
{
    // public static Dictionary<string, SceneType> sceneObjectList { get; private set; }
    /// <summary>
    /// Key = PrefabName(string)
    /// Value = SceneType(enum)
    /// </summary>
    public static OrderedDictionary sceneObjectList { get; private set; }

    public static void Init()
    {
        sceneObjectList = PopulateSceneList();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="style"></param>
    /// <returns></returns>
    //private static Dictionary<string, SceneType> PopulateSceneList()
    private static OrderedDictionary PopulateSceneList()
    {
        // var assetFiles = GetFiles(GetSelectedPathOrFallback()).Where(s => s.Contains(".meta") == false); //Better syntax?
        string myPath = Application.dataPath + "/Prefabs/SceneObjects/";
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.prefab"); //dir.GetFiles("prefab");
        //Dictionary<string, SceneType> sceneObjectList = new Dictionary<string, SceneType>();
        OrderedDictionary sceneObjectList = new OrderedDictionary();

        for (int i = 0; i < info.Length; i++)
        {
            if (info[i].Name.Substring(0, "Battle_".Length) == "Battle_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab",""), SceneType.Battle);
            }
            else if (info[i].Name.Substring(0,"Boss_".Length) == "Boss_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Boss) ;
            }
            else if (info[i].Name.Substring(0,"Empty_".Length) == "Empty_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Empty);
            }
            else if (info[i].Name.Substring(0,"Experience_".Length) == "Experience_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Experience);
            }
            else if (info[i].Name.Substring(0,"Puzzle_".Length) == "Puzzle_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Puzzle);
            }
            else if (info[i].Name.Substring(0,"Relax_".Length) == "Relax_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Relax);
            }
            else if (info[i].Name.Substring(0,"Reward_".Length) == "Reward_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Reward);
            }
            else if (info[i].Name.Substring(0,"Town_".Length) == "Town_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Town);
            }
            else if (info[i].Name.Substring(0,"Uncommon_".Length) == "Uncommon_")
            {
                sceneObjectList.Add(info[i].Name.Replace(".prefab", ""), SceneType.Uncommon);
            }
            else
            {
                Debug.Log("sceneObjectList does not support sceneObject name:\"" + info[i].Name + "\"Name will not be added to the list");
            }
        }

        return sceneObjectList;
    }


    /// <summary>
    /// Generates a scene
    /// 
    /// </summary>
    //public static void PrepareUpComingScene()
    public static Scene PrepareUpComingScene(SceneType type)
    {
        SceneEngineer engineer = null;
        Scene nextScene = new Scene();
        switch (type)
        {
            //case SceneType.Tutorial:
            //    engineer = new SceneEngineer(new TutorialSceneBuilder("Tutorial Scene"));
            //    break;
            case SceneType.Battle:
                engineer = new SceneEngineer(new BattleSceneBuilder("Normal Battle"));
                break;
            case SceneType.SpecialBattle:
                engineer = new SceneEngineer(new BattleSceneBuilder("TwoSidedBattle scene Super awesome"));
                break;
            case SceneType.Boss:
                engineer = new SceneEngineer(new BattleSceneBuilder("Boss Battle"));
                break;
            //case SceneType.SuperBoss:  //The scene needs to be created manually (currently, this might change)
            //    engineer = new SceneEngineer(new BattleSceneBuilder("Super Boss Battle"));
            //    break;
            //case SceneType.UncommonEvent: //The scene needs to be created manually (currently, this might change)
            //    engineer = new SceneEngineer(new BattleSceneBuilder("Uncommon Event"));
            //    break;
            case SceneType.Town: //The scene needs to be created manually (currently, this might change)
                engineer = new SceneEngineer(new TownSceneBuilder());
                break;
            case SceneType.Empty:
                engineer = new SceneEngineer(new BattleSceneBuilder("Empty scene"));
                break;
            //case SceneType.Reward: //The scene needs to be created manually (currently, this might change)
            //    engineer = new SceneEngineer(new BattleSceneBuilder("Reward"));
            //    break;
            //case SceneType.Puzzle: //The scene needs to be created manually (currently, this might change)
            //    engineer = new SceneEngineer(new BattleSceneBuilder("Puzzle"));
            //    break;
            default:
                Debug.Log("FATAL ERRROR, unknown type!: Scenetype:" + type);
                break;
        }

        //Wipe current scene and scene settings
        engineer.MakeScene();
        //Only saving, not loading yet
        nextScene = engineer.GetScene();
        nextScene.IsPrefabScene = false;

        return nextScene;
    }

    //public static Scene BuildFirstEScene() //Is called only once after character selection
    //{
    //    Scene newEScene = new Scene();
    //    newEScene = newEScene.CalculateContentForThisScenetype(SceneType.Empty, scene.Stage.stageSize);
    //    return newEScene;
    //}

    /// <summary>
    /// Used once when game begins
    /// </summary>
    public static void InitGameFirstScene()
    {
        //Don't initalize these as null to be sure that individual initializations work properly
        GC.GetCntr().Scene = new Scene();
        GC.GetCntr().Level = new Level();
        GC.GetCntr().Level.InitializeGame();
        GC.GetCntr().JourneyData = new JourneyData();
        //GC.GetCntr().journeyData.InsertTestData();
        GC.GetCntr().Score = new ScoreData();
        //GC.GetCntr().score.InsertTestData();

        //JOTAIN VANHAA:
        //if (loadLevel == "Level01")
        //{
        //}
        //GameObject tempObject = GameObject.FindGameObjectWithTag("Chest");
        //	MeshRenderer[] trans = 	tempObject.GetComponents<MeshRenderer>();
        /*
            if(trans != null)
            {
                if(scene.Loot> 0)
                {
                  //trans.enabled = true;

                    //myGUI.activeLootObject.SetActive(true);
                }
                else
                {
                    //trans.enabled = false;
                    //tempObject.SetActive(false);
                    //myGUI.activeLootObject.SetActive(false);
                }
            }
        }
       */
    }


    public static void ChooseReward()
    {
        //int losing = 10;
        //int ok = 40;
        //int dominating = 100;

        /*
		if (GC.GetCntr().singlePlayerData.Situation <= Situation.Struckling)
		{
			//Grant heal pot, but flip a coin
		}
        */

        if (GC.GetCntr().JourneyData.SufferPoints > 1000)
        {
            //Grant heal pot
        }

        //if (GameController.balance.BalancePoints < losing)
        //{
        //}
    }


    public static int DetermineCreatureLvlRangeMax(int totalExp)
    {
        if (totalExp < 2000)
        {
            return 5;
        }
        else if (totalExp > 2000 && totalExp < 10000)
        {
            return 20;
        }
        else if (totalExp > 30000 && totalExp < 60000)
        {
            return 30;
        }
        else if (totalExp > 100000)
        {
            return 100;
        }

        throw new Exception("Error,Creature level range is absurb!");
        //return 0;

    }

    //TODO
    public static List<Creature> ChooseCreatures(int count)
    {
        List<Creature> list = new List<Creature>();

        /* TODO
        //eWorlds; (ehkä);
        List<CreatureTags> featureCriteria = new List<CreatureTags>();
        int rangeMax = DetermineCreatureLvlRangeMax(GC.GetCntr().journeyData.TotalExp);
        foreach (Creature c in GC.GetCntr().scene.SceneCreatures)
        {
            if (Pools.poolCreature.FindObjectFromPool() == true)
            { continue; } //There is one in the pool, no need to create new one

            Creature creation = new Creature();
            creation = CreatureMaker.BuildCreature(1, rangeMax, featureCriteria);
            list.Add(creation);
        }
        */

        return list;
    }

}
