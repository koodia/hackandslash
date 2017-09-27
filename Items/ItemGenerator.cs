using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class ItemGenerator
{
    public const int BASE_MELEE_RANGE = 1;
    //public const int BASE_MELEE_RANGE = 5;
    private const string WEAPON_MELEE_PATH = "Weapon icons/Melee/";
    private const string WEAPON_RANGED_PATH = "Weapon icons/Ranged/";
    private const string WEAPON_MAGIC_PATH = "Weapon icons/Magic/";


    public static Item GreateItem(ItemTypes type, string subType)
    {
        Item newItem = new Item();

        switch (type)
        {
            case ItemTypes.Armor:
                newItem = CreateArmor();
                break;
            case ItemTypes.Artifact:
                newItem = CreateArtifact();
                break;
            case ItemTypes.Consumable:
                newItem = CreateConsumable();
                break;
            case ItemTypes.Weapon:

                        if (subType == "Melee")
                        {
                            newItem = CreateWeapon(WEAPON_MELEE_PATH); //muista oikeesti CreateWeapon?
                        }
                        else if (subType == "Ranged")
                        {
                            newItem = CreateWeapon(WEAPON_RANGED_PATH);
                        }
                        else if (subType == "Magic")
                        {
                            newItem = CreateWeapon(WEAPON_MAGIC_PATH);
                        }
                        else
                        {
                            throw new ArgumentException("subType is not supported:" + subType);
                        }
                break;
            default:
                throw new Exception("There is no such type in ItemType enum list: " + type);

        }

        //item.Rarety = RarityTypes.Common;
        //item.Name = item.Name + "Sword";

        return newItem;
    }

    //public static Weapon CreateWeapon(ItemTypes typeOfWeapon) 
    //{
    //	Weapon weapon = CreateMeleeWeapon();
    //	return weapon;
    //}

    private static Weapon CreateWeapon(string subType)
    {
        Weapon meleeWeapon = new Weapon();
        string[] weaponNames = Help.GetFileNames(Application.dataPath + "/Resources/" + subType, "*.png");
        if (weaponNames == null)
        {
            Debug.Log("ERROR, weaponNames are null");
        }

        //meleeWeapon.Name = Path.GetFileNameWithoutExtension(weaponNames[GC.rand.rnd.Next(0, weaponNames.Length)]);
        int coinToss = GC.rand.rnd.Next(0, 3);
        if(coinToss == 0)
        {
            meleeWeapon.Name = "Recurve_Bow 1";

        }
        else if (coinToss == 1)
        {
            meleeWeapon.Name = "Infinity_Edge";
        }
        else
        {
            meleeWeapon.Name = "Infinity_Edge";
        }


        meleeWeapon.PrefabName = meleeWeapon.Name; //tarkkana kuin porkkana
        meleeWeapon.PrefabPath = "Items/Item/";
        meleeWeapon.MaxDamage = 5;
        meleeWeapon.DamageVarience = 10f;
        meleeWeapon.MaxRange = BASE_MELEE_RANGE;
        meleeWeapon.TypeOfDamage = DamageType.Neutral;
        meleeWeapon.BaseAccuracity = 10; // 1(10%) to 10(100%)
        meleeWeapon.Icon = Resources.Load(WEAPON_MELEE_PATH + meleeWeapon.Name) as Texture2D;
        meleeWeapon.FFMode = Item.ffMode.OnlyEnemy;

        return meleeWeapon;
    }


    private static Weapon CreateArmor()
    {
        Weapon meleeWeapon = new Weapon();
        string[] weaponNames = Help.GetFileNames(Application.dataPath + "/Resources/" + WEAPON_MELEE_PATH, "*.png"); //HUOM VAIHDA PATH
        if (weaponNames == null)
        {
            Debug.Log("ERROR, weaponNames are null");
        }

        meleeWeapon.Name = Path.GetFileNameWithoutExtension(weaponNames[GC.rand.rnd.Next(0, weaponNames.Length)]);
        meleeWeapon.PrefabName = meleeWeapon.Name; //tarkkana kuin porkkana
        meleeWeapon.PrefabPath = "Items/Item/";
        meleeWeapon.MaxDamage = 5;
        meleeWeapon.DamageVarience = 10f;
        meleeWeapon.MaxRange = BASE_MELEE_RANGE;
        meleeWeapon.TypeOfDamage = DamageType.Neutral;
        meleeWeapon.BaseAccuracity = 10; // 1(10%) to 10(100%)
        meleeWeapon.Icon = Resources.Load(WEAPON_MELEE_PATH + meleeWeapon.Name) as Texture2D;
        meleeWeapon.FFMode = Item.ffMode.OnlyEnemy;

        return meleeWeapon;
    }

    private static Weapon CreateArtifact()
    {
        Weapon meleeWeapon = new Weapon();
        string[] weaponNames = Help.GetFileNames(Application.dataPath + "/Resources/" + WEAPON_MELEE_PATH, "*.png"); //HUOM VAIHDA PATH
        if (weaponNames == null)
        {
            Debug.Log("ERROR, weaponNames are null");
        }

        meleeWeapon.Name = Path.GetFileNameWithoutExtension(weaponNames[GC.rand.rnd.Next(0, weaponNames.Length)]);
        meleeWeapon.PrefabName = meleeWeapon.Name; //tarkkana kuin porkkana
        meleeWeapon.PrefabPath = "Items/Item/";
        meleeWeapon.MaxDamage = 5;
        meleeWeapon.DamageVarience = 10f;
        meleeWeapon.MaxRange = BASE_MELEE_RANGE;
        meleeWeapon.TypeOfDamage = DamageType.Neutral;
        meleeWeapon.BaseAccuracity = 10; // 1(10%) to 10(100%)
        meleeWeapon.Icon = Resources.Load(WEAPON_MELEE_PATH + meleeWeapon.Name) as Texture2D;
        meleeWeapon.FFMode = Item.ffMode.OnlyEnemy;

        return meleeWeapon;
    }

    private static Weapon CreateConsumable()
    {
        Weapon meleeWeapon = new Weapon();
        string[] weaponNames = Help.GetFileNames(Application.dataPath + "/Resources/" + WEAPON_MELEE_PATH, "*.png"); //HUOM VAIHDA PATH
        if (weaponNames == null)
        {
            Debug.Log("ERROR, weaponNames are null");
        }

        meleeWeapon.Name = Path.GetFileNameWithoutExtension(weaponNames[GC.rand.rnd.Next(0, weaponNames.Length)]);
        meleeWeapon.PrefabName = meleeWeapon.Name; //tarkkana kuin porkkana
        meleeWeapon.PrefabPath = "Items/Consumables/";
        meleeWeapon.MaxDamage = 5;
        meleeWeapon.DamageVarience = 10f;
        meleeWeapon.MaxRange = BASE_MELEE_RANGE;
        meleeWeapon.TypeOfDamage = DamageType.Neutral;
        meleeWeapon.BaseAccuracity = 10; // 1(10%) to 10(100%)
        meleeWeapon.Icon = Resources.Load(WEAPON_MELEE_PATH + meleeWeapon.Name) as Texture2D;
        meleeWeapon.FFMode = Item.ffMode.OnlyEnemy;

        return meleeWeapon;
    }


    //private static string GetCorrespondingMeleePrefabName(ItemTypes type)
    //{
    //    string prefabName = "";
    //    switch (type)
    //    {
    //        case ItemTypes.Weapon:
    //             prefabName = ObstacleType.Box.ToString();
    //            break;
    //        case ObstacleType.Wall:
    //            prefabName = ObstacleType.Wall.ToString();
    //            break;
    //        default:
    //            throw new Exception("There is no such type in ObstacleType enum list: " + obstacleType);

    //    }

    //    return prefabName;
    //}

}
