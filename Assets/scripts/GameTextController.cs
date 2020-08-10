using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTextController : MonoBehaviour {

    static Dictionary<string, string> portugues = initializePortugues();
    static Dictionary<string, string> english = initializeEnglish();


    enum Lang
    {
        portuguese, english
    }

    static Lang selectedLang = Lang.english;

    private static Dictionary<string, string> initializePortugues()
    {
        Dictionary<string, string> portugues = new Dictionary<string, string>();
        portugues.Add("lizard_name", "Calangos");
        portugues.Add("predator_name", "Predadores");
        portugues.Add("competitor_name", "Sapos");
        portugues.Add("food_name", "Vegetação");

        return portugues;
    }


    private static Dictionary<string, string> initializeEnglish()
    {
        Dictionary<string, string> english = new Dictionary<string, string>();
        english.Add("lizard_name", "Lizards");
        english.Add("predator_name", "Predators");
        english.Add("competitor_name", "Competitors");
        english.Add("food_name", "Lizard's Food Source");

        return english;
    }

    static public string getText(string textName)
    {
        if(selectedLang.Equals(Lang.english)) return english[textName];
        else return portugues[textName];
    }
}
