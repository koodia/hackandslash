using UnityEngine;
using System.Collections;

public class SpawnArea  {


    public int WidthX { get; set; }
    public int HeightY { get; set; }
    public int DeptZ { get; set; }
    public int Size { get; set; }

    private const int SPAWN_HEIGHT = 1;

    private const int SPAWN_SIDE_DEPT = 20;
    private const int SPAWN_SIDE_LENGTH = 100;

    private const int SPAWN_MID_DEPT = 70;
    private const int SPAWN_MID_LENGTH = 60;

    private const int SPAWN_OPPOSITE_AND_BEHIND_LENGTH = 20;
    private const int SPAWN_OPPOSITE_AND_BEHIND_DEPT = 70;

    // Use this for initialization
    void Start() {

    }

    //spawn location
    public void SetSPAWNAreaSize(string spawnType, int size)
    {
        switch (spawnType)
        {
            case "top":
                this.DeptZ = SPAWN_MID_DEPT * size;
                this.HeightY = SPAWN_HEIGHT;
                this.WidthX = SPAWN_SIDE_LENGTH * size;
                this.Size = size;
                break;
            case "bottom":
                this.DeptZ = SPAWN_SIDE_DEPT * size;
                this.HeightY = SPAWN_HEIGHT;
                this.WidthX = SPAWN_SIDE_LENGTH * size;
                this.Size = size;
                break;
            case "middle":
                this.DeptZ = SPAWN_MID_DEPT * size;
                this.HeightY = SPAWN_HEIGHT;
                this.WidthX = SPAWN_MID_LENGTH * size;
                this.Size = size;
                break;
            case "behind":
                this.DeptZ = SPAWN_OPPOSITE_AND_BEHIND_DEPT * size;
                this.HeightY = SPAWN_HEIGHT;
                this.WidthX = SPAWN_OPPOSITE_AND_BEHIND_LENGTH * size;
                this.Size = size;
                break;
            case "opposite":
                this.DeptZ = SPAWN_OPPOSITE_AND_BEHIND_DEPT * size;
                this.HeightY = SPAWN_HEIGHT;
                this.WidthX = SPAWN_OPPOSITE_AND_BEHIND_LENGTH * size;
                this.Size = size;
                break;

            default:
                Debug.Log("ERROR, DetermineBattlefieldSize ended in the default case. Size or Type is faulty: size:" + size + ". spawnType:" + spawnType);
                break;
        }

    }
}