using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStageRules : GameRules
{

    protected override void checkRules()
    {
       
    int numCalangos = registry.getCalangosList().Count;
        if (!gameOver && numCalangos > 700)
        {
            game_over();
        }
        else if (!success && numCalangos <= 100)
        {
            gameSuccess();
        }
    }

    protected override void updateObjectives()
    {
        int numCalangos = registry.getCalangosList().Count;
        objectiveText.text = "1. Reduzir a população de calangos para 100 ou menos. Atualmente: " + numCalangos + "\n2. Não deixar a população de calangos passar de 700 indivíduos.";
    }
}
