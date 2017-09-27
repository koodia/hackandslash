using System;
using System.Collections.Generic;
using UnityEngine;


//public class PlayerCharacter : BaseHero , IDamageable<int>
public class PlayerCharacter : GenericCharacter, IDamageable<int>, IKillable, IHealable<int>,IRevivable, IInteractable
{
    [SerializeField]
    public InventoryController Inventory;

    [SerializeField]
    public BoxCollider interactionField;
    [SerializeField]
    private bool interacting;
    public Collider colliderTrigger;




    void Start()
    {
        Inventory = GameObject.Find("InventoryCanvas").GetComponentInChildren<InventoryController>();

        if (Inventory == null)
        {
            throw new Exception("Could not find Inventory from scene!");
        }
    }

    public PlayerCharacter()
    {
        isAlive = true;
    }

    public bool Interacting
    {
        get { return interacting; }
        set { interacting = value; }
    }

 //   private static List<Item> _inventory = new List<Item>();
	//public static List<Item> Inventory
	//{
	// 	get{return _inventory; }
	//}

    public float liftSpeed
    {
        get
        {
            return 1.0f;
        }
    }

    private bool isAlive;
    public bool IsAlive
    {
        get{ return isAlive; }
        set{ isAlive = value;}
    }

    public void Damage(int damageAmount)
    {
        if (!isAlive)
        { return; }

        Hp -= damageAmount;
        if(Hp < 0)
        {
            Kill();
        }
    }

    public void Heal(int hpAmount)
    {
        if (!isAlive)
        { return; }

        Hp += hpAmount;
    }

    public void Kill()
    {
        Debug.Log("Player character killed!");
        Destroy(gameObject);
    }

    public void Interact()
    {
        Debug.Log("You are talking with yourself");
    }

    public void InteractWithObject(Collider trigger)
    {

        trigger.gameObject.GetComponent<IInteractable>().Interact();
    }

    public void CollectObject(Collider trigger, Transform newParent)
    {
        trigger.gameObject.GetComponent<ICollectable>().Collect(newParent);
    }


    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (Interacting)
            {
               // Debug.Assert(colliderTrigger == true, "ERROR, you are interacting with an interactable but colliderTrigger is false?!");


                if (colliderTrigger != null && colliderTrigger.gameObject.GetComponent<IInteractable>() != null)
                {
                    Debug.Log("Triggered with " + colliderTrigger.gameObject.name);
                    InteractWithObject(colliderTrigger);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Inventory.inspectionItem != null)
        {
            if (Interacting) //probably useless
            {
                Inventory.itemSlots[0] = Inventory.inspectionItem;
                CollectObject(colliderTrigger, Inventory.gameObject.transform);
                Inventory.slotA.sprite = Inventory.testikuva;
                Inventory.slotA.enabled = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha4) && Inventory.inspectionItem != null)
        {
            if (Interacting) //probably useless
            {
                Inventory.itemSlots[1] = Inventory.inspectionItem;
                CollectObject(colliderTrigger, Inventory.gameObject.transform);

                //               slotA.sprite = itemSlots[0].GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && Inventory.inspectionItem != null)
        {
            if (Interacting) //probably useless
            {
                Inventory.itemSlots[4] = Inventory.inspectionItem;
                CollectObject(colliderTrigger, Inventory.gameObject.transform);

                //               slotA.sprite = itemSlots[0].GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && Inventory.inspectionItem != null)
        {
            if (Interacting) //probably useless
            {
                Inventory.itemSlots[3] = Inventory.inspectionItem;
                CollectObject(colliderTrigger, Inventory.gameObject.transform);

                //               slotA.sprite = itemSlots[0].GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }




    }

    public void Revive()
    {
        throw new NotImplementedException();
    }


}
