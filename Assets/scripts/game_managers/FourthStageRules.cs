﻿using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthStageRules : GameRules
{
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
            gameSuccess();
        }
    }

    protected override void updateObjectives()
    {
        int numCalangos = registry.getCalangosList().Count;

        string textColorCalangos = "<color=#008000ff>";
        if (numCalangos < numObjetivo * 0.75f)
        {
            textColorCalangos = "<color=#800000ff>";
        }
        else if (numCalangos < numObjetivo)
        {
            textColorCalangos = "<color=#ffa500ff>";
        }
        objectiveText.text = string.Format(GameTextController.getText(LanguageConstants.FOURTH_PHASE_OBJ), numObjetivo, numDays, textColorCalangos + numCalangos);
    }
}

