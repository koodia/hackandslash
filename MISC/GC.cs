using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System;


/// <summary>
/// Game Controller
/// </summary>
public class GC : MonoBehaviour
{
    //The only random in the game
    public static Rand rand = new Rand();
    public static List<BadEncounter> blackListTxt;
    private static GC cntr;
    private static GameData gameData; //manager?
    private ScoreData score;   //manager?
    private JourneyData journeyData; //manager?
    private List<PlayerData> playerData;
    //Current ongoing scene
    [SerializeField]
    private Scene scene;
    private Scene previousScene;
    // Scene and content that will be loaded next
    private Scene nextScene;
    [SerializeField]
    private Level level;
    GameObject mainCamera;
    CameraHeightSlider cameraSlider;
    //GameObject player1;
    //public List<PlayerData> playerList;
    public GenericCharacter[] players;
    public string loadLevel;
    private bool loadingScene;
    public SoundManager soundManager { get; set; }
    public GameObjectSpawner gameObjectSpawner;
    public GameObject FinishLine = null;
    public GameObject poolObject;
    public ObjectPooler pool;
   
    public bool temporaryVariableThatHenryHasToRemoveLater;
    public bool sceneSwitchAllowed = false;
    public delegate void MyDelegate();
    public MyDelegate sceneSwitchAction;


    private LimitMovementArea movementArea; //player 1's movementAreaLimiter

    #region Properties

    public static GC GetCntr()
    {
        return cntr;

    }

    public static GameData GetGameData()
    {
        return gameData;
    }


    public ScoreData Score
    {
        get { return score; }
        set { score = value; }
    }

    public ScoreData ScoreData
    {
        get { return score; }
        set { score = value; }
    }

    public JourneyData JourneyData
    {
        get { return journeyData; }
        set { journeyData = value; }
    }

    public List<PlayerData> PlayerData
    {
        get { return playerData; }
        set { playerData = value; }
    }

    public Scene Scene
    {
        get { return scene; }
        set { scene = value; }
    }

    public Level Level
    {
        get { return level; }
        set { level = value; }
    }

    public Scene PreviousScene
    {
        get { return previousScene; }
        set { previousScene = value; }
    }

    public Scene NextScene
    {
        get { return nextScene; }
        set { nextScene = value; }
    }

    #endregion

    public void InitPlayerCount(int number)
    {
        players = new GenericCharacter[number];
        players[0] = new GenericCharacter();
        players[1] = new GenericCharacter();
    }

    void Awake()
    {
        if (cntr != null)
        {
            // Lets just destroy ourselves before armageddon happes
            Destroy(this.gameObject);
            return;
        }

        // If we get here, this is the real one
        cntr = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }



    void Start()
    {
        sceneSwitchAction = Switch;
    }

    public void Switch()
    {
        GC.GetCntr().SwitchScene();
    }



    /// <summary>
    /// Lock everything so players cannot die. But dont disable stuff until the curtain has lowered
    /// </summary>
    public void FreezeAllObjects(bool flag)
    {
        pool.TimeFreezeAllObjects(flag);
    }

    public void StartGame()
    { 
        StartCoroutine(CurtainController.RiseCurtain(0));
        GameObject sceneObject = InstantiateSceneObject("Tutorial");
        FindManagers();
        SceneMaker.InitGameFirstScene();
        LoadSceneDataFromStandaloneObject(sceneObject);
        GameObject kicker = GameObject.Find("GameKicker");
        DestroyObject(kicker); //Kicker won't be needed after Tutorial scene
        SceneMaker.Init();
        FinishLine = GameObject.Find("FinishLine");
        if (FinishLine == null)
        {
            throw new NullReferenceException("FinishLine gameobject");
        }

        // ActivateScene();GameKicker will call this

        movementArea = GameObject.Find("Player 1").GetComponent<LimitMovementArea>();
        movementArea.enabled = false;
        // GameObject.Find("Button").SetActive(true); //not in use at the moment
        CodeTester.DebugSettingsOn();
        ActivateScene();
    }


    //void OnEnable()
    //{
    //    EventManager.StartListening("ActivateScene", ActivateScene);
    //}

    //void OnDisable()
    //{
    //   EventManager.StopListening("ActivateScene", ActivateScene);
    //}

    public void ActivateScene()
    {

        StartCoroutine(RescaleStageAndActivateStuff());
    }

    IEnumerator RescaleStageAndActivateStuff()
    {
        //Scale the Stage first

        //Then activate everything
        yield return new WaitForSeconds(0.5f); //Required! or battlefield wont scale
        StartCoroutine(ActivateStuff());
    }

    //TODO: activate stuff: Scene knows itself what to activate. IMPLEMENT
    public IEnumerator ActivateStuff()
    {

        if (Scene.Stage.stageSize == StageSize.Custom)
        {
           
           //Scene.Stage.RepositionAllSpawnPositions();  ...Make a custom?

            //Anything else?
        }
        else
        {
            Scene.Stage.UpdateBattlefieldScale();
            Scene.Stage.UpdateSpawnScale();
            Scene.Stage.RepositionBattleField();
            Scene.Stage.RepositionAllSpawnPositions();
        }



        yield return new WaitForSeconds(0.5f);

        soundManager.ActivateMusic(GC.GetCntr().Scene);

        StartCoroutine(CurtainController.RiseCurtain(2));
        // GC.GetCntr().Scene.Stage.UpdateBattlefieldScale();//HELVETTTI

        //Unlock players controls
        Movement movement = GameObject.Find("Cha_Knight").GetComponentInChildren<Movement>();
        yield return StartCoroutine(movement.FreeMovement(0.5f));

        //GameObject.Find("Player 1").GetComponent<LimitMovementArea>().UpdateWalkingBorders();

        //yield return new WaitForSeconds(0.5f); //Maybe useless, who knows

        //Create monsters, obstacles, items etc
        if (Scene.SceneCreatures.Count != 0) { StartCoroutine(ActivateMonsters(2.0f)); }
        if (Scene.Obstacles.Count != 0) { StartCoroutine(ActivateFieldObstacles(2.0f)); }
        // if (Scene.Items.Count != 0) { SetFieldItemsActive(); }
        SetFieldItemsActive();

        StartCoroutine(ChangeCameraAngle(0.0f, Scene.Stage));

        GameObject.Find("Player 1").GetComponent<LimitMovementArea>().UpdateWalkingBorders();

        yield return new WaitForSeconds(0.5f);

        Scene.Stage.RecalcFinishLine();

        yield return  StartCoroutine(SetPlayerPositionToBeginning(1));

        Vector3 endPos = new Vector3(100, GC.GetCntr().Scene.Stage.SummonZone.y, GC.GetCntr().Scene.Stage.SummonZone.z);
        yield return StartCoroutine(CallHerosToStage(endPos, 1.2f)); //+50x
        movementArea.enabled = true;

        //TODO_02: players run to scene
        //TODO
        pool.TimeFreezeAllObjects(false);

        yield return null;
    }

    public IEnumerator SetPlayerPositionToBeginning(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetPlayerPosition(-200);
    }

    private void SetPlayerPosition(int positionX)
    {
        GameObject target = GameObject.Find("Cha_Knight");
        if (target != null)
        {
            Vector3 vec = new Vector3(positionX, GC.GetCntr().Scene.Stage.SummonZone.y, GC.GetCntr().Scene.Stage.SummonZone.z);
            target.transform.position = vec;
        }
        Debug.Log("Hero position reset");
    }


    public IEnumerator CallHerosToStage(Vector3 endPos, float seconds)
    {
        GameObject objectToMove = GameObject.Find("Cha_Knight");
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, endPos, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = endPos;
    }

    public GameObject InstantiateSceneObject(string sceneObjectName)
    {
        if (string.IsNullOrEmpty(sceneObjectName) == true) { throw new System.NullReferenceException("Error, sceneObjectName empty!"); }

        UnityEngine.Object newObject = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SceneObjects/" + sceneObjectName + ".prefab", typeof(GameObject));
        GameObject obj = Instantiate(newObject) as GameObject;
        obj.SetActive(true);
        obj.name = sceneObjectName;
        return obj;
    }

    public void LoadSceneDataFromStandaloneObject(GameObject sceneObject)
    {
        this.Scene = sceneObject.GetComponentInChildren<StandaloneScene>().GetScene();
    
            //this.Level = .GetComponentInChildren<Level>(); //Mistä nämä?
            //this.ScoreData = .GetComponentInChildren<ScoreData>();
            //this.JourneyData = .GetComponentInChildren<JourneyData>();
    }

    public void FindManagers()
    {
        poolObject = GameObject.Find("GameObjectPooler");
        if (poolObject == null)
        {
            Debug.LogWarning("Could not find GameObjectPooler from scene");
        }
        this.pool = poolObject.GetComponent<ObjectPooler>();

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogWarning("Could not find SoundManager from scene");
        }

        gameObjectSpawner = GameObject.Find("CreatureSpawnManager").GetComponent<GameObjectSpawner>();
        if (gameObjectSpawner == null)
        {
            Debug.LogWarning("Could not find CreatureSpawnManager from scene");
        }

        if (mainCamera == null)
        {
            mainCamera = GameObject.Find("Main Camera");
            if (mainCamera == null)
            {
                Debug.LogWarning("Could not Main Camera from scene");
            }

          // cameraSlider = mainCamera.GetComponent<CameraHeightSlider>();
        }
    }

    void OnDestroy()
    {
        Debug.Log("GameController was destroyed! Why?!");
    }

    public bool IsSceneSwitchPossible()
    {
        if (loadingScene)
        {
            return this.loadingScene = false;
        }
        return false;
    }


    public void SwitchScene()
    {
       // FreezeAllObjects(true); //This is called on Finishline Script

        soundManager.FadeOutPlayingMusic(2.0f); //dont go over 2, has a bug at the moment

        //GameObject poolObject = GameObject.Find("GameObjectPooler");
        //if (poolObject == null)
        //{
        //    Debug.LogWarning("Could not find pooler object from scene");
        //}

        //ObjectPooler pool = poolObject.GetComponent<ObjectPooler>();
      //   StartCoroutine(CurtainController.LowerCurtain());
       


        //StartCoroutine(GC.GetCntr().gameObject.GetComponent<CurtainController>().LowerCurtain());
        Movement movement = GameObject.Find("Cha_Knight").GetComponent<Movement>();
        movement.MovementLock = true;

        
        movementArea.enabled = false; //Free limitations so CallHeroesToStage can be called

        //Remember to unactivate all objects so player(s) don't not hit anything when curtain is lowered
        pool.DeactivePoolObjects();

        /*
        *    Create next scene and level -SECTION
        */
        temporaryVariableThatHenryHasToRemoveLater = false; // init
        //Create new scene but lets not load it or switch it yet
        Scene freshNewScene = null;
        SceneType chosenScenario = SceneType.NotSet;
        if (GC.GetCntr().Level.IsFinalScene == true)
        {
            //Load new level
            GC.GetCntr().Level.ChangeNewLevel();
            //First scene in the list
            chosenScenario = GC.GetCntr().Level.LevelScenes[0];
            Debug.Log("Level changed");
        }
        else
        {
            //TODO: THIS WILL CHANGE. need a subclass also to check if the Scene is prefabscene (isPrefabScene)??
            chosenScenario = this.Scene.DetermineNextScene();
        }

        //TEMPORARY PART:*************************
        if (chosenScenario == SceneType.Experience ||
            chosenScenario == SceneType.Puzzle ||
            //chosenScenario == SceneType.Relax ||
            chosenScenario == SceneType.Reward ||
            chosenScenario == SceneType.Town ||
            chosenScenario == SceneType.Uncommon)
        {
            //Slip a coin to determine is the scene 1)a prefab scene or 2) created on the fly  
            //int coinSlip = GC.rand.rnd.Next(1, 4); // 25% change to get these more rare prefab scenes
            temporaryVariableThatHenryHasToRemoveLater = true; // chosenScenario.isprefab 
        }
        //****************************************
        //Store the previous in memory before overriding it
        this.PreviousScene = this.Scene;

        string upcomingSceneObjectName = PickRandomSceneObjectPrefab(chosenScenario);
        Debug.Log("Chosen scenario:" + chosenScenario + ".Chosen sceneName:" + upcomingSceneObjectName);
        if (temporaryVariableThatHenryHasToRemoveLater == true && chosenScenario != SceneType.Battle &&
            temporaryVariableThatHenryHasToRemoveLater == true && chosenScenario != SceneType.Boss)
        {
            //Load a prefabSceneObject
            GameObject sceneObject = GameObject.FindGameObjectWithTag("SceneObject") as GameObject;
            DestroyImmediate(sceneObject); //Very important to DestroyImmediately or duplicate is created... Unity gees
            GameObject newSceneObject = InstantiateSceneObject(upcomingSceneObjectName);
            Debug.Assert(newSceneObject != null, "Could not load SceneObject");
            LoadSceneDataFromStandaloneObject(newSceneObject);
        }
        else //Generate the scene using the "Battle_Scene" object as the base of battle ground
        {
            GameObject sceneObject = GameObject.FindGameObjectWithTag("SceneObject") as GameObject;
            //DestroyObject(sceneObject); 
            DestroyImmediate(sceneObject);
            //if (sceneObject.name != "Battle_Scene")
            //{
            //    DestroyObject(sceneObject);
            //}

            // WaitCurtainToCloseBefore_LoadingGeneratedSceneObject(chosenScenario, "Battle_Scene");
            GameObject correctSceneObject = InstantiateSceneObject("Battle_Scene");
            Debug.Assert(correctSceneObject != null, "Could not load SceneObject");
            GC.GetCntr().Scene = new Scene(); //null might be better?
            freshNewScene = SceneMaker.PrepareUpComingScene(chosenScenario);
            GC.GetCntr().Scene = freshNewScene;
        }

        CheckScore();

        //Final preparations, activates scene objects, triggers, settings etc
        //LoadNewSceneSettings();
       
        CodeTester.RunAllChecks();

        ActivateScene();
    }

    //HOW TO FIX:
    public IEnumerator WaitCurtainToCloseBefore_LoadingNewSceneObject(SceneType chosenScenario, string sceneObjectName)
    {
        while (CurtainController.IsCurtainUp)
        {
            yield return null;
        }
    }

    public IEnumerator WaitCurtainToCloseBefore_LoadingGeneratedSceneObject(SceneType chosenScenario, string sceneObjectName)
    {
        while (CurtainController.IsCurtainUp)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Dont leave any bugs lying here or the outcome is catastrophic
    /// </summary>
    /// <param name="chosenScenario"></param>
    /// <returns></returns>
    public string PickRandomSceneObjectPrefab(SceneType chosenScenario) //should return string
    {
        string sceneObjectName = "";

        Debug.Assert(chosenScenario != SceneType.NotSet, "error, chosenScenario is NotSet!");

        switch (chosenScenario)
        {
            case SceneType.Battle:
            case SceneType.Boss:
                                sceneObjectName = "Battle_Scene";
                                break;
            case SceneType.Puzzle:
                                int coinFlip = GC.rand.rnd.Next(1, 4);
                                if (coinFlip == 1)
                                {
                                    sceneObjectName = "Puzzle_Tower";
                                }
                                else if (coinFlip == 2)
                                {
                                    sceneObjectName = "Puzzle_TowerBehind";
                                }
                                else
                                {
                                    sceneObjectName = "Puzzle_Pyramid";
                                }
                                break;
            case SceneType.Uncommon:
                                sceneObjectName = "Uncommon_Shop";
                                break;
            case SceneType.Empty:
                                sceneObjectName = "Empty_Default";
                                break;
            case SceneType.Reward:
                                sceneObjectName = "Reward_Fountain";
                                break;
            default: throw new Exception("No such case:" +chosenScenario);
        }

        return sceneObjectName;
    }


    public IEnumerator ChangeCameraAngle(float waitingTime, Stage stage)
    {
        yield return new WaitForSeconds(waitingTime);

        //TODO:
        mainCamera.GetComponent<CameraHeightSlider>().UpdateCameraReferences();
        // ChangeAngle(stage);
        mainCamera.GetComponent<CameraHeightSlider>().ChangeCameraAngle(stage);


    }

    public IEnumerator ActivateMonsters(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        SetMonstersActive();
    }

    public IEnumerator ActivateFieldObstacles(float waitingTime)
    {
        yield return new WaitForSeconds(1);
        SetFieldObstaclesActive();
    }
 
    //public void SwitchSceneToNextScene()
    //{
    //    GC.GetCntr().PreviousScene = new Scene();
    //    //Save the current scene to previous variable
    //    GC.GetCntr().PreviousScene = GC.GetCntr().Scene;
    //    soundManager.StopCurrentMusic();
    //    soundManager.musicSource.clip = null; //Important to remove the old track(until each scene has audioData class)
    //    //Wipe the current scene
    //    GC.GetCntr().Scene = new Scene();
    //    //Set upcoming scene to be the current scene
    //    GC.GetCntr().Scene = GC.GetCntr().NextScene;
    //}

    //public void SwitchSceneToNextScenePrefab()
    //{
    //    //Save the current scene to previous variable
    //    GC.GetCntr().PreviousScene = GC.GetCntr().Scene;
    //    //Wipe the current scene
    //    GC.GetCntr().Scene = new Scene();
    //    //Set upcoming scene to be the current scene
    //    GC.GetCntr().Scene = GC.GetCntr().NextScene;
    //}

    /// <summary>
    /// Creates the scene obstacles
    /// </summary>
    void SetFieldObstaclesActive()
    {
        //  GameObjectSpawner spawnManager = GameObject.Find("CreatureSpawnManager").GetComponent<GameObjectSpawner>();
        gameObjectSpawner.CreateGameObjects(GC.GetCntr().scene.Obstacles);
    }

    /// <summary>
    /// Creates the scene items/random loot on the field
    /// </summary>
    void SetFieldItemsActive()
    {
        Test_Build_Items();//exprimental
        gameObjectSpawner.CreateItemObjects(GC.GetCntr().scene.Items);
    }


    public void Test_Build_Items()
    {
        this.scene.ItemsOnFloorCount = GC.rand.rnd.Next(1, 5);
        for (int ii = 0; ii < this.scene.ItemsOnFloorCount; ii++)
        {
            int MagicOrMeleeTag = GC.rand.rnd.Next(0, 3);
            //CreatureType type, CreatureCategory category, CreatureLvl lvl, CreatureTags attack new List<CreatureTags>() { (CreatureTags)MagicOrMeleeTag }
            Item newItem = ItemGenerator.GreateItem(ItemTypes.Weapon, "Melee"); // numerolla vielä väliä
            this.scene.Items.Add(newItem);
        }
    }




    /// <summary>
    /// Creates the scene monsters
    /// </summary>
    void SetMonstersActive()
    {
        //GameObjectSpawner spawnManager = GameObject.Find("CreatureSpawnManager").GetComponent<GameObjectSpawner>();
        gameObjectSpawner.CreateGameObjects(GC.GetCntr().Scene.SceneCreatures);
    }
	

    public void PrepareLevel()
	{
		//seuraavan levelin, nimi, teema,vaikeus, ja pituus
	}

	public void PrepareNextSceneData() //Scene scene, PlayerData data , JourneyData balance
	{
			GC.GetCntr().Scene.DetermineNextScene();
		    Debug.Log("Scene data prepared");
	}
	
	public void ExitSceneTransition()
	{
		Debug.Log("Exit from scene");
	}

    public void CheckScore()
    {
        this.JourneyData.ScenesSurvived--;
        this.Score.scenes += 1;
    }

}