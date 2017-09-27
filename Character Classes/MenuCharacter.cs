using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/*
public class MenuCharacter : MonoBehaviour 
{
		private Texture2D _icon;
		private string _name;
		private string _story;
		private string _skills;
		private string _toolTip;
		
		public Texture2D Icon
		{
			get{return _icon;}
			set{ _icon = value;}
		}
		
		public string Name
		{
			get {return _name;}
			set	{_name = value;}
		}
		
		public string Story
		{
			get {return _story;}
			set	{_story = value;}
		}
		
		public string Skills
		{
			get {return _skills;}
			set	{_skills = value;}
		}
		
		public string GetToolTip()
		{
			return Name + 
				//	"\n" + Story + 
				"\n" + Skills;
		}
}
*/

[System.Serializable]
public partial class GenericCharacter : MonoBehaviour
{

    //		Health,                 //Player collapses if this hits zero
    //		Level,					//Rises multiple stats depending on the this.growthModifier
    //		StatBoost,				//Your get slightly more points to skills. Handycaps early game, but late game is stronger
    //		ExpToLevel,				//Needed exp will grow exponentially
    //		Exp,					//Current total gained
    //		Sparks,                 //The game currency
    //		Discount,				//For shopping and trading
    //		Movement,               //Character movement speed
    //		Leadership,				//Can control group, make desicions, Open talks
    //		DMG_MAX,	            //Attack Damage Max
    //		DMG_MIN,	            //Minimun Damage To Inflict (base damage)
    //		MagicPower,				//Magic Damage Max
    //		BaseCooldown,			//General, affects every ability
    //		Knowledge,				//The more powerful the magic item is, the more knowledge you need to control the power. Picking too strong item might even set you on fire				
    //		Mass,					//Enemies cannot move you if you have higher or equal value
    //		MissChange,				//Possibity to miss
    //		Intelligence,			//Lvl up faster, "ExpBooster"
    //		Luck,					//The bigger the possibility to hit critical hit or dodge an attack
    //		Fortune,				//Better chance to happen something nice
    //		Holy,					//Possibility to rise again after getting killing blow. Gets random power boosts in fights
    //		Clues,					//Person can see the future and warn about danger lying ahead
    //		Favour,					//How people see you
    //		HealPower				//All the game healing methods will be more effective on you or by through your hands. 
    
   
    [SerializeField]  private Texture2D _icon;
    [SerializeField]  private string _name;
    [SerializeField]  private string _class;
    [SerializeField]  private string _world;
    [SerializeField]  private string _mastery;
    [SerializeField]  private string _about;
    [SerializeField]  private string _toolTip;
    [SerializeField]  private string _primary; //main ability
    [SerializeField]  private string _secondary; //main ability
    [SerializeField]  private double _primaryCooldown;
    [SerializeField]  private double _secondaryCooldown;
    [SerializeField]  private int _sparks;
    [SerializeField]  private List<string> uniquePassives;
    [SerializeField]  private string _startWeapon;
    [SerializeField]  private string _growthModifier;

    [Space]

    [Header("Stats")]
    [SerializeField]  private int _dmgMax;
    [SerializeField]  private int _dmgMin;
    [SerializeField]  private double _crit;
    [SerializeField]  private int _luck;
    [SerializeField]  private int _miss; //Miss change
    [SerializeField]  private int _mag;
    [SerializeField]  private int _intel;
    [SerializeField]  private int _hp;
    [SerializeField]  private int _mass;
    [SerializeField]  private int _size;
    [SerializeField]  private int _arch; //Archery damage
    [SerializeField]  private int _move;
    [SerializeField]  private int _dodge;
    [SerializeField]  private int _holy; //Possibility to rise again after getting killing blow. Gets random power boosts in fights
    [SerializeField]  private int _know;

    [Space]

    [Header("Estetics")]
    [SerializeField]  private string _clothColor;
    [SerializeField]  private string _birthMark;
    [SerializeField]  private string _alliance;
    [SerializeField]  private string _story;

    private PlayerData _playerData;


    public GenericCharacter()
    {
        _playerData = new PlayerData();
        this._hp = 5;
    }

    public PlayerData PlayerData
    {
        get { return _playerData; }
        set { _playerData = value; }
    }

    public Texture2D Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Class
    {
        get { return _class; }
        set { _class = value; }
    }

    public string Mastery
    {
        get { return _mastery; }
        set { _mastery = value; }
    }

    public string Story
    {
        get { return _story; }
        set { _story = value; }
    }

    public string World
    {
        get { return _world; }
        set { _world = value; }
    }

    public string About
    {
        get { return _about; }
        set { _about = value; }
    }

    public string Primary
    {
        get { return _primary; }
        set { _primary = value; }
    }

    public string Secondary
    {
        get { return _secondary; }
        set { _secondary = value; }
    }

    public double PrimaryCooldown
    {
        get { return _primaryCooldown; }
        set { _primaryCooldown = value; }
    }

    public double SecondaryCooldown
    {
        get { return _secondaryCooldown; }
        set { _secondaryCooldown = value; }
    }

    public int Sparks
    {
        get { return _sparks; }
        set { _sparks = value; }
    }

    public string GrowthModifier
    {
        get
        {
            return _growthModifier;
        }

        set
        {
            _growthModifier = value;
        }
    }

    public int DmgMax
    {
        get
        {
            return _dmgMax;
        }

        set
        {
            _dmgMax = value;
        }
    }

    public int DmgMin
    {
        get
        {
            return _dmgMin;
        }

        set
        {
            _dmgMin = value;
        }
    }

    public double Crit
    {
        get
        {
            return _crit;
        }

        set
        {
            _crit = value;
        }
    }

    public int Luck
    {
        get
        {
            return _luck;
        }

        set
        {
            _luck = value;
        }
    }

    public int Miss
    {
        get
        {
            return _miss;
        }

        set
        {
            _miss = value;
        }
    }

    public int Mag
    {
        get
        {
            return _mag;
        }

        set
        {
            _mag = value;
        }
    }

    public int Intel
    {
        get
        {
            return _intel;
        }

        set
        {
            _intel = value;
        }
    }

    public int Hp
    {
        get
        {
            return _hp;
        }

        set
        {
            _hp = value;
        }
    }

    public int Mass
    {
        get
        {
            return _mass;
        }

        set
        {
            _mass = value;
        }
    }

    public int Size
    {
        get
        {
            return _size;
        }

        set
        {
            _size = value;
        }
    }

    public int Arch
    {
        get
        {
            return _arch;
        }

        set
        {
            _arch = value;
        }
    }

    public int Move
    {
        get
        {
            return _move;
        }

        set
        {
            _move = value;
        }
    }

    public int Dodge
    {
        get
        {
            return _dodge;
        }

        set
        {
            _dodge = value;
        }
    }

    public int Holy
    {
        get
        {
            return _holy;
        }

        set
        {
            _holy = value;
        }
    }

    public int Know
    {
        get
        {
            return _know;
        }

        set
        {
            _know = value;
        }
    }

    public List<string> UniquePassives
    {
        get
        {
            return uniquePassives;
        }

        set
        {
            uniquePassives = value;
        }
    }

    public string StartWeapon
    {
        get
        {
            return _startWeapon;
        }

        set
        {
            _startWeapon = value;
        }
    }

    public string ClothColor
    {
        get
        {
            return _clothColor;
        }

        set
        {
            _clothColor = value;
        }
    }

    public string BirthMark
    {
        get
        {
            return _birthMark;
        }

        set
        {
            _birthMark = value;
        }
    }

    public string Alliance
    {
        get
        {
            return _alliance;
        }

        set
        {
            _alliance = value;
        }
    }

    public string ToolTip
    {
        get
        {
            return _toolTip;
        }

        set
        {
            _toolTip = value;
        }
    }

    public string GetToolTip()
    {
        return Name +
            "\n World:" + World +
            "\n Health " + Hp +
            "\n Damage: " + DmgMin + "(" + DmgMax +")" +
            "\n Magic: " + Mag + 
            "\n Mastery: " + Mastery +
            "\n Passives:" + UniquePassives +
            "\n Sparks: " + Sparks;
    }
}