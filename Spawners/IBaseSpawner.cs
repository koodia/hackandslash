using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IBaseSpawner
{
    void CreateGameObjects<T>(List<T> objects);

    void SpawnGameObject(BaseObject personality, SpawnLocation spawnLocation);
}

public abstract class SpawnerHelper : MonoBehaviour
{
    public ObjectPooler pooler;
    const int MAX_REPOSITIONING_TRIES = 50;
    //remove later
    public static int CREATURE_NUMBER = 0;
    const int SPAWNING_HEIGHT = 2;

    void Start()
    {
        //GameObject poolObject = GameObject.Find("GameObjectPooler");
        //pooler = poolObject.GetComponent<ObjectPooler>();
    }

    public virtual void ReusePoolObjectAndPutToTargetSpawnArea()
    {
        throw new NotImplementedException();
    }


    public void IntantianteObjectToTargetSpawnArea(SpawnLocation point, BaseObject newPersonality)
    {
        if (point == SpawnLocation.Random)
        {
            throw new ArgumentException("SpawnLocation.Random cannot be used");
        }
        GameObject targetSpawn = GameObject.Find("Spawn" + point.ToString());

        if (targetSpawn == null)
        { throw new System.NullReferenceException("Error, could not find target spawn point!"); }

    
        UnityEngine.Object newObject = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + newPersonality.PrefabPath + newPersonality.PrefabName + ".prefab", typeof(GameObject));
        GameObject obj = Instantiate(newObject, targetSpawn.transform.position, Quaternion.identity) as GameObject;
        //obj.AddComponent("Creature");
        obj.SetActive(true); //pakko varmaan asettaa, muuten renderi ei toimi!
        obj.name = newObject.name.Replace("(Clone)", "");

       
        try
        {
            obj.GetComponent<BaseObject>().OverwriteOldValues(newPersonality);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        if (newPersonality.FieldObjectType == FieldObjectType.Creature)
        {
            try
            {

                obj.GetComponentInChildren<UnityEngine.UI.Text>().text = newPersonality.Name;
            }
            catch
            {
                Debug.Log("Could not found UI text. Personality.Name:" + newPersonality.Name);
            }
        }


        //Set this to targetSpawn area, even this is not the final position
        Vector3 posCopy = new Vector3(targetSpawn.transform.position.x, targetSpawn.transform.position.y, targetSpawn.transform.position.z);
        obj.transform.SetParent(targetSpawn.transform);
        int objId = obj.GetInstanceID();
        //Add this type of creature to the pool
        pooler.Pool.Add(obj);

        //Lol
        RepositionCreatureUntilFreeAreaIsFound(objId, posCopy, targetSpawn);
    }


    //public void OverwriteOldValuesAccordingly(GameObject obj)
    //{
    //    //FieldObjectType type = obj.Type;// objects.GetType().GetProperty("Item").PropertyType;


    //    switch (obj.GetComponent<BaseObject>().FieldObjectType)
    //    {
    //        case FieldObjectType.Creature:
    //            obj.GetComponent<BaseObject>().OverwriteOldValues(obj.GetComponent<BaseObject>());
    //            break;
    //        case FieldObjectType.Obstacle:
    //            obj.GetComponent<BaseObject>().OverwriteOldValues(obj.GetComponent<BaseObject>());
    //            break;
    //        case FieldObjectType.NotSet:
    //            throw new Exception("Error FieldObjecType is not set");
    //        default: throw new Exception("Error FieldObjecType is not set");
    //    }
    //}


 

    public void RepositionCreatureUntilFreeAreaIsFound(int objectPoolId, Vector3 posCopy, GameObject targetObject)
    {
        if (targetObject == null) { throw new System.NullReferenceException("Fatal error, targetObject "); }

        if (objectPoolId == 0) { throw new System.NullReferenceException("Fatal error, creatureId = 0"); }

        bool positionTaken = false;

        //GameObject creature = (GameObject)newCreep;

        // foreach (GameObject o in pool.creaturePool.Pool)
        // {
        //Jump over the same object
        //if (o.name == newCreep.name)
        //     if (o.GetInstanceID() == creaturePoolId)
        //         continue;

        //if (o.name == creature.name)
        //    continue;

        int repositionTryCount = 0;
        do
        {
            Vector3 newPos = ChangePos(targetObject);
            //Change the position and test if nothing hits this 
            pooler.GetObjectById(objectPoolId).transform.position = new Vector3(newPos.x, newPos.y, newPos.z);

            //Is any of the battlefield objects overlapping?:
            foreach (GameObject o in pooler.Pool)
            {
                if (o.GetInstanceID() == objectPoolId)
                { continue; }

                if (IsObjectInsideAnother(pooler.GetObjectById(objectPoolId), o) == true)
                {
                    positionTaken = true;
                    Debug.LogWarning("Position taken. Resetting creature position. Reset count:" + repositionTryCount);
                    break;
                }
            }

            if (repositionTryCount == MAX_REPOSITIONING_TRIES)
            {
                //End the looping because a free position could not be found inside the time frame(tries)
                positionTaken = false;
                //and set the object to unactive
                pooler.DeactivePoolObject(objectPoolId);
            }
            repositionTryCount++;




        } while (positionTaken == true); //Jippii, a free position found!, we can happily exit from the loop

    }

    /// <summary>
    /// Get random position inside the target spawn x and z 
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="targetSpawn"></param>
    /// <returns></returns>
    public Vector3 ChangePos(GameObject targetSpawn)
    {
    //http://answers.unity3d.com/questions/292874/renderer-bounds.html

        Vector3 newPos = new Vector3();
        Renderer renderer = targetSpawn.GetComponentInChildren<Renderer>();
        newPos.x = GC.rand.rnd.Next((int)renderer.bounds.min.x, (int)renderer.bounds.max.x);
        newPos.z = GC.rand.rnd.Next((int)renderer.bounds.min.z, (int)renderer.bounds.max.z); //Mathf.RoundToInt
        newPos.y = SPAWNING_HEIGHT; // Star slighly above the ground
        return newPos;
    }

    public static bool IsObjectInsideAnother(GameObject spawningObject, GameObject existingObject)
    {
        Renderer renderer = spawningObject.GetComponentInChildren<Renderer>();

            if (renderer == null)
            {
                throw new UnityException("Error, Could not find renderer from spawningObject renderer is null");
            }

            if (existingObject == null)
            {
                throw new UnityException("Error, existingObject is null");
            }

            // Get the two corners f this GameObject that Unity can tell us.
            Vector3 leftBottomFrontCornerOfObject = renderer.bounds.min;
            Vector3 rightTopBackCornerOfObject = renderer.bounds.max;
            float left = leftBottomFrontCornerOfObject.x;
            float right = rightTopBackCornerOfObject.x;
            float top = rightTopBackCornerOfObject.y;
            float bottom = leftBottomFrontCornerOfObject.y;
            float front = leftBottomFrontCornerOfObject.z;
            float back = rightTopBackCornerOfObject.z;

            // Construct each corner of the GameObject from the two known corners.
            Vector3 rightTopFrontCornerOfObject = new Vector3(right, top, front);
            Vector3 leftTopFrontCornerOfObject = new Vector3(left, top, front);
            Vector3 leftTopBackCornerOfObject = new Vector3(left, top, back);
            Vector3 rightBottomFrontCornerOfObject = new Vector3(right, bottom, front);
            Vector3 rightBottomBackCornerOfObject = new Vector3(right, bottom, back);
            Vector3 leftBottomBackCornerOfObject = new Vector3(left, bottom, back);

            //GameObject targetFieldBorders =  GameObject.Find("BattlefieldBorders");   //GameObject.Find("BattlefieldBorders"); //existingObject.Name
            //Collider boxColliders = existingObject.GetComponent<Collider>();
            Collider boxColliders = existingObject.GetComponentInChildren<Collider>();

            if (boxColliders != null)
            {

                if (boxColliders.bounds.Contains(leftBottomFrontCornerOfObject) ||
                    boxColliders.bounds.Contains(rightTopBackCornerOfObject) ||
                    boxColliders.bounds.Contains(rightTopFrontCornerOfObject) ||
                    boxColliders.bounds.Contains(leftTopFrontCornerOfObject) ||
                    boxColliders.bounds.Contains(leftTopBackCornerOfObject) ||
                    boxColliders.bounds.Contains(rightBottomFrontCornerOfObject) ||
                    boxColliders.bounds.Contains(rightBottomBackCornerOfObject) ||
                    boxColliders.bounds.Contains(leftBottomBackCornerOfObject))
                {
                    //Debug.Log("Object at least partially outside world edges!");
                    //Debug.Log("Objects colliders are intersecting");
                    return true;
                }
            }

        return false;
    }
}