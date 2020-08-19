using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants {
    internal static float temperatureInSun;
    internal static string DIFFICULTY = "DIFFICULTY";
    internal static string LANGUAGE = "LANGUAGE";    

    public class states
    {
        public const string IDDLE = "rest";
        public const string TRYTOMATE = "trying_mate";
        public const string SEARCHINGFOOD = "looking_food";
        public const string TRYTOEAT = "trying_eat";
        public const string SEARCHINGSHADOW = "looking_shadow";
        public const string RUNNINGTOSHADOW = "running_shadow";
        public const string COOLING = "cooling";
        public const string RUNNINGFROMPREDATOR = "running_predator";
        public const string HIDDING = "hiding";
        public const string RUNNINGFROMCOMPETITOR = "running_competitor";
        public const string ENGAGING = "figthing";
    }
}
