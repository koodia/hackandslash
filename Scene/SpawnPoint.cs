using UnityEngine;
using System.Collections;

public class SpawnPoint {

    public bool IsActive { get; set; }
    public Vector3 Scale { get; set; }
    public string SpawnType { get; set; }
    public string Position { get; set; }

    //DUPLIKAATTEJA:
    //private const int BF_BORDERS_MAX_X = 1140;
    //private const int BF_BORDERS_MIN_X = 0;
    //private const int BF_BORDERS_MAX_Y = 300;
    //private const int BF_BORDERS_MAX_Z = 660;
    //private const int BF_BORDERS_MIN_Z = 0;
    private const int BF_BORDERS_MAX_Y = 2;

   
    private const int SPAWNPOINT_MID_LENGTH = 60;
    private const int SPAWNPOINT_MID_DEPTH = 60;
    private const int SPAWNPOINT_SIDE_LENGTH = 70;
    private const int SPAWNPOINT_OPPOSITE_AND_BEHIND_LENGTH = 20;
    private const int SPAWNPOINT_SIDE_DEPTH = 20;
   

    public SpawnPoint()
    {
        IsActive = true;
        Scale = new Vector3(0, 0, 0);
        SpawnType = null;
        Position = null;
    }


    


    /// <summary>
    /// 
    /// </summary>
    /// <param name="size"></param>
    /// <param name="position">top,bot,mid,behind,opposite</param>
    /// <returns></returns>
    public static SpawnPoint BuildSpawnSizeOf(int size, string position)
    {
        SpawnPoint newSpawn = new SpawnPoint();
        switch (position)
        {
            case "top":
            case "bot":
                newSpawn.Scale = new Vector3(SPAWNPOINT_SIDE_LENGTH * size, BF_BORDERS_MAX_Y , SPAWNPOINT_SIDE_DEPTH);
                break;
            case "opposite":
            case "behind":
                newSpawn.Scale = new Vector3(SPAWNPOINT_OPPOSITE_AND_BEHIND_LENGTH, BF_BORDERS_MAX_Y , SPAWNPOINT_MID_DEPTH);
                break;
            case "mid":
                newSpawn.Scale = new Vector3(SPAWNPOINT_MID_LENGTH * size, BF_BORDERS_MAX_Y, SPAWNPOINT_MID_DEPTH);
                break;
        }

        return newSpawn;
    }
	
}
