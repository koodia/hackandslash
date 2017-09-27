using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class CharacterSelection : MonoBehaviour
{
    private const string CHARACTER_ICON_PATH = "Character icons/";
    public static List<GenericCharacter> _list = new List<GenericCharacter>();
    // public List<GenericCharacter> heroList = new List<GenericCharacter>();

    private int _characterPictureCount;
    //private int _playerCount;
    private string _player1Selection;
    //private string _player2Selection;
    //private string _player3Selection;
    //private string _player4Selection;
    string[] fileNames = new string[100];
    private string _toolTip = "";
    //private int level;
    public string loadLevel;

    //private int pictureDistanceX;
    //private int pictureDistanceY;
    private int _offset;
    private int _iconWidth;
    private int _iconHeight;
    private float _pictureRotation;

    public Vector2 buttonPos;


    private bool startGame = false;
    FadeManager fadeManager;

    // Use this for initialization
    void Start()
    {
        fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        if (fadeManager == null)
        {
            throw new NullReferenceException("fadeManager");
        }

        loadLevel = "CharacterSelection";
        _iconWidth = 36;
        _iconHeight = 36;


        JsonReader reader = new JsonReader();
        List<GenericCharacter> heroList = new List<GenericCharacter>();
        heroList = reader.LoadHeroList();

        if (heroList == null)
        {
            Debug.LogWarning("ERROR, heroList is null");
        }

        if (heroList.Count == 0)
        {
            Debug.Log("ERROR, hero list is empty");
        }

        Debug.LogWarning("Number of heroes found from excel list: " + heroList.Capacity);

        LoadMenuCharacters(heroList);

    }


    private void LoadMenuCharacters(List<GenericCharacter> heroList)
    {
        fileNames = Help.GetFileNames(Application.dataPath + "/Resources/" + CHARACTER_ICON_PATH, "*.png");
        if (fileNames == null)
        {
            Debug.Log("ERROR, fileNames are null");
        }

        string name;
        GenericCharacter hero;
        Texture2D ico;

        int cnt = 0;
        foreach (string fileName in fileNames)
        {
            name = Path.GetFileNameWithoutExtension(fileName);
            hero = new GenericCharacter();
            ico = Resources.Load(CHARACTER_ICON_PATH + name) as Texture2D;
            hero.Icon = ico;

            if (cnt > heroList.Count - 1)
            {
                //Varo herolistan kokoa
                hero.Name = name;
            }
            else
            {
                hero.Name = heroList[cnt].Name;
                int rolledDmg = GC.rand.rnd.Next(0, 3);
                VitalName rolledSkill = Help.GetRandomEnumValue<VitalName>();
                hero.Sparks = GC.rand.rnd.Next(0, 100);
                hero.Class = heroList[cnt].Class;
                hero.DmgMin = heroList[cnt].DmgMin;
                hero.DmgMax = heroList[cnt].DmgMax;
                hero.Hp = heroList[cnt].Hp;
                hero.Mastery = heroList[cnt].Mastery;
                //hero.Story = "Long lost warrior whos trying to find his past";
            }

            _list.Add(hero);
            cnt++;
        }
        Debug.Log("iconList size:" + _list.Count);
    }



    void OnGUI()
	{
   
        // RotateGUIElements();
         DrawCharacterButtons();

		if (loadLevel != "CharacterSelection")
		{
      
            startGame = true;
			//Tallennetaan pelaajan valinta controllliin, _player1Selection, _player2Selection
			//GC.GetCntr().scene.DetermineJourney();

			//GC.GetCntr().singlePlayerData = new PlayerData();
			//GC.GetCntr().singlePlayerData.InsertTestData();
            
        }
    }


    void Update()
    {
        if (startGame)
        {
           
          //  fadeManager.Fade(true, 1.25f);

            SceneManager.LoadScene("Game"); //keep it simple: Game scene will call GC.GetCntr().StartGame(); 

            // fadeManager.Fade(false, 3.0f);
            //  StartCoroutine(WaitCurtainToLower());
        }
    }


    public void DrawCharacterButtons()
	{
        int rowX = 30;
        int rowY = 12;
        int ii = 0;

            for (int x = 0; x < rowX; x++)
            {
            for (int y = 0; y < rowY; y++)
            {

                if (ii < _list.Count - 1 - 5) //-5 natimpi rivi
                {
                    // Rotation correction
                    int xRot = 100;
                    int yRot = 50;

                    //(Screen.width / 2, Screen.height / 2);
                    if (GUI.Button(new Rect(5 + xRot +(x * _iconWidth), 5 + yRot + y * _iconHeight, _iconWidth, _iconHeight),
                    new GUIContent(_list[ii].Icon, _list[ii].GetToolTip())))
                    {
                        GC.GetCntr().InitPlayerCount(2);
                        GC.GetCntr().players[1] = _list[ii];
                        GC.GetCntr().players[1].PlayerData.Name = _list[ii].Name;
                        loadLevel = "Tutorial";
                    }
                    
                    ii++;
                }
             }
        }

        SetToolTip();
		DisplayButtonToolTip();
	}

    //private void RotateGUIElements()
    //{
    //    float rotAngle = 20;
    //    Vector2 pivotPoint;
    //    pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    //    GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
       
    //}
    

	
	private void SetToolTip()
	{
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
		{
			if(_toolTip != "")
			{_toolTip = "";}
			
			if(GUI.tooltip != "")
			{_toolTip = GUI.tooltip;}
		}
	}
	
	private void DisplayButtonToolTip()
	{
		if(_toolTip != "")
		{
			buttonPos = Event.current.mousePosition;
			GUI.Box(new Rect(buttonPos.x, buttonPos.y, 120, 150), _toolTip);
		}
	}
}











