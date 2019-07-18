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
        objectiveText.text = "1. Aumentar a população de calangos para "+ numObjetivo + " ou mais antes de " + numDays + " anos. Atualmente: " + textColorCalangos  + numCalangos+" </color>/" + numObjetivo;
    }

    public void finishFirstPhase()
    {
        gameSuccess();
        int years = timeManager.getDay();
        int months = timeManager.getHour()/2;
        textoSucesso.text = "Você conseguiu crescer sua população para 100 ou mais em por " + years + " anos e" + months + " meses!";
    }
}
