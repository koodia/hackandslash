using System;
using UnityEngine;

public class Item : BaseObject, ICollectable
{
	private string _name;
	private string _info;
	private int _value;
	private RarityTypes _rarity;
	private int _balancePoint;
	//private int _;
	private Texture2D _icon;
    public Sprite _sprite; //testi

   


    //Taiat FF = Everything
    //Melee FF = OnlyEnemy
    //Range FF = Everything, suora linja tai sitten ampuu roikulla joka on vaikeampaa(ei merkkiä kentällä)
    //Artifact FF = OnlyAlly
    private ffMode _ffMode;
	
	public enum ffMode
	{
		Everything,
		OnlyAlly,
		OnlyEnemy,
	}
	
	public Item()
	{
		_name  = "Need name";
		_value = 0;
		_rarity = RarityTypes.Common;
		_balancePoint = 1;
	}
	
	public Item(string name, int value, RarityTypes rare,  int balancePoint )
	{
		_name = name;
		_value = value;
		_rarity = rare;
		_balancePoint = balancePoint;
	}
	
	public string Name
	{
		get {return _name;}
		set  {_name = value;}
	}
	public string Info
	{
		get {return _info;}
		set {_info = value;}
	}
	
	public int Value
	{
		get {return _value;}
		set {_value = value;}
	}
	
	public RarityTypes Rarety
	{
		get  {return _rarity;}
		set  {_rarity = value;}
	}
	
	public int BalancePoint
	{
		get {return _balancePoint; }
		set {_balancePoint = value;}
	}
	
	public ffMode FFMode
	{
		get {return _ffMode;}
		set {_ffMode = value;}
	}

	public Texture2D Icon
	{
		get{return _icon;}
		set{ _icon = value;}
	}
	
	public virtual string ToolTip()
	{
		return Name + 
		"\n" + "Value: " + Value + 
		"\n" + "BalancePoint: " + BalancePoint + 
		"\n" + "Rarity: " + Rarety + 
		"\n" + "Info: " + Info;
	}

    public void Collect(Transform newParent )
    {
        Debug.Assert(newParent != null, "newParent is null");

        Debug.Log("Item collected");
        gameObject.transform.parent = newParent;
       // gameObject.SetActive(false); //jaa a
    }

    //Testaa, ei tietoa toimiiko
    public override void OverwriteOldValues(BaseObject identity) //ChangePersonality
    {
        Item newPersonality = (Item)identity;
        this.Name = newPersonality.Name;
        this.PrefabName = newPersonality.PrefabName;
        this.Appearance = new Appearance();

        //Todo:
        //this.Appearance.Color
        //this.Appearance.ColorCode




        //ESIMERKKIÄ:
        /*
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
        */

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
    //    //TODO:just a temp
    //    PlayerCharacter player = GameObject.Find("Player 1").GetComponent<PlayerCharacter>();
    //    player.inventory.inspectionItem = this.gameObject;
    //}
}
	
public enum RarityTypes
{
	Common,
	Uncommon,
	Rare,
	Rare3_lvl_0, 	//Limited number of variables which the weapon is generated
	Rare2_lvl_20, 	//Limited number of variables which the weapon is generated
	Rare1_lvl_30, 	//Limited number of variables which the weapon is generated
	Legendary						//RareS_lvl_50 	//Totally random stats
}


