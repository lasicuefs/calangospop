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

    private static string[] loadFile(string name)
    {
        Directory.CreateDirectory(savedDataPath);
        string[] fileContent = new string[2];
        String filePath = savedDataPath + "/" + name;
        if (File.Exists(filePath))
        {
            fileContent = File.ReadAllLines(filePath, System.Text.Encoding.Default);
        }
        return fileContent;
    }

    private static Dictionary<string, string> initializePortugues()
    {
        string[] fileContent = loadFile("portugues.txt");

        Dictionary<string, string> portugues = new Dictionary<string, string>();

        return portugues;
    }


    private static Dictionary<string, string> initializeEnglish()
    {
        string[] fileContent = loadFile("english.txt");
        Dictionary<string, string> english = new Dictionary<string, string>();

        foreach (string line in fileContent)
        {
            string[] buffer = line.Split('=');
            if (buffer.Length == 2)
                portugues.Add(buffer[0], buffer[1]);
        }
        
        return english;
    }

    static public string getText(string textName)
    {     
        if (PlayerPrefs.GetInt(GameConstants.LANGUAGE, 1) == 0) return english[textName];
        else return portugues[textName];
    }
}
