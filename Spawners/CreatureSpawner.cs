//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System;
//using System.Linq;

//public class GameObjectSpawner : SpawnerHelper, IBaseSpawner
//{

//    //  public GameObject SpawnTop;
//    //  public GameObject SpawnBot;
//    // public GameObject SpawnMid;


//    GameObject poolObject;

//    //etsittävä poolit
//    //ja varmaan battlefielding spawnit?

//    // Use this for initialization
//    void Start()
//    {

//        GameObject poolObject = GameObject.Find("GameObjectPooler");
//        if (poolObject == null)
//        {
//            Debug.LogWarning("Could not find pooler object from scene");
//        }
//        else
//        {
//            pool = poolObject.GetComponent<Pools>();
//        }

//        // SpawnTop = GameObject.Find("SpawnTop");
//        // SpawnBot = GameObject.Find("SpawnBot");
//        // SpawnMid = GameObject.Find("SpawnMid");
//    }

//    void Update()
//    {

//        if (pool == null)
//        {
//            pool =     //poolObject.GetComponent<Pools>();
//        }

//        ////For testing
//        //if (Input.GetKeyUp(KeyCode.Home))
//        //{
//        //    foreach (GameObject creature in pool.creaturePool.Pool)
//        //    {
//        //        creature.SetActive(false);
//        //        break;
//        //    }
//        //}
//    }

//    public void CreateGameObjects<T>(List<T> objects)
//    {

//        //List<BaseObject> creatures = objects as List<BaseObject>;
//        List<BaseObject> creatures = objects.Select(o => o as BaseObject).ToList();
//        for (int ii = 0; ii < creatures.Count; ii++)
//        {
//            SpawnGameObject(creatures[ii], SpawnLocation.Mid);
//        }

//        //System.Type type = typeof(T);// objects.GetType().GetProperty("Item").PropertyType;
//        //if (type == typeof(Creature))
//        //{
//        //    CreateCreatures(objects);
//        //}
//        //else
//        //{
//        //    throw new UnityException("Could not objectlist type");
//        //}
//    }

//    //public void CreateCreatures<T>(List<T> objects)
//    //{
//    //    //Is any of the battlefield objects overlapping?:
//    //    //for (int ii = 0; ii < pool.creaturePool.Pool.Count; ii++)

//    //    //List<BaseObject> creatures = objects as List<BaseObject>;
//    //    List<BaseObject> creatures = objects.Select(o => o as BaseObject).ToList();
//    //    for (int ii = 0; ii < creatures.Count; ii++)
//    //    {
//    //        SpawnGameObject(creatures[ii], SpawnLocation.Mid);
//    //    }
//    //}




//    //private void SpawnGroup(List<Creature> creature, SpawnLocation point)
//    //{
//    //}

//    //private void SpawnHorde(List<Creature> creature, SpawnLocation point)
//    //{
//    //}

//    //private void SpawnMob(GameObject creature, SpawnLocation point)
//    //{
//    //    IntantianteObjectToTargetSpawnArea(creature, point);

//    //    UnityEngine.Debug.Log("*** Creature added to the scene! ***");
//    //    //TODO DO GameObject[] gos = AvailableOppositeSpawnPoints();
//    //}


//    //private void SpawnCreature(CreatureType type, CreatureCategory category, CreatureLvl lvl, SpawnLocation spawnLocation, CreatureTags attack)
//    public void SpawnGameObject(BaseObject personality, SpawnLocation spawnLocation)
//    {
//       // Creature personality = (Creature)newLook;
//        //  SpawnCreatureToTargetSpawnArea(creature, point);

//        //GameObject creature = pool.creaturePool.PickFirstUnactiveCreature( type, attack,  category,  lvl);
//        int creatureId = pool.generalPool.ReuseUnactiveGO(personality.PrefabName);
//        if (creatureId == 0) //Could not find a free object, so Instantiate new one
//        {
//            //Ota listasta luokka joka halutaan luoda
//            //TODO
//            //PickClass(name, type, category, pos, lvl)
//            //Creature newPersonality = new Cr; //CreatureMaker.PickCreaturePersonalityAndTraits(personality.CreatureType, personality.Category, personality.AttackType, personality.Lvl);
//            //Give an uniqueID:
//            //CREATURE_NUMBER++;
//            //newPersonality.ID = CREATURE_NUMBER;

//            IntantianteObjectToTargetSpawnArea(spawnLocation, personality);
//            //IntantianteObjectToTargetSpawnArea(obj, type, category, pos, lvl);
//        }
//        else  //Lets create new one;
//        {
//            //Creature newPersonality = CreatureMaker.PickCreaturePersonalityAndTraits(type,  category,  lvl,  attack);
//            //ReusePoolObjectAndPutToTargetSpawnArea((int)creatureId, spawnLocation, (Creature)personality);
//            ReusePoolObjectAndPutToTargetSpawnArea((int)creatureId, spawnLocation, personality);
//        }

//        UnityEngine.Debug.Log("*** Creature added to the scene! ***");
//    }



//    //public void UpdateSpawnStatus()
//    //{
//    //     //SpawnTop = GameObject.Find("SpawnTop");
//    //     //SpawnBot = GameObject.Find("SpawnBot");
//    //     //SpawnMid = GameObject.Find("SpawnMid");
//    //}


//    public void ReusePoolObjectAndPutToTargetSpawnArea(int creaturePoolId, SpawnLocation point, BaseObject newPersonality)
//    {
//        //if (creature.activeInHierarchy == true)
//        //{
//        //    throw new System.Exception("Error, target is not should not be active!");
//        //}

//        GameObject targetSpawn = GameObject.Find("Spawn" + point.ToString());
//        if (targetSpawn == null)
//        {
//            Debug.LogWarning("Could not find target spawn point");
//            throw new System.Exception("Error, could not find target spawn point!");
//        }
       
//       //Nice for debugging:
//       // print("targetSpawn active in heirarchy: " + targetSpawn.activeInHierarchy);
//       // print("targetSpawn active self: " + targetSpawn.activeSelf);

//        //pool.creatures.Fi

//        //creature.name = creature.name;


//        //creature.transform.parent = targetSpawn.transform;
//        //creature.transform.SetParent(targetSpawn.transform);


//        // = go.transform.position;
//        //creature.transform.parent = targetSpawn.transform;
//        Vector3 newPos = new Vector3();
//        //TESTIA:
//        newPos = ChangePos(targetSpawn);//Just temporary line
//        //creature.transform.position = new Vector3(580, 0, 6);
//        //pool.creaturePool.ResetGameObjectValuesById(creaturePoolId, targetSpawn, newPos);

//        newPersonality.OverwriteOldValues(newPersonality);
//        pool.generalPool.ReactivateObjectById(creaturePoolId, targetSpawn, newPos);

//        //RepositionCreatureUntilFreeAreaIsFound(creature, posCopy, targetSpawn);
//    }



//    //private void ReusePoolObjectAndPutToTargetSpawnArea(GameObject creature, SpawnLocation point)
//    //{
//    //    //if (creature.activeInHierarchy == true)
//    //    //{
//    //    //    throw new System.Exception("Error, target is not should not be active!");
//    //    //}

//    //    GameObject targetSpawn = GameObject.Find("Spawn" + point.ToString());
//    //    if (targetSpawn == null)
//    //    {
//    //        Debug.LogWarning("Could not find target spawn point");
//    //        throw new System.Exception("Error, could not find target spawn point!");
//    //    }

//    //    print("targetSpawn active in heirarchy: " + targetSpawn.activeInHierarchy);
//    //    print("targetSpawn active self: " + targetSpawn.activeSelf);

//    //    creature.name = creature.name;
//    //    creature.SetActive(true);
//    //    CREATURE_NUMBER++;


//    //    //creature.transform.parent = targetSpawn.transform;
//    //    creature.transform.SetParent(targetSpawn.transform);


//    //    Vector3 posCopy = new Vector3(); // = go.transform.position;
//    //                                     //creature.transform.parent = targetSpawn.transform;

//    //    //TESTIA:
//    //    // posCopy = ChangePos(posCopy, targetSpawn);//Just temporary line
//    //    creature.transform.position = new Vector3(580, 0, 6);


//    //    //RepositionCreatureUntilFreeAreaIsFound(creature, posCopy, targetSpawn);
//    //}

//    ////private void IntantianteObjectToTargetSpawnArea(Object creature, SpawnLocation point)
//    //private void IntantianteObjectToTargetSpawnArea(SpawnLocation point, BaseObject newPersonality)
//    //{
//    //    GameObject targetSpawn = GameObject.Find("Spawn" + point.ToString());

//    //    if (targetSpawn == null)
//    //    { throw new System.NullReferenceException("Error, could not find target spawn point!"); }

//    //    //GameObject newCreature = new GameObject();
//    //    //newCreature = Instantiate(creature, targetSpawn.transform.position, Quaternion.identity) as GameObject;
//    //    //newCreature.name = creature.name;
//    //    //newCreature.SetActive(true);

//    //    //FieldObjects

//    //    UnityEngine.Object newCreature = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + newPersonality.PrefabPath + newPersonality.PrefabName + ".prefab", typeof(GameObject));
//    //    //GameObject creature = null;

//    //    GameObject creature = Instantiate(newCreature, targetSpawn.transform.position, Quaternion.identity) as GameObject;
//    //    creature.SetActive(true); //pakko varmaan asettaa, muuten renderi ei toimi!
//    //    //creature.name = newPersonality.Name;
//    //    creature.name = newCreature.name.Replace("(Clone)", "");
//    //    creature.GetComponent<Creature>().OverwriteOldValues((Creature)newPersonality);
//    //    creature.GetComponentInChildren<UnityEngine.UI.Text>().text = newPersonality.Name;
      

//    //    //Set this to targetSpawn area, even this is not the final position
//    //    Vector3 posCopy = new Vector3(targetSpawn.transform.position.x, targetSpawn.transform.position.y, targetSpawn.transform.position.z);
//    //    //GameObject.Find("NewCreep").transform.parent = targetSpawn.transform;


//    //    // Transform parent = GameObject.Find("Suelo").transform;
//    //    creature.transform.SetParent(targetSpawn.transform);
//    //    //creature.transform.parent = targetSpawn.transform;
//    //    //creature.transform.SetParent(targetSpawn.transform, false);
//    //    int creatureId = creature.GetInstanceID();
//    //    //Add this type of creature to the pool
//    //    pool.CreaturePool.Add(creature); //pienellä?

//    //    //MItähän tämä tekee
//    //    RepositionCreatureUntilFreeAreaIsFound(creatureId, posCopy, targetSpawn);
//    //}









//    //public void RepositionCreatureUntilFreeAreaIsFound(ref GameObject creature , Vector3 posCopy, GameObject targetObject )
//    //{
//    //    bool freePositionFound = false;

//    //    //GameObject creature = (GameObject)newCreep;

//    //    foreach (GameObject o in pool.CreaturePool)
//    //    {
//    //        //Jump over the same object
//    //        //if (o.name == newCreep.name)
//    //        if (o.GetInstanceID() == creature.GetInstanceID())
//    //            continue;

//    //        if (o.name == creature.name)
//    //            continue;

//    //        do
//    //        {
//    //            posCopy = ChangePos(posCopy, targetObject);//Just temporary line
//    //            creature.transform.position = new Vector3(posCopy.x, posCopy.y, posCopy.z);

//    //            if (IsObjectInsideAnother(creature, o) == false)
//    //            {
//    //                freePositionFound = true;
//    //                break;
//    //            }
//    //            Debug.LogWarning("Resetting creature position");
//    //        } while (freePositionFound == false);
//    //    }
//    //}



//}
