using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class Obstacle : BaseObject, IInteractable, ICarryable, ICollectable
{
    public bool interacting;

    public bool Interacting
    {
        get { return interacting; }
        set { interacting = value; }
    }

    public Obstacle()
    {
        this.MaterialType = MaterialType.Rock;
        //this.Tags = new List<CreatureTags>();
        this.Appearance = new Appearance();
        this.FieldObjectType = FieldObjectType.Obstacle;
        this.PrefabPath = "FieldObjects/";
    }

  

    public void Carry()
    {
        Debug.Log("Carrying");
    }

    public void Collect(Transform newParent)
    {
        Debug.Log("Obstacle collected, but no where to put it yet?!");
       // Destroy(gameObject);
    }


    public void CheckInteractionDistance(Collider trigger)
    {
        if (trigger.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log("Triggered!");
            Interact();

        }
    }

    public void Interact()
    {
        Debug.Log("Obstacle destroyed or picked");
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider trigger)
    {
        CheckInteractionDistance(trigger);
    }


    //public UnityEngine.AI.NavMeshAgent playerAgent { get; set; }

    //public void MoveToInteraction(UnityEngine.AI.NavMeshAgent playerAgent)
    //{
    //    this.playerAgent = playerAgent;
    //    playerAgent.destination = this.transform.position;

    //    Interact();
    //}

    //public void Interact()
    //{
    //    Debug.Log("Interacting with the base class");
    //}





    public override void OverwriteOldValues(BaseObject identity) //ChangePersonality
    {
        Obstacle newPersonality = (Obstacle)identity;
        this.Name = newPersonality.Name;
        this.PrefabName = newPersonality.PrefabName;
        this.Appearance = new Appearance();

        //Todo:
        //this.Appearance.Color
        //this.Appearance.ColorCode
    }
}
