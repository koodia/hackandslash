using UnityEngine;
using System.Collections;
using System;
public class BaseHero: MonoBehaviour
{


	//Only with heros:
	//private float _expToLevel;
	//private int _statBoost;
	//private int _exp;
	//private int _discount;
	//private int leadership;
	//private int knowledge
	//private int intelligence
	//private int fortune;
	//private int holy;
	//private int clues;
	//private int favour;


	private string _name;
	private int _level;
	private uint _freeExp;
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	//private Skill[] _skill;
	
	
	public void Awake()
	{
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
		
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		//_skill  = new Skill[Enum.GetValues(typeof(SkillName)).Length];
	
	
		SetupPrimaryAttributes();
		SetupVitals();
		//SetupSkills();
	
	}
	
	public string Name
	{
		get{ return _name;}
		set{ _name = value;}
	}

	public int Level
	{
		get{ return _level;}
		set{_level = value;}
	}

	public uint FreeExp
	{
		get{return _freeExp;}
		set{_freeExp = value;}
	}

	public void AddExp(uint exp)
	{
		_freeExp += exp;
		CalculcateLevel();
	}
	
	// take avg of all of the players skills and assign that as the player level
	public void CalculcateLevel()
	{
	
	}
	
	private void SetupPrimaryAttributes()
	{
		for(int cnt = 0; cnt <_primaryAttribute.Length; cnt++)
		{
			_primaryAttribute[cnt] = new Attribute();
		}
	}
	
	private void SetupVitals()
	{
		for(int cnt = 0; cnt <_vital.Length; cnt++)
		{
			_vital[cnt] = new Vital();
		}
		SetupVitalModifiers();
	}
	
//	private void SetupSkills()
//	{
//		for(int cnt = 0; cnt <_skill.Length; cnt++)
//		{
//			_skill[cnt] = new Skill();
//		}
//		SetupSkillModifiers();
//	}
	
	public Attribute GetPrimaryAttribute(int index)
	{
		return _primaryAttribute[index];
	}
	
	public Vital GetVital(int index)
	{
		return _vital[index];
	}
	
//	public Skill GetSkill(int index)
//	{
//		return _skill[index];
//	}

	private void SetupSkillModifiers()
	{
	
	}
	
	private void SetupVitalModifiers()
	{
		//Debug.Log("VitalModifiersSet");
		////Fighters & Melee
		//GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 5f));
		//GetVital((int)VitalName.AttackStrengthMax).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), 1.5f));
		//GetVital((int)VitalName.BaseDamage).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Focus), 0.5f));
		//GetVital((int)VitalName.AttackSpeed).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.WeaponHandling), 1f));
		//GetVital((int)VitalName.Luck).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Luck), 2.0f));
		
		////Ranged
		//GetVital((int)VitalName.AttackDistance).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Focus), 1f));
		
		////Mages
		//GetVital((int)VitalName.MagicPower).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Wisdom), 1f));
		//GetVital((int)VitalName.BaseCooldown).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), 1f));
		//GetVital((int)VitalName.BaseCooldown).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), 1f));
		
		////Tanks/Defenders
		//GetVital((int)VitalName.Mass).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Muscle), 1f));
		//GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Muscle), 0.5f));
		
		////Carrys
		//GetVital((int)VitalName.StatBoost).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Intelligence), 1f));
		//GetVital((int)VitalName.Leadership).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Charisma), 1.0f));
	
		////The suprising unnatural stuff
		//GetVital((int)VitalName.Fortune).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.FortuneTelling), 1f));
		//GetVital((int)VitalName.Holy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Believer), 1f));
		//GetVital((int)VitalName.Fortune).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Believer), 0.5f));
	
		////Town boosters and way to get earn Favour
		//GetVital((int)VitalName.Discount).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Bartel), 1f));
		//GetVital((int)VitalName.Coin).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Bartel), 20f));
		////GetSkill((int)SkillName.Discount).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)SkillName.Leadership), 0.5f));
		////GetSkill((int)SkillName.Favour).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)SkillName.Leadership), 0.5f));
		//GetVital((int)VitalName.Favour).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Heroism), 0.5f));
		//GetVital((int)VitalName.Favour).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.DarkSide), -5.0f));
		
		////MISC
		//GetVital((int)VitalName.Movement).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 1f));
		//GetVital((int)VitalName.AttackSpeed).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 0.25f));
	
		//Healing
		//GetVital((int)VitalName.HealPower).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Healer), 1.0f));
		
	
	}
	
	public void StatUpdate()
	{
		for(int cnt = 0; cnt <_vital.Length; cnt++)
		{	
			_vital[cnt].Update();
		}
		
//		for(int cnt = 0; cnt <_skill.Length; cnt++)
//		{	
//			_skill[cnt].Update();
//		}

	
	}
	
}
