using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class GameObjectSpawner : SpawnerHelper, IBaseSpawner
{
    void Start()
    {
        GameObject poolObject = GameObject.Find("GameObjectPooler");
        pooler = poolObject.GetComponent<ObjectPooler>();
    }

    public void CreateGameObjects<T>(List<T> objects)
    {
        List<Creature> things = objects.Select(o => o as Creature).ToList();
        for (int ii = 0; ii < objects.Count; ii++)
        {
            SpawnGameObject(things[ii], SpawnLocation.Random); //Just testing
        }
    }

    //temporary
    public void CreateItemObjects<T>(List<T> objects)
    {
        List<Item> things = objects.Select(o => o as Item).ToList();
        for (int ii = 0; ii < objects.Count; ii++)
        {
            //GC.GetCntr().Scene.Stage.SpawnMid.Scale.x
            SpawnGameObject(things[ii], SpawnLocation.Mid); //Just testing

            //IntantianteObjectToTargetSpawnArea(SpawnLocation.Mid, things[ii]);
        }
    }


    /// <summary>
    /// TODO: Requires attention! Might have a bug and logic issues
    /// </summary>
    /// <param name="personality"></param>
    /// <param name="spawnLocation"></param>
    //private void SpawnCreature(CreatureType type, CreatureCategory category, CreatureLvl lvl, SpawnLocation spawnLocation, CreatureTags attack)
    public void SpawnGameObject(BaseObject personality, SpawnLocation spawnLocation)
    {
        //if (personality == null)
        //{
        //    throw new ArgumentNullException("personality");
        //}

        //SpawnLocation will be overwritten if the value is set to Random
        if (spawnLocation == SpawnLocation.Random)
        {
            spawnLocation = ScatterGameObjectSpawnLocation(personality, spawnLocation);
        }

        if (personality.ID == 0)
        {
            Debug.Log("Why is personality null?");
        }

        int freeId = 0;
        if (personality.ID != 0)
        {
            // personality.ID = pooler.ReuseUnactiveGO(personality.PrefabName); //toimiiko? Grab freeId?
            freeId = pooler.ReuseUnactiveGO(personality.PrefabName);
        }
       

        if (freeId == 0) //Could not find a free object, so Instantiate new one
        {
            IntantianteObjectToTargetSpawnArea(spawnLocation, personality);
            //IntantianteObjectToTargetSpawnArea(obj, type, category, pos, lvl);
        }
        else  //Lets create new one;
        {
            ReusePoolObjectAndPutToTargetSpawnArea(freeId, spawnLocation, personality);
        }

        //Debug.Log("*** Creature added to the scene! ***");
    }




    public SpawnLocation  ScatterGameObjectSpawnLocation(BaseObject personality, SpawnLocation spawnLocation)
    {
        SpawnLocation newLocation = SpawnLocation.Notset; //default
        //Tarvitsee Battlefield parametrin analysoimiseen
        int spot = Mathf.RoundToInt(GC.rand.rnd.Next(1, 11));
       
        //Obstacles should be created to the mid only
        //TODO

        //Probabilities:
        switch (spot)
        {
            case 1:
            case 2:
                newLocation = SpawnLocation.Mid;
                break;
            case 3:
            case 4:
            case 5:
                newLocation = SpawnLocation.Opposite;
                break;
            case 6:
            case 7:
                newLocation = SpawnLocation.Top;
                break;
            case 8:
            case 9:
                newLocation = SpawnLocation.Bot;
                break;
            case 10:
                newLocation = SpawnLocation.Opposite;
                break;
            case 11:
                newLocation = SpawnLocation.Behind;
                break;
        }
 
        if ( newLocation == SpawnLocation.Notset)
        {
            throw new Exception("FATAL ERROR");
        }

        return newLocation;
    }


    public void ReusePoolObjectAndPutToTargetSpawnArea(int creaturePoolId, SpawnLocation point, BaseObject newPersonality)
    {

        GameObject targetSpawn = GameObject.Find("Spawn" + point.ToString());
        if (targetSpawn == null)
        {
            Debug.LogWarning("Could not find target spawn point");
            throw new System.Exception("Error, could not find target spawn point!");
        }
       
        Vector3 newPos = new Vector3();
        //TESTIA:
        newPos = ChangePos(targetSpawn);//Just temporary line

        newPersonality.OverwriteOldValues(newPersonality);
        pooler.ReactivateObjectById(creaturePoolId, targetSpawn, newPos);
    }

}
