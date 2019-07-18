using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants {
    internal static float temperatureInSun;

    public class states
    {
        public const string IDDLE = "Repouso";
        public const string TRYTOMATE = "Tentando acasalar";
        public const string SEARCHINGFOOD = "Procurando comida";
        public const string TRYTOEAT = "Tentando comer";
        public const string SEARCHINGSHADOW = "Procurando sombra";
        public const string RUNNINGTOSHADOW = "Correndo para a sombra";
        public const string COOLING = "Refrescando";
        public const string RUNNINGFROMPREDATOR = "Fugindo de predadores";
        public const string HIDDING = "Escondendo-se";
        public const string RUNNINGFROMCOMPETITOR = "Fugindo de competidor";
        public const string ENGAGING = "Batalhando";
    }
    
}
