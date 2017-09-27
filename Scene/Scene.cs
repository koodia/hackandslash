using UnityEngine;
using System.Collections.Generic;


public class Environment
{
    //Daytime
    public DayTime daytime;
    public float timeSpeed;
   
}

public enum Wether
{
    Sunny, //Default
    Rain,
    Windy,
    Thunderstorm,
    Floading,
    Fog,
    Sandstorm
}

public enum DayTime
{
    Morning,
    Day,
    Noon,
    Midnight,
}

[System.Serializable]
public class Scene
{
    [SerializeField]
    public bool IsPrefabScene; //{ get; set; }
    [SerializeField]
    public Scenario Scenario;
    [SerializeField]
    public SceneType SceneType; // { get; set; }
    [SerializeField]
    public SceneType PreviousSceneType; // { get; set; }
    [SerializeField]
    public string SceneName; // { get; set; }
    [SerializeField]
    public int Loot; // { get; set; }
    [SerializeField]
    public bool Reward; // { get; set; }
    [SerializeField]
    private Stage stage;
    [SerializeField]
    public MusicData musicData; // { get; set; }
    [SerializeField]
    public List<int> SpawnPoints; // { get; set; }
    [SerializeField]
    public List<Creature> SceneCreatures; // { get; set; }
    [SerializeField]
    public List<Obstacle> Obstacles; // { get; set; }
    [SerializeField]
    public List<Item> Items; // { get; set; }

    public int MonsterCount; // { get; set; }
    [HideInInspector]
    public int NeutralCreatureCount; //{ get; set; }
    [HideInInspector]
    public int GraveYardCount { get; private set; }
    [HideInInspector]
    public int ItemsOnFloorCount  { get; set; }
    [HideInInspector]
    public int ObstacleCount { get; set; }
    [HideInInspector]
    public int PlayersSpawnX { get; set; }
    [HideInInspector]
    public int PlayersSpawnY { get; set; }


    //public List<FieldObjects> SceneObjects;
   // [SerializeField]
    public List<Creature> GraveYard; //Tätä pitää miettiä, tosin menee kuitenkin pooliin vaikka nyt erillisenä listana

    public Scene()
    {
        // Keep the null values so we know later that something is gone horribly wrong

    

    }

    public void SetBattlefield(int size, StageSize stageSize)
    {
        stage = new Stage();
        Stage bf = BFMaker.BuildBattlefieldSizeOf(1, stageSize); //Battlefield.BuildBattlefieldSizeOf(1);
        stage = bf;
    }

    public Stage Stage
    {
        get { return stage; }
        set { stage = value; }
    }

    ///<summary>
    /// Makes sure the scenebuilder has made a scene correctly. Will log warning and 
    /// throws exception if scene is missing something
    /// <para>************************************************************************</para>
    /// <para>PURPOSE   : Tests the scene content before it can be used in the game. 
    /// Test inlogical values, parameters in certain scenarios                        </para>
    /// <para>LOGIC     : Simple if checks                                            </para>
    /// <para>COMMENTS  : Last line of defence against developer doing stupid 
    ///                   things. Developer needs to keep this updated when the game 
    ///                   logic is created or changes                                 </para>
    /// <para>USE       : SceneBuilders                                               </para>
    /// ******************************************************************************</summary>
    /*******
     History   : 08/01/2017 Henry E - Created
    ********/
    public bool IsSceneReady()
    {
        bool passes = true;

        if (stage == null)
        {
            Debug.Log("battlefield is null");
            passes = false;
        }

        if (stage.CameraDistance == 0)
        {
            Debug.Log("CameraAngle is not set!");
            passes = false;
        }
        if (stage.CameraAngle == 0)
        {
            Debug.Log("CameraAngle is not set!");
            passes = false;
        }


        if (stage.cameraPosition != CameraPositions.Custom)
        {
            if (stage.CameraDistance == 1)
            {
                Debug.Log("CameraPositions is not Spesific but CameraDistance value is 1?!");
                passes = false;
            }
            if (stage.CameraAngle == 1)
            {
                Debug.Log("CameraPositions is not Spesific but CameraAngle value is 1?!");
                passes = false;
            }
        }


        //if (Scenario == Scenario.NotSet) //Normal will be the default?
        //{
        //    Debug.Log("Scenario is NotSet");
        //    passes = false;
        //}

        //Return this when you fix the initialization for good
        //if (stage.Size == (int)StageSize.NotSet)
        //{

        //    Debug.Log("stage.Size is 0 (NotSet)");
        //    passes = false;
        //}


        //SceneType


        //if (PreviousScene == null)
        //{
        //    Debug.Log("PreviousScene is null");
        //    return false;
        //}

        if (SceneName == "")
        {
            Debug.Log("Scene does not have a name");
                passes = false;

        }

        /* 
        Loot
        MonsterCount
        ItemsOnFloor
        FieldObjects
        Reward
        PlayersSpawnX = 0
        PlayersSpawnY = 0
        */

        //if (PlayersSpawnX == 0)
        //{
        //    Debug.Log("PlayersSpawnX is 0");
        //    return false;
        //}

        //if (PlayersSpawnY == 0)
        //{
        //    Debug.Log("PlayersSpawnY is 0");
        //    return false;
        //}

        //if (SpawnPoints == null)
        //{
        //    Debug.Log("No any SpawnPoints set");
        //    return false;
        //}

        if (SceneCreatures == null)
        {
            Debug.Log("SceneCreatures is null");
            passes = false;
        }

        if (GraveYard == null)
        {
            Debug.Log("GraveYard is null");
            passes = false;
        }

        if (Obstacles == null)
        {
            Debug.Log("SceneObjects is null");
            passes = false;
        }

        //Creature count cannot be 0 in battle
        if (SceneCreatures.Count == 0 && SceneType == SceneType.Battle ||
            SceneCreatures.Count == 0 && SceneType == SceneType.Boss)
        {
            Debug.Log("SceneType is set to battle but there are no creatures. SceneScreatures.Count:" + SceneCreatures.Count);
            passes = false;
        }

        if (stage.cameraPosition == CameraPositions.Intimate &&  SceneType == SceneType.Battle ||
            stage.cameraPosition == CameraPositions.Intimate &&  SceneType == SceneType.Boss ||
             stage.cameraPosition == CameraPositions.Intimate && SceneType == SceneType.Boss 
            )
        {
            Debug.Log("Enum CameraPositions is too close");
            passes = false;
        }

        if (stage.cameraPosition == CameraPositions.NotSet)
        {
            Debug.Log("Camera position is not set");
            passes = false;
        }

        //TODO: too much to think about
        //if (stage.cameraLock == true && stage.Size != 1) //stage.cameraLock == true && stage.cameraPosition == CameraPositions.Spesific
        //{
        //    Debug.Log("Camera lock can only be used in scene of size 1");
        //    passes = false;
        //}

        if (passes == false)
        {
            Debug.Log("Current scene type is:\"" + SceneType + "\"");
        }

           return passes;
    }







    //public void DetermineNextEScene()
    //{
    //    //GameController.balance.SufferPoints

    //    if (GC.GetCntr().level.IsFinalScene == true)
    //    {
    //        GC.GetCntr().level.ChangeNewLevel();
    //    }
    //    else
    //    {
    //        SceneType chosenScenario = DetermineRandomESceneType();
    //        CalculateContentForThisScenetype(chosenScenario);

    //        //LoadEScene()
    //    }
    //}

    /*
	public string ChooseScenarioType(string previousScenario)
	{
		// SelectLocation(previousScenario);	
		 
		 return null;
	}
	*/


    public void ChooseESceneObjects()
    {
        Debug.Log("EScene objects chosen");

    }


    public void ChooseReward()
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

    public void ChooseCreatures()
    {

    }

    public void ChooseLoot()
    {

    }



    //public void MakeCreatureIfNotInPool()
    //{
    //    List<Creature> list = new List<Creature>();
    //    foreach (Creature creature in GC.GetCntr().scene.SceneCreatures)
    //    {
    //        //Check if pool has one already and take it
    //        if (Pools.poolCreature.FindObjectFromPool() == true)
    //        {
    //            Pools.poolCreature.GetPooledObject();
    //        }
    //        else
    //        {
    //            CreatureMaker.BuildCreature();
    //        }
    //    }
    //}

    //public void InstantiateCreatures()?

    //public void SetCreaturesActive()
    //{
    //    List<Creature> list = new List<Creature>();
    //    foreach (Creature creature in GC.GetCntr().Scene.SceneCreatures)
    //    {

    //    }

    //   // return list;
    //}


    ///TODO KIRJOITA UUDELLEEN!
    public SceneType DetermineNextScene()
    {
        SceneType? chosenScenario = null;
        //GameController.balance.SufferPoints

        //if (GC.GetCntr().Level.IsFinalScene == true)
        //{
        //    //Load new level
        //    GC.GetCntr().Level.ChangeNewLevel();
        //    //First scene in the list
        //    chosenScenario = GC.GetCntr().Level.LevelScenes[0];
        //    GC.GetCntr().Level.IsFinalScene = false;
        //    Debug.Log("Level changed");
        //}
        //else
        //{
            GC.GetCntr().Level.CurrentLoopIndex += 1;
            chosenScenario = GC.GetCntr().Level.LevelScenes[GC.GetCntr().Level.CurrentLoopIndex]; // -1

            //If the scene is final in the loop
            if (GC.GetCntr().Level.LevelScenes.Length - 1 == GC.GetCntr().Level.CurrentLoopIndex)  //    if (GC.GetCntr().Level.LevelScenes.Length - 1 == GC.GetCntr().Level.CurrentLoopIndex) 
            {
                GC.GetCntr().Level.IsFinalScene = true;
            }

        // CalculateContentForThisScenetype(chosenScenario);??
        //  }

    

        return (SceneType)chosenScenario;
    }



    //public SceneType DetermineNextESceneType()
    //{
    //    //ScenarioType
    //    this.PreviousScene.SceneType = this.SceneType;
    //    SceneType type = SceneType.Battle;
    //    //tähän IF jos sama, ei valita samaa	
    //    return type;
    //}

    public SceneType DetermineRandomESceneType()
    {
        //ScenarioType
        this.PreviousSceneType = this.SceneType;
        SceneType type = Help.GetRandomEnumValue<SceneType>(); // Tätä ei kyllä vedetä randilla
                                                               //tähän IF jos sama, ei valita samaa	
        return type;
    }



    public void SetObjectsToEScene()
    {
        //GameObject obj = GameObject.Find("Monster Generator");
        //MonsterGenerator generator = obj.GetComponent<MonsterGenerator>();
        //for (int ii = 0; ii < GC.GetCntr().Scene.MonsterCount; ii++)
        //{
        //    generator.Setup();
        //}
    }

    /*
        public Battlefield BuildBattlefield_SizeOF(int battleFieldLength)
        {
            Battlefield nextBattlefieldSize = new Battlefield(1);
            // No need for min values, both x and y are 0;
            // battlefield.HeightY = 100;
            //battlefield.WidthX = 100;
            //battlefield.DeptZ = 200;

            return nextBattlefieldSize;
        }
        */

    //public Scene CalculateContentForThisScenetype(SceneType sceneType)
    //{
    
    //       Scene nextScene = new Scene();
    //    /*
    //        1.	Basic loop
    //        2.	Basic loop
    //        3.	Good Special EVENT
    //        4.	Basic loop
    //        5.	Hell loop
    //        6.	Boss battle EVENT(Reward)
    //        7.	Basic loop
    //        8.	Relax (1 – 3 areas) , shops, camps etc
    //        9.	To the next world . . . If there are no more levels, go to Eng Game Loop
    //    */

    //    //  nextEScene = PopulateESceneContent(nextEScene, PreSettings.settingsBeginning, SceneType);

    //    switch (sceneType)
    //    {
          
    //        case SceneType.Battle:
    //            ChooseLoot();
    //            ChooseCreatures();
    //            // ChooseSceneObjects();
    //            ChooseReward();
    //            break;

    //        case SceneType.SpecialBattle:
    //            ChooseLoot();
    //            ChooseCreatures();
    //            // ChooseSceneObjects();
    //            ChooseReward();
    //            break;

    //        case SceneType.Boss:
    //            ChooseLoot();
    //            ChooseCreatures();
    //            ChooseESceneObjects();
    //            ChooseReward();
    //            break;

    //        //case SceneType.Boss: //Super
    //        //    ChooseLoot();
    //        //    ChooseCreatures();
    //        //    // ChooseSceneObjects();
    //        //    ChooseReward();
    //        //    break;

    //        case SceneType.Puzzle:
    //            ChooseCreatures();
    //            //ChooseSceneObjects();
    //            ChooseReward();
    //            break;
    //        case SceneType.Empty:
    //            ChooseESceneObjects();
    //            break;
    //        case SceneType.Uncommon: 
    //            //ChooseSceneObjects();
    //            ChooseReward();
    //            break;
    //        case SceneType.Town:
    //            ChooseLoot();
    //            ChooseCreatures();
    //            ChooseESceneObjects();
    //            ChooseReward();
    //            break;

    //        default:
    //            Debug.Log("DEFAULT CASE, So nothing!");
    //            break;

             
    //    }
   
    //    nextScene.stage = BFMaker.BuildBattlefieldSizeOf(1);
    //    return nextScene;
    //}

   

    

/*
    public EScene PopulateESceneContent(Scene scene, SceneSettings settings, SceneType ESceneType)
    {
        //Pidä mielessä, sillä nämä vaikuttaa arvoihin
        //GameController.balance.BalancePoints;
        //GameController.balance.SufferPoints;
        //GameController.balance.TotalExp;

        //Game maximums:
        int monsterCountMax = settings.monsterCountMax;
        int rewardOccurence = settings.rewardOccurence;
        int rewardCountMax = settings.rewardCountMax;
        int objectCountMax = settings.objectCountMax;
    
       Scene.SceneType = ESceneType;

        //Monster count
        int monsterCount = UnityEngine.GC.rand.rnd.Next(0, monsterCountMax);
        Scene.MonsterCount = monsterCount;

        //Monster loot
        int lootCount = UnityEngine.GC.rand.rnd.Next(0, monsterCount);
       Scene.Loot = lootCount; //Tätä ei välttis tarvita 

        //Chest loot
        int chest = UnityEngine.GC.rand.rnd.Next(0, 10); //10%
        if (chest == 10)
        {
            EScene.FieldObjects = 1;
        }
        else
        {
            Scene.FieldObjects = 0;
        }

        //ItemsOnFloor
        int itemsOnFloor = UnityEngine.GC.rand.rnd.Next(0, 10); //10%
        if (itemsOnFloor == 10)
        {
            EScene.ItemsOnFloor = 1;
        }
        else
        {
            EScene.ItemsOnFloor = 0;
        }

        //Reward
        rewardOccurence = UnityEngine.GC.rand.rnd.Next(0, rewardOccurence); //50%
        if (rewardOccurence >= 1)
        {
            EScene.Reward = true;
        }
        else
        {
            EScene.Reward = false;
        }

        //FieldObjects
        int objectCount = UnityEngine.GC.rand.rnd.Next(0, objectCountMax);
        EScene.FieldObjects = objectCount;

        return EScene;
    }
    */
    
}