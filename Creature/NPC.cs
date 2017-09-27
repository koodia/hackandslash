using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class NPC : Creature, IInteractable
{
    public UnityEngine.AI.NavMeshAgent playerAgent { get; set; }

    public void Start()
    {
        //TODO: This is temporary. Collider.istTrigger should be true all times
        BoxCollider collider = gameObject.transform.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.isTrigger = true;

        }

    }

    private bool interacting;
    public bool Interacting
    {
        get { return interacting; }
        set { interacting = value; }
    }

    //public void CheckInteractionDistance(Collider trigger)
    //{
    //    if (trigger.gameObject.GetComponent<Iinteractable>() != null)
    //    {
    //        Debug.Log("Triggered!");
    //        Interact();    
    //    }
    //}

    public void Interact()
    {
        Debug.Log("Hello! How can I help you");
    }

    //public void OnTriggerEnter(Collider trigger)
    //{
    //    CheckInteractionDistance(trigger);
    //}

    //NavMeshAgent Iinteractable.playerAgent
    //{
    //    get
    //    {
    //        throw new NotImplementedException();
    //    }

    //    set
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public void MoveToInteraction(UnityEngine.AI.NavMeshAgent playerAgent)
    //{
    //    this.playerAgent = playerAgent;
    //    playerAgent.destination = this.transform.position;

    //    Interact();
    //}





    //public  void Interact()
    //{
    //    Debug.Log("Interacting with the base class");
    //}





}
