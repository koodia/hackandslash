using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Assertions;

public static class BFMaker
{

    public const int BF_BORDERS_MAX_X = 120;
    private const int BF_BORDERS_MIN_X = 0;
    public const int BF_BORDERS_MAX_Y = 2;
    //private const int BF_BORDERS_MIN_Y = 2;
    public const int BF_BORDERS_MAX_Z = 70;
    private const int BF_BORDERS_MIN_Z = 0;

    public static int LimitMonsterCountMaxByBattlefieldSize(int count)
    {
        int max = 0;
        
        switch(count)
        {
            case 1:
                max = 10;
                break;
            case 2:
                max = 15;
                break;
            case 3:
                max = 30;
                break;
            case 4:
                max = 35;
                break;
            default :
                throw new Exception("FATAL ERROR. number of creatures is not supported. Count:" + count);
        }
        return max;
    }

    public static Stage BuildBattlefieldSizeOf(int size, StageSize stageSize)
    {
        Assert.IsTrue(size != 0,"Error, size cannot be 0");

        //hax at the moment
        if (stageSize != StageSize.NotSet)
        {

            if (stageSize == StageSize.Random)
            {
                size = GC.rand.rnd.Next(1, 5);
            }
            else
            {
                size = (int)stageSize;
            }
            
        }

        Debug.Log("Size of stage is:" + size);

        Stage bf = new Stage();
        bf.Size = size;
        bf.stageSize = stageSize;
        bf.SpawnTop = SpawnPoint.BuildSpawnSizeOf(size, "top");
        bf.SpawnBot = SpawnPoint.BuildSpawnSizeOf(size, "bot");
        bf.SpawnMid = SpawnPoint.BuildSpawnSizeOf(size, "mid");
        bf.SpawnOpposite = SpawnPoint.BuildSpawnSizeOf(size, "opposite");
        bf.SpawnBehind = SpawnPoint.BuildSpawnSizeOf(size, "behind");
        bf.SetScale(new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y, BF_BORDERS_MAX_Z));
        bf.SummonZone = new Vector3(20, 5, 0);
        bf.MonsterCountMax = BFMaker.LimitMonsterCountMaxByBattlefieldSize(size);


        //bf.FinishLine = (BF_BORDERS_MAX_X * size) - 20; // Ota renderista...
        //bf.EndZone = new Vector3(20, BF_BORDERS_MAX_Y, 60); 
        //bf.WalkingArea      
        /*
        switch (size)
        {
            case 1:
                bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                bf.Size = size;
                break;
            case 2:
                bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                bf.Size = size;
                break;
            case 3:
                bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                bf.Size = size;
                break;
            case 4:
                bf.Scale = new Vector3(BF_BORDERS_MAX_X * size, BF_BORDERS_MAX_Y * size, BF_BORDERS_MAX_Z * size);
                bf.Size = size;
                break;

            default:
                Debug.Log("ERROR, DetermineBattlefieldSize ended in the default case. Size is set to:" + size);
                break;     
    }
    */
        return bf;
    }
}
