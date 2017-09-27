
using UnityEngine;

public class Interactable : MonoBehaviour 
{
    public UnityEngine.AI.NavMeshAgent playerAgent;

    public virtual void MoveToInteraction(UnityEngine.AI.NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        playerAgent.destination = this.transform.position;

        Interact();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with the base class");
    }

}

