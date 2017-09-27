using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    private ObjectPooler current;
    public List<GameObject> pool;
    public bool timeFreezeAll;

    public List<GameObject> Pool
    {
        get { return pool; }
        set { pool = value; }
    }

    void Awake()
    {
        current = this;
        DontDestroyOnLoad(current);
    }

    void Start()
    {
        pool = new List<GameObject>();
    }

    public void TimeFreezeAllObjects(bool flag)
    {
        if (this.pool.Count != 0)
        {
            for (int i = 0; i < Pool.Count; i++)
            {
                Pool[i].GetComponent<BaseObject>().timeFreeze = flag;
                if (Pool[i].GetComponent<EnemyAI>() != null)
                {
                    Pool[i].GetComponentInChildren<EnemyAI>().isTimeFreezed = flag;
                }
            }
        }
        timeFreezeAll = flag;
    }


    public void DeactivePoolObjects()
    {
        if (this.pool != null && this.pool.Count != 0)
        {
            for (int i = 0; i < Pool.Count; i++)
            {
                //Detach from the parent first
                Pool[i].transform.SetParent(gameObject.transform);
                Pool[i].transform.position = Vector3.zero; //TODO: remove this when game is ready. Helps debugging but decreases performance 
                Pool[i].SetActive(false);
            }
        }
    }

    public void DeactivePoolObject(int instanceID)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (Pool[i].GetInstanceID() == instanceID)
            {
                Pool[i].transform.SetParent(gameObject.transform);
                Pool[i].transform.position = Vector3.zero; //TODO: remove this when game is ready. Helps debugging but decreases performance 
                Pool[i].SetActive(false);
            }
        }
    }


    /// <summary>
    /// Tries to find a unactive(free) object from the pool by PrefabName
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public int ReuseUnactiveGO(string prefabName)
    {
        for (int i = 0; i < this.Pool.Count; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                //Creature c = Pool[i].GetComponent<Creature>();
                BaseObject c = Pool[i].GetComponent<BaseObject>();
                if (c.PrefabName == prefabName && Pool[i].activeSelf == false)
                {
                    Pool[i].SetActive(true);//Tämä valittu joten asetetaan varmuuden vuoksi aktiiviseksi
                    return c.ID;
                }
            }
        }
        return 0;
    }

    public void ReactivateObjectById(int id, GameObject parentObject, Vector3 pos)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (Pool[i].GetComponent<BaseObject>().ID == id)
            {
                if (Pool[i].activeSelf)
                {
                    Debug.LogWarning( "Spawn of " + Pool[i].GetComponent<BaseObject>().Name + " is already active object!", Pool[i]);
                }
                Pool[i].transform.SetParent(parentObject.transform);
                Pool[i].transform.position = new Vector3(pos.x, pos.y, pos.z);
              //  Pool[i].timeFreeze = true;
                break; //found the unique id
            }
        }
    }

    public GameObject GetObjectById(int id)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (Pool[i].GetInstanceID() == id)
            {
                return Pool[i];
            }
        }
        return null;
    }
}
