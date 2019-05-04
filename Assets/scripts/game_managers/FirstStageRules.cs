using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageRules : GameRules
{
    protected override void checkRules()
    {
        int numCalangos = registry.getCalangosList().Count;
        if (!gameOver && numCalangos <= 0)
        {
            game_over();
        }
        else if (!success && numCalangos >= 300)
        {
            gameSuccess();
        }
    }

    protected override void updateObjectives()
    {
        int numCalangos = registry.getCalangosList().Count;
        objectiveText.text = "1. Aumentar a população de calangos para 300 ou mais. Atualmente: " + numCalangos + "/300";
    }
}
