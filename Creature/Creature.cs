using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Creature : BaseObject
{
   // public  string name;
    public CreatureType creatureType;
    [SerializeField]
    private CreatureCategory category;
    [SerializeField]
    private CreatureLvl lvl;
    private List<CreatureTags> tags;
    [SerializeField]
    private NaturalFieldPosition naturalFieldPosition;
    private float moveSpeed;
    public CreatureTags attackType;
    [SerializeField]
    private bool isNpc;

    public Creature()
    {
       // CREATURE_NUMBER++;
       // this.id = CREATURE_NUMBER;
        this.CreatureType = CreatureType.NotSet;
        this.Tags = new List<CreatureTags>();
        this.Appearance = new Appearance();
        this.FieldObjectType = FieldObjectType.Creature;
        this.PrefabPath = "Creatures/";
    }


    public CreatureType CreatureType
    {
        get { return creatureType; }
        set { creatureType = value; }
    }


    public CreatureCategory Category
    {
        get { return category; }
        set { category = value; }
    }

    public CreatureLvl Lvl
    {
        get { return lvl; }
        set { lvl = value; }
    }

    public List<CreatureTags> Tags
    {
        get { return tags; }
        set { tags = value; }
    }

    public NaturalFieldPosition NaturalFieldPosition
    {
        get { return naturalFieldPosition; }
        set { naturalFieldPosition = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public CreatureTags AttackType
    {
        get { return attackType; }
        set { attackType = value; }
    }

    //[SerializeField]
    //private bool interacting;
    //public bool Interacting
    //{
    //    get { return interacting; }
    //    set { interacting = value; }
    //}

    //public void Interact()
    //{
    //    Debug.Log("Murr!");
    //    EnemyAI ai = gameObject.GetComponent<EnemyAI>();
    //    if (ai != null)
    //    {
    //        ai.enabled = true;
    //    }
    //}

    public override void OverwriteOldValues(BaseObject identity) //ChangePersonality
    {
        Creature newPersonality = (Creature)identity;
        //this.name = newPersonality.Name.Replace("(Clone)", "");
        this.Name = newPersonality.Name;
        //this.CreatureType = newPersonality.CreatureType;
        this.PrefabName = newPersonality.PrefabName;
        //this.NaturalFieldPosition = newPersonality.NaturalFieldPosition; //the model will have same abilities
        this.MoveSpeed = newPersonality.MoveSpeed;
        //this.Category = newPersonality.Category; // Category will be the same
        this.Lvl = newPersonality.Lvl;
        this.Tags = new List<CreatureTags>();
        this.Tags.AddRange(newPersonality.Tags);
        this.Appearance = new Appearance();
        this.PrefabPath = newPersonality.PrefabPath;

        //Todo:
        //this.Appearance.Color
        //this.Appearance.ColorCode
    }


}

