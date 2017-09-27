//using UnityEngine;
//using System.Collections;
//using System;

//public class CharacterGenerator : MonoBehaviour {
////public GameObject playerPrefab;
	
//private int equipmentToChoose;
//private int itemToChoose;
//private int pointsLeft;
//public PlayerCharacter _toon;

//private const int BUTTON_WIDTH = 20;
//private const int BUTTON_HEIGHT = 25;
//private const int STARTING_POINTS = 20;
//private const int MIN_STARTING_ATTRIBUTE = 0;
//private const int GAP_BETWEENBUTTONS = 2;

//	// Use this for initialization
//	void Start () 
//	{
//		//_toon = new PlayerCharacter();
//		GameObject pc = GameObject.Find("pc");
//		 _toon = pc.GetComponent<PlayerCharacter>();
//		//_toon.Awake();
		
//		//Saatoa
//		//GameObject pc = GameObject.Find("playerPrefab");
//		//PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
//		//_toon = Instantiate(playerPrefab,Vector3.zero, Quaternion.identity) as PlayerCharacter;
//		//Ei vain suostu toimimaan
//		//GameObject pc = Instantiate(playerPrefab,Vector3.zero, Quaternion.identity) as GameObject;
//		//_toon = (PlayerCharacter)pc.GetComponent<PlayerCharacter>();
		
//		pointsLeft = STARTING_POINTS;
//		//equipmentToChoose = 1;
//		//itemToChoose = 3;
		
//		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
//		{
//			_toon.GetPrimaryAttribute(cnt).BaseValue = MIN_STARTING_ATTRIBUTE;
//		}
		
//		//Default starting values
//		_toon.GetVital((int)VitalName.Level).BaseValue = 1;
//		_toon.GetVital((int)VitalName.Health).BaseValue = 40;
//		_toon.GetVital((int)VitalName.Luck).BaseValue = 1;	
//		_toon.GetVital((int)VitalName.Holy).BaseValue = 1; //Gives nice suprise
//		_toon.GetVital((int)VitalName.BaseDamage).BaseValue = 1; //0 = Miss?
//		_toon.GetVital((int)VitalName.MagicPower).BaseValue = 0; //0 = Miss? or the magic will only do the static damage which is very small
//		_toon.GetVital((int)VitalName.AttackStrengthMax).BaseValue = 2;
//		_toon.GetVital((int)VitalName.AttackSpeed).BaseValue = 2; // one hit per every second sec
//		_toon.GetVital((int)VitalName.Coin).BaseValue = 100;
//		_toon.GetVital((int)VitalName.BaseCooldown).BaseValue = 50;  //The normal
//		_toon.GetVital((int)VitalName.Movement).BaseValue = 20;  //The normal
//		_toon.GetVital((int)VitalName.ChanceToHit).BaseValue = 90;  // 90% of the time you hit
		

//		_toon.StatUpdate();
//	}

	
//	// Update is called once per frame
//	void Update () 
//	{
	
//	}
	
	
//	void OnGUI()
//	{
//	    DisplayName();
//	    //DisplayCreateButton();
//		DisplayAttributesAndButtons();
//		//DisplayVitalsAndButtons();
//		//DisplaySkills();
//		DisplayVitals();
//		DisplayPointsLeft();
//		//DisplayOptionalChoosing();
//	}
	
//	private void DisplayName()
//	{
//		GUI.Label(new Rect(10,15,50,25), "Name:");
//		_toon.Name = GUI.TextField(new Rect(60,15,150,BUTTON_WIDTH), _toon.Name);
//	}
	
//	private void DisplayAttributesAndButtons()
//	{
//		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
//		{
//			GUI.Label(new Rect(10,40 + (cnt * 25),100,25), ((AttributeName)cnt).ToString());
//			GUI.Label(new Rect(110,40 + (cnt * 25),30,25), _toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
//			if (GUI.Button(new Rect(130,40 + (cnt * 25),30,25),"-"))
//			{
//				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE)
//				{
//					_toon.GetPrimaryAttribute(cnt).BaseValue--;
//					pointsLeft++;
//					_toon.StatUpdate();
					
//				}
//			}
//			if (GUI.Button(new Rect(160 +GAP_BETWEENBUTTONS,40 + (cnt * 25),30,25),"+"))
//			{
//				if(pointsLeft > 0)
//				{
//					_toon.GetPrimaryAttribute(cnt).BaseValue++;
//					pointsLeft--;
//					_toon.StatUpdate();
//				}
//			}
//		}
//		//GUI.Button(new Rect(130,40,30,25),"<-");
//		//GUI.Button(new Rect(160 +GAP_BETWEENBUTTONS,40, 30,25),"->");
		
//	}
	
//	private void DisplayVitals()
//	{
//		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
//		{
//			GUI.Label(new Rect(250,40 + (cnt * 25),100,25), ((VitalName)cnt).ToString());
//			GUI.Label(new Rect(355,40 + (cnt  * 25),30,25), _toon.GetVital(cnt).AdjustedBaseValue.ToString());
//		}
//	}
	
//	private void DisplayPointsLeft()
//	{
//		GUI.Label(new Rect(250,15,100,25), "Points Left:" + pointsLeft);
//	}
	
	
//	//private void DisplayCreateButton()
//	//{	
//	//	if (GUI.Button(new Rect(500,200,150,100),  "Play") == true)
//	//	{
//	//		//GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
//	//		//gsScript.SaveCharacterData();
//	//		Application.LoadLevel("Level01");
//	//	}
//	//}
	
//}
