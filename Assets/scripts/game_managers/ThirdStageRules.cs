using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdStageRules : GameRules
{
    int numObjetivo = 200;
    int numDays = 5;

    protected override void checkRules()
    {

        int numCalangos = registry.getCalangosList().Count;
        if(timeManager.getDay() == numDays && !success && !gameOver)
        {
            if (numCalangos >= numObjetivo)
            {
                game_over();
            }
            else
            {
                gameSuccess();
            }
        }         
    }

    protected override void updateObjectives()
    {
        int numCalangos = registry.getCalangosList().Count;

        string textColorCalangos = "<color=#800000ff>";
        if (numCalangos < numObjetivo / 2)
        {
            textColorCalangos = "<color=#008000ff>";
        }
        else if (numCalangos < numObjetivo * 0.75f)
        {
            textColorCalangos = "<color=#ffa500ff>";
        }
        objectiveText.text =  string.Format(GameTextController.getText(LanguageConstants.THIRD_PHASE_OBJ),numObjetivo, numDays, textColorCalangos + numCalangos);
        //objectiveText.text = "1. Reduzir a população de calangos para 100 ou menos. Atualmente: " + numCalangos + "\n2. Não deixar a população de calangos passar de 700 indivíduos.";
    }
}

