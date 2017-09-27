using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionField : MonoBehaviour
{
    [SerializeField]
    public PlayerCharacter player;
    //private Color activeColor = Color.green;
    //private Color unactiveColor = Color.red;

    public void Start()
    {
        //TODO: This is temporary. Collider.istTrigger should be true all times
        BoxCollider collider = gameObject.transform.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }

        //Gizmos.color = unactiveColor;
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag  == "Player") //Not tested
        {
            Debug.Log("Errror! One of the colliders is hitting yourself!");
            return;
        }

        if (trigger.gameObject.tag == "Loot")
        {
            player.Inventory.inspectionItem = trigger.gameObject;
        }

        player.Interacting = true;
        player.colliderTrigger = trigger;
        //Gizmos.color = activeColor;
    }

    private void OnTriggerExit(Collider trigger)
    {
        player.Interacting = false;
        player.colliderTrigger = null;
        //Gizmos.color = unactiveColor;

        player.Inventory.inspectionItem = null;
    }
}
