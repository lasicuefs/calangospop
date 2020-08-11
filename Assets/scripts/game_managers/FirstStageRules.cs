using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstStageRules : GameRules
{
    public Text textoSucesso;
    int numObjetivo = 100;
    int numDays = 5;

    protected override void checkRules()
    {
        int numCalangos = registry.getCalangosList().Count;
        if (!gameOver && (numCalangos <= 0 || timeManager.getDay() == numDays))
        {
            game_over();
        }
        else if (!success && numCalangos >= numObjetivo)
        {
            finishFirstPhase();
        }
    }

    protected override void updateObjectives()
    {
        int numCalangos = registry.getCalangosList().Count;

        string textColorCalangos = "<color=#008000ff>";
        if(numCalangos< numObjetivo / 2)
        {
            textColorCalangos = "<color=#800000ff>";
        } else if(numCalangos < numObjetivo * 0.75f)
        {
            textColorCalangos = "<color=#AD7819>";
        } 
        objectiveText.text = string.Format(GameTextController.getText(LanguageConstants.FIRST_PHASE_OBJ), numObjetivo, numDays, textColorCalangos + numCalangos, numObjetivo);
    }

    public void finishFirstPhase()
    {
        gameSuccess();
        int years = timeManager.getDay();
        int months = timeManager.getHour()/2;
        textoSucesso.text = string.Format(GameTextController.getText(LanguageConstants.FIRST_PHASE_OBJ), years, months);
    }
}
