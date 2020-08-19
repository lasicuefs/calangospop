using Assets.scripts.game_managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameTextController : MonoBehaviour {

    private static string savedDataPath = Application.dataPath + "/languages";
    static Dictionary<string, string> portugues = initializePortugues();
    static Dictionary<string, string> english = initializeEnglish();

    

    enum Lang
    {
        portuguese, english
    }

    internal static string getText(object p)
    {
        throw new NotImplementedException();
    }

    private static Dictionary<string, string> initializePortugues()
    {
        Portuguese portuguese = new Portuguese();

        return portuguese.buildDictionary();
    }


    private static Dictionary<string, string> initializeEnglish()
    {
        English english = new English();

        return english.buildDictionary();
    }

    static public string getText(string textName)
    {     
        if (PlayerPrefs.GetInt(GameConstants.LANGUAGE, 1) == 1) return portugues[textName.ToLower()];
        else return english[textName.ToLower()]; 
    }
}
