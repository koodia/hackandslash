using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public enum CameraPositions
{
    NotSet
    ,Top
    ,Custom
    ,Random
    ,Side
    ,Intimate

}

[System.Serializable]
public class Stage
{
    //Max value is Battlefield's plane.render.bound.max.x MINUS midspawn's render.bound.max.x (in theory)
    private float spawnMidPlaneDefaultPosition = 0;

    [SerializeField]
    public int MonsterCountMax; // { get; set; }
    [SerializeField]
    public int Size; //999 means custom

    [SerializeField]
    public StageSize stageSize;

    [SerializeField]
    private Vector3 scale; //{ get; set; }


    public Vector3 GetScale()
    {
        return scale;
    }

    [SerializeField]
    float StageWidth;

    [SerializeField]
    float StageDepth;


    public void SetScale(Vector3 newScale)
    {
        scale = newScale;
    }

    [SerializeField]
    public SpawnPoint SpawnTop; //{ get; set; }
    [SerializeField]
    public SpawnPoint SpawnBot; //{ get; set; }
    [SerializeField]
    public SpawnPoint SpawnOpposite; //{ get; set; }
    [SerializeField]
    public SpawnPoint SpawnBehind; //{ get; set; }
    [SerializeField]
    public SpawnPoint SpawnMid; //{ get; set; }
    [SerializeField]
    public Vector3 SummonZone; //{ get; set; }
    [SerializeField]
    public float FinishLine; //{ get; set; }

    //[Range(40.0f, 90.0f)] Don't limit here at the moment. CameraHeightSlider has this range annotation on
    [SerializeField]
    public float CameraAngle; //{ get; set; }

    //[Range(380.0f, 800.0f)] Don't limit here at the moment. CameraHeightSlider has this range annotation on
    [SerializeField]
    public float CameraDistance; //{ get; set; }

    [SerializeField]
    public CameraPositions cameraPosition;

    //Can only be used when stage size is 1(Tiny) or CameraPosition is set to Spesific. Maybe 2 later?. Recommended to use only with manually created scene objects
    public bool cameraLock;

    //public Vector3 EndZone { get; set; }
    //private Battlefield battlefield;
    GameObject gameObject;

    public Stage() //Default constructor should not be used
    {
       
    }

    private void BattlefieldInit()
    {
        //this.gameObject = GameObject.Find("Battlefield") as GameObject;
        //if (gameObject == null)
        //{ Debug.LogWarning("Error, could not find target Battlefield object!"); }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="battleFieldLength">1 to 4 </param>
    public Stage(int size)
    {

        // this.FinishLine = 0; //Needs to kept as zero so scene won't change
        ////BattlefieldInit();

        // SetBattlefieldSize(size);
        // battlefield = new Battlefield();
        //Battlefield bfWithSize = new Battlefield(1);
        //bfWithSize = BuildBattlefieldSizeOf(1);
        //battlefield = bfWithSize;


        this.FinishLine = 0; //Needs to kept as zero so scene won't change
        //BattlefieldInit();
        this.CameraAngle = Globals_Stage.CAMERA_CAMERA_ANGLE_MIN;
        this.CameraDistance = Globals_Stage.CAMERA_MIN_DISTANCE_MAX;
        this.cameraPosition = CameraPositions.NotSet;
        //this.stageSize = StageSize.NotSet;

        //SetBattlefieldSize(size); //Ei tee oikein mitään
    }

    public bool IsObjectInsideBattlefield()
    {

        return false;
    }

    ////Adjustment screen thingi
    //public bool IsEndReached(Transform trans)
    //{
    //    if (trans.position.x < Globals_Stage.BF_BORDERS_MIN_X)
    //    {
    //        //Seuraava scene tietää mihin pelaaja siirretään
    //        Debug.Log("You hit the scene changer wall!. Loading next scene!");
    //        return true;
    //    }
    //    return false;
    //}


    ////Adjustment screen thingi
    //public void SetLargerBattlefield()
    //{
    //    if (Size >= 4)
    //    {
    //        Debug.Log("Battlefield is the maximum size");
    //    }
    //    else
    //    {
    //        SetBattlefieldSize(this.Size + 1);
    //    }
    //}

    ////Mostly for debugging
    //public void SetSmallerBattlefield()
    //{
    //    if (Size <= 1)
    //    {
    //        Debug.Log("Battlefield is the minimum size");
    //    }
    //    else
    //    {
    //        SetBattlefieldSize(this.Size - 1);
    //    }
    //}

   

    public void SetCameraPosition(CameraPositions cameraPos)
    {
        this.cameraPosition = cameraPos;

        switch (cameraPos)
        {
            //case CameraPositions.Default:
            //    //TODO
            //    this.CameraAngle = Globals_Stage.CAMERA_CAMERA_ANGLE_MIN;
            //    this.CameraMinDistance = Globals_Stage.CAMERA_MIN_DISTANCE_MIN;
            //    break;
            case CameraPositions.Random:
                this.CameraAngle = GC.rand.rnd.Next((int)Globals_Stage.CAMERA_CAMERA_ANGLE_MIN,(int) Globals_Stage.CAMERA_CAMERA_ANGLE_MAX);
                this.CameraDistance = GC.rand.rnd.Next((int)Globals_Stage.CAMERA_MIN_DISTANCE_MIN, (int)Globals_Stage.CAMERA_MIN_DISTANCE_MAX);
                break;
            case CameraPositions.Top:
                this.CameraAngle =  Globals_Stage.CAMERA_CAMERA_ANGLE_MAX;
                this.CameraDistance = Globals_Stage.CAMERA_MIN_DISTANCE_MAX;
                break;
            case CameraPositions.Side:
                //TODO
                this.CameraAngle = Globals_Stage.CAMERA_CAMERA_ANGLE_MIN;
                this.CameraDistance = Globals_Stage.CAMERA_MIN_DISTANCE_MAX / 2;
            break;
            case CameraPositions.Intimate:
                //TODO
                this.CameraAngle = Globals_Stage.CAMERA_CAMERA_ANGLE_MIN;
                this.CameraDistance = Globals_Stage.CAMERA_MIN_DISTANCE_MIN;
                break;
            case CameraPositions.Custom:
                //TODO
                this.CameraAngle = 1; //anything but not negatives
                this.CameraDistance = 1; //anything
                break;

            case CameraPositions.NotSet:
                //TODO
                Debug.Log("Error!, CameraPosition is NotSet! Something went wrong"); //TODO: Tee oma exception!
                break;
        }
    }

    //public void SetCameraPositionTesti(Stage stage)
    //{

    //    if (stage.cameraPosition != CameraPositions.Custom)
    //    {
    //        Debug.Log("Error!, CameraPosition is Spesific?!");
    //    }

    //    this.CameraAngle = stage.CameraAngle; //Nää pitää tuoda jostain!!
    //    this.CameraDistance = stage.CameraAngle; //Nää pitää tuoda jostain!!
    //}






    public void SetBattlefieldSpawnPoints(bool top, bool bot, bool mid, bool opposite, bool behind)
    {
        if ((top == false) &&
            (mid == false) &&
            (bot == false) &&
            (opposite == false) &&
            (behind == false))
        {
            throw new UnityException("ERROR, All spawnpoints are false");
        }

        if (top)
        this.SpawnTop.IsActive = true;
        if (mid)
            this.SpawnMid.IsActive = true;
        if (bot)
            this.SpawnBot.IsActive = true;
        if (opposite)
            this.SpawnBehind.IsActive = true;
        if (behind)
            this.SpawnOpposite.IsActive = true;
    }

    /*
     public static BuildSpawnPoint(int size)
     {
        Battlefield bf = new Battlefield();
        bf.spawnTop = SpawnPoint.BuildSpawnSizeOf(size, "top");
        bf.spawnBot = SpawnPoint.BuildSpawnSizeOf(size, "bot");
        bf.spawnMid = SpawnPoint.BuildSpawnSizeOf(size, "mid");
        bf.spawnOpposite = SpawnPoint.BuildSpawnSizeOf(size, "opposite");
        bf.spawnBehind = SpawnPoint.BuildSpawnSizeOf(size, "behind");
        bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);

    }
    */



    //TODO: Needs work
    public void RecalcFinishLine()
    {
        GameObject bf = GameObject.Find("Battlefield") as GameObject;
        if (bf == null)
        { Debug.LogWarning("Error, could not find target Battlefield object!"); }


        Renderer renderer = bf.GetComponentInChildren<Renderer>() as Renderer;
        if (renderer == null)
        {
            throw new UnityException("Error, Could not find renderer from Battlefield");
        }

        Transform finishline = GC.GetCntr().FinishLine.transform;

        //The pink plane area
        Transform bfPlane = bf.transform.GetChild(0); 
        //TODO: HARDCODED Finishline width!
        finishline.position = new Vector3(renderer.bounds.max.x - 50, finishline.position.y, finishline.position.z);

        //Set Z scale:
        // finishline.localScale.Set(finishline.localScale.x, finishline.localScale.y, renderer.bounds.max.z);

        Debug.Log("new FinishLine repositioned to: " + finishline.position.x);
    }

    public void UpdateSpawnScale()
    {
        GameObject top = GameObject.Find("SpawnTop");
        if (top == null)
        {
            Debug.Log("Error, could not find target spawn point!: " + "SpawnTop");
        }
        else
        {
            Transform topPlane = top.GetComponent(typeof(Transform)) as Transform;
            if (topPlane != null)
                topPlane.transform.localScale = new Vector3(this.SpawnTop.Scale.x, this.SpawnTop.Scale.y, this.SpawnTop.Scale.z);
        }

        GameObject mid = GameObject.Find("SpawnMid");
        if (mid == null)
        {
            Debug.Log("Error, could not find target spawn point!: " + "SpawnMid");
        }
        else
        {
            Transform midPlane = mid.GetComponent(typeof(Transform)) as Transform;
            if (midPlane != null)
                midPlane.transform.localScale = new Vector3(this.SpawnMid.Scale.x, this.SpawnMid.Scale.y, this.SpawnMid.Scale.z);
        }

        GameObject bot = GameObject.Find("SpawnBot");
        if (bot == null)
        {
            Debug.Log("Error, could not find target spawn point!: " + "SpawnBot");
        }
        else
        {
            Transform botPlane = bot.GetComponent(typeof(Transform)) as Transform;
            if (botPlane != null)
                botPlane.transform.localScale = new Vector3(this.SpawnBot.Scale.x, this.SpawnBot.Scale.y, this.SpawnBot.Scale.z);
        }

        GameObject behind = GameObject.Find("SpawnBehind");
        if (behind == null)
        {
            Debug.Log("Error, could not find target spawn point!: " + "SpawnBehind");
        }
        else
        {
            Transform behindPlane = behind.GetComponent(typeof(Transform)) as Transform;
            if (behindPlane != null)
                behindPlane.transform.localScale = new Vector3(this.SpawnBehind.Scale.x, this.SpawnBehind.Scale.y, this.SpawnBehind.Scale.z);
        }

        GameObject opposite = GameObject.Find("SpawnOpposite");
        if (opposite == null)
        {
            Debug.Log("Error, could not find target spawn point!: " + "SpawnOpposite");
        }
        else
        {
            Transform oppositePlane = opposite.GetComponent(typeof(Transform)) as Transform;
            if (oppositePlane != null)
                oppositePlane.transform.localScale = new Vector3(this.SpawnOpposite.Scale.x, this.SpawnOpposite.Scale.y, this.SpawnOpposite.Scale.z);
        }
    }


    public void RepositionAllSpawnPositions()
    {
        GameObject bf = GameObject.Find("Battlefield");
        GameObject behind = GameObject.Find("SpawnBehind");
        GameObject opposite = GameObject.Find("SpawnOpposite");
        GameObject mid = GameObject.Find("SpawnMid");

        GameObject top = GameObject.Find("SpawnTop");
        GameObject bot = GameObject.Find("SpawnBot");

        if (bf == null)
        {
            Debug.Log("Error, could not find target battlefield game object!");
        }
        else
        {
            Renderer bfRenderer = bf.GetComponentInChildren<Renderer>();

            Transform behindPlane = behind.GetComponent<Transform>();
            Renderer behindRenderer = behind.GetComponent<Renderer>();
            behindPlane.position = new Vector3(0, behindPlane.position.y, behindPlane.position.z);
             behindPlane.position = new Vector3(bfRenderer.bounds.min.x - behindRenderer.bounds.max.x, behindPlane.position.y, behindPlane.position.z);

            Transform oppositePlane = opposite.GetComponent<Transform>();
            Renderer oppositeRenderer = opposite.GetComponent<Renderer>();
            oppositePlane.position = new Vector3(0, oppositePlane.position.y, oppositePlane.position.z);
            oppositePlane.position = new Vector3(bfRenderer.bounds.max.x + oppositeRenderer.bounds.max.x, oppositePlane.position.y, oppositePlane.position.z);

            if (top != null)
            {
                Transform topPlane = top.GetComponent<Transform>();
                topPlane.position = new Vector3(bfRenderer.bounds.max.x / 2, topPlane.position.y, topPlane.position.z);
            }
            if (bot != null)
            {
                Transform botPlane = bot.GetComponent<Transform>();
                botPlane.position = new Vector3(bfRenderer.bounds.max.x / 2, botPlane.position.y, botPlane.position.z);
            }

            if (spawnMidPlaneDefaultPosition != 0)
            { spawnMidPlaneDefaultPosition = (spawnMidPlaneDefaultPosition / bfRenderer.bounds.max.x); }

            Transform midPlane = mid.GetComponent<Transform>();
            Renderer midRenderer = mid.GetComponent<Renderer>();
            midPlane.position = new Vector3(bfRenderer.bounds.max.x / 2 + spawnMidPlaneDefaultPosition, midPlane.position.y, midPlane.position.z);
           
        }
    }

   /// <summary>
   /// Important! Call this in the ActiveStuff method. 
   /// </summary>
    public void UpdateBattlefieldScale()
    {
        GameObject bf = GameObject.Find("Battlefield");
        Transform bfPlane = bf.transform.GetChild(0);
        if (bfPlane == null)
        {
            Debug.Log("Error, could not find target battlefield game object!");
        }
        else
        {
            if (bfPlane != null)
            {
                bfPlane.transform.localScale = new Vector3(this.scale.x, this.scale.y, this.scale.z);
            }
        }
    }

    public void UpdateBattlefieldSize()
    {
        //UpdateSpawnScale(); //OK kait
        //UpdateBattlefieldScale(); //Ei toimi? 
        //RepositionBattleField();
       // RepositionAllSpawnPositions();
    }


    public void RescaleCustomSizeStage(int creatureCap)
    {
        this.Size = 999;
        //  this.stageSize = StageSize.Custom;

        // Spawns need to be scaled manually in the editor
        //this.SpawnTop = SpawnPoint.BuildSpawnSizeOf(newSize, "top");
        //this.SpawnBot = SpawnPoint.BuildSpawnSizeOf(newSize, "bot");
        //this.SpawnMid = SpawnPoint.BuildSpawnSizeOf(newSize, "mid");
        //this.SpawnOpposite = SpawnPoint.BuildSpawnSizeOf(newSize, "opposite");
        //this.SpawnBehind = SpawnPoint.BuildSpawnSizeOf(newSize, "behind");

        GameObject bf = GameObject.Find("Battlefield");
        if (bf == null)
        {
            Debug.Log("Error, could not find target battlefield game object!");
        }
        else
        {
            Renderer bfRenderer = bf.GetComponentInChildren<Renderer>();
            StageWidth = bfRenderer.bounds.max.x;
            StageDepth = bfRenderer.bounds.max.z;
        }

      //  this.SummonZone = new Vector3(20, 5, 0);
        this.MonsterCountMax = BFMaker.LimitMonsterCountMaxByBattlefieldSize(creatureCap);

    }

    public  void RescaleStage(StageSize newStageSize)
    {
        int newSize = 0;
         Assert.IsTrue(newStageSize != StageSize.NotSet, "Error, newStageSize cannot be 0");

        if (newStageSize == StageSize.Random)
        {
            newSize = GC.rand.rnd.Next(1, 5);
        }
        else
        {
            newSize = (int)newStageSize;
        }

        this.Size = newSize;
        this.stageSize = newStageSize;
        this.SpawnTop = SpawnPoint.BuildSpawnSizeOf(newSize, "top");
        this.SpawnBot = SpawnPoint.BuildSpawnSizeOf(newSize, "bot");
        this.SpawnMid = SpawnPoint.BuildSpawnSizeOf(newSize, "mid");
        this.SpawnOpposite = SpawnPoint.BuildSpawnSizeOf(newSize, "opposite");
        this.SpawnBehind = SpawnPoint.BuildSpawnSizeOf(newSize, "behind");
        this.SetScale( new Vector3(BFMaker.BF_BORDERS_MAX_X * newSize, BFMaker.BF_BORDERS_MAX_Y, BFMaker.BF_BORDERS_MAX_Z));
        this.SummonZone = new Vector3(20, 5, 700/2); //notize z-value, currently hardcoded middle of the screen
        this.MonsterCountMax = BFMaker.LimitMonsterCountMaxByBattlefieldSize(newSize);
    }

    public void RepositionBattleField()
    {
        GameObject bf = GameObject.Find("Battlefield");
        Renderer bfRenderer = bf.GetComponentInChildren<Renderer>();
        Transform bfPlane =  bf.transform.GetChild(0);
        //WTF, kysy tästä
        bfPlane.transform.position = new Vector3(0, bfPlane.transform.position.y, bfPlane.transform.position.z);
        bfPlane.transform.position = new Vector3(bfRenderer.bounds.max.x, bfPlane.transform.position.y, bfPlane.transform.position.z);
    }

    //TOinen:http://answers.unity3d.com/questions/7788/closest-point-on-mesh-collider.html
    //KATO: http://answers.unity3d.com/questions/424974/nearest-point-on-mesh.html
    //Vain esimerkkinä:
    //Transform GetClosestEnemy(Transform[] enemies)
    //{
    //    Transform tMin = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 currentPos = transform.position;
    //    foreach (Transform t in enemies)
    //    {
    //        float dist = Vector3.Distance(t.position, currentPos);
    //        if (dist < minDist)
    //        {
    //            tMin = t;
    //            minDist = dist;
    //        }
    //    }
    //    return tMin;
    //}

    public float GetClosestPlaneDistance(Transform spawn, Transform target)
    {
        float dist = 0;
        if (spawn)
        {
            //GameObject bf = target;
            if (spawn == null || target == null)
            {
                Debug.Log("Error, could not find target battlefield game object!");
            }
            else
            {
                dist = Vector3.Distance(spawn.position, spawn.transform.position);
               // RepositionAllSpawnPositions(spawn, dist);
            }

            Debug.Log("Distance to other: " + dist);
       
        }
        return dist;
    }

    //Transform GetClosestPlaneDistance(Transform plane)
    //{

    //    GameObject bf = GameObject.Find("Battlefield");
    //    if (bf == null)
    //    {
    //        Debug.Log("Error, could not find target battlefield game object!");
    //    }
    //    else
    //    {
    //        Transform bfPlane = bf.GetComponent(typeof(Transform)) as Transform;
    //        if (bfPlane != null)
    //            bfPlane.transform.position = new Vector3(0, 0, 0);
    //    }


    //    Transform tMin = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 currentPos = transform.position;

    //    float dist = Vector3.Distance(plane.position, currentPos);
    //    if (dist < minDist)
    //    {
    //        tMin = plane;
    //        minDist = dist;
    //    }
    //    return tMin;
    //}


   


    //public Battlefield BuildBattlefieldSizeOf(int size)
    //{
    //    Battlefield bf = new Battlefield();
    //    bf.Size = size;
    //    bf.SpawnTop = SpawnPoint.BuildSpawnSizeOf(size, "top");
    //    bf.SpawnBot = SpawnPoint.BuildSpawnSizeOf(size, "bot");
    //    bf.SpawnMid = SpawnPoint.BuildSpawnSizeOf(size, "mid");
    //    bf.SpawnOpposite = SpawnPoint.BuildSpawnSizeOf(size, "opposite");
    //    bf.SpawnBehind  = SpawnPoint.BuildSpawnSizeOf(size, "behind");
    //    bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y, BF_BORDERS_MAX_Z);
    //    bf.SummonZone = new Vector3(20, 5, 0);
    //    bf.MonsterCountMax = BFMaker.LimitMonsterCountMaxAccordingBattlefieldSize(size);


    //    //bf.FinishLine = (BF_BORDERS_MAX_X * size) - 20; // Ota renderista...
    //    //bf.EndZone = new Vector3(20, BF_BORDERS_MAX_Y, 60); 
    //    //bf.WalkingArea      
    //    /*
    //    switch (size)
    //    {
    //        case 1:
    //            bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
    //            bf.Size = size;
    //            break;
    //        case 2:
    //            bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
    //            bf.Size = size;
    //            break;
    //        case 3:
    //            bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
    //            bf.Size = size;
    //            break;
    //        case 4:
    //            bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
    //            bf.Size = size;
    //            break;

    //        default:
    //            Debug.Log("ERROR, DetermineBattlefieldSize ended in the default case. Size is set to:" + size);
    //            break;     
    //}
    //*/
    //    return bf;
    //}

    public void SetBattlefieldSize(int size)
    {

     
        this.Size = size;
        this.SpawnTop = SpawnPoint.BuildSpawnSizeOf(size, "top");
        this.SpawnBot = SpawnPoint.BuildSpawnSizeOf(size, "bot");
        this.SpawnMid = SpawnPoint.BuildSpawnSizeOf(size, "mid");
        this.SpawnOpposite = SpawnPoint.BuildSpawnSizeOf(size, "opposite");
        this.SpawnBehind = SpawnPoint.BuildSpawnSizeOf(size, "behind");
        this.SetScale(new Vector3(Globals_Stage.BF_BORDERS_MAX_X * size, Globals_Stage.BF_BORDERS_MAX_Y, Globals_Stage.BF_BORDERS_MAX_Z)); //TODO MUISTAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA OTTTAAAAAAAAA
        this.SummonZone = new Vector3(20, 5, 0);
        //this.FinishLine = (BF_BORDERS_MAX_X * size) - 20;
        //bf.EndZone = new Vector3(20, BF_BORDERS_MAX_Y, 60); 



        /*
        switch (size)
        {
            case 1:
                this.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                this.Size = size;
                break;
            case 2:
                this.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                this.Size = size;
                break;
            case 3:
                this.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                this.Size = size;
                break;
            case 4:
                this.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                this.Size = size;
                break;

            default:
                Debug.Log("ERROR, DetermineBattlefieldSize ended in the default case. Size is set to:" + size);
                break;
        }
        */
    }
}
