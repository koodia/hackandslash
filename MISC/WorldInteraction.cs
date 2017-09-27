using UnityEngine;
using System.Collections;

public class WorldInteraction : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent playerAgent;
	// Use this for initialization
	void Start () {
        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKeyUp(KeyCode.L))
        {
           if( IsObjectCloseEnough())
            {
                GetInteraction();
            }
         
        }
    }

    public void GetInteraction()
    {
        GameObject tempObject = GameObject.FindGameObjectWithTag("Chest");
        tempObject.GetComponent<Chest>().TryUse();
    }


     
    public bool IsObjectCloseEnough()
    {
        float minimumDistance = 80;
        //GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag("Chest");//GameObject.FindGameObjectsWithTag("Interactable Object");
        CustomExceptions.Assert(goWithTag == null, "Could not found  object by \"Player\" tag");

        GameObject pc = GameObject.Find("Cha_Knight");
        Transform pcTransform = pc.GetComponent<Transform>();

        for (int i = 0; i < goWithTag.Length; ++i)
        {
            //if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
            if (Vector3.Distance(pcTransform.position, goWithTag[i].transform.position) <= minimumDistance)
            {
                Debug.Log("Interactable interacted");
                return true;
            }
        }
        

        return false;
    }
}
