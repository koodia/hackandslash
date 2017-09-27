using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


//Käytä vaikka:
//http://www.convertcsv.com/csv-to-json.htm
public class JsonReader : MonoBehaviour
{

    public List<GenericCharacter> LoadHeroList()
    {
        List<GenericCharacter> finalList = new List<GenericCharacter>();

        var jsonHroList = LoadJsonHeroList();
        foreach (JsonHero e in jsonHroList)
        {
            GenericCharacter newHero =  new GenericCharacter();
            newHero.Name = e.NAME;
            newHero.World = e.WORLD;
            newHero.DmgMin =  e.DMG_MIN;
            newHero.DmgMax = e.DMG_MAX;
            newHero.Hp = e.HP;
            newHero.Sparks = GC.rand.rnd.Next(0, 100);
            newHero.Class = e.CLASS;
            newHero.Mastery = e.MASTERY;

            finalList.Add(newHero);
        }

        return finalList;
    }


    public List<JsonHero> LoadJsonHeroList()
    {
        List<JsonHero> heroList;
        string jsonString = File.ReadAllText(Application.dataPath + "/Data/Json/herolist.json");
        if (String.IsNullOrEmpty(jsonString))
        {
            Debug.LogWarning("Could not load jsonString from file herolist.json");
        }

        heroList = CreateJsonHeroList(jsonString);
        if (heroList == null)
        {
            Debug.LogWarning("Could not load LoadHeroList, herolist is null");
        }
        return heroList;
    }

    public List<JsonHero> CreateJsonHeroList(string json)
    {
        List<JsonHero> heroList = new List<JsonHero>();
       
        try
        {
            heroList = JsonConvert.DeserializeObject<List<JsonHero>>(json);
        }
        catch (Exception e)
        {
            Debug.LogWarning("The file could not be read:" + e.Message);
        }
        return heroList;
    }
}