using Assets.scripts.game_managers;
using UnityEngine;
using UnityEngine.UI;

public class SandBoxRules : GameRules
{
    public Text textoSucesso;
    int numObjetivoPrimario = 150;
    int numLimiarInferior = 50;
    int numLimiarSuperior = 300;
    int numDays = 5;

    float counter = 0;

    bool SecondPhase = false;

    protected override void checkRules()
    {
        int numCalangos = registry.getCalangosList().Count;

        if (numCalangos <= 0)
        {
            game_over();
        } else if (SecondPhase)
        {
            if (numCalangos > numLimiarSuperior || numCalangos < numLimiarInferior)
            {
                finishSandbox();
            } else
            {
                counter += Time.deltaTime;
            }
        } else
        {  
            if (numCalangos >= numObjetivoPrimario)
            {
                SecondPhase = true;
            }
        }        
    }

    protected override void updateObjectives()
    {
        int numCalangos = registry.getCalangosList().Count;

        if (SecondPhase)
        {
            string textColorCalangos = "<color=#008000ff>";
            if (numCalangos < numLimiarInferior * 1.5f || numCalangos > numLimiarSuperior* 0.85f)
            {
                textColorCalangos = "<color=#800000ff>";
            }
            else if (numCalangos < numLimiarInferior * 2f || numCalangos > numLimiarSuperior * 0.7f)
            {
                textColorCalangos = "<color=#ffa500ff>";
            }
            objectiveText.text = string.Format(GameTextController.getText(LanguageConstants.SAND_BOX_OBJ_1), numLimiarSuperior, numLimiarInferior,textColorCalangos + numCalangos);
        } else
        {
            string textColorCalangos = "<color=#008000ff>";
            if (numCalangos < numObjetivoPrimario / 2)
            {
                textColorCalangos = "<color=#800000ff>";
            }
            else if (numCalangos < numObjetivoPrimario * 0.75f)
            {
                textColorCalangos = "<color=#ffa500ff>";
            }
            objectiveText.text = string.Format(GameTextController.getText(LanguageConstants.SAND_BOX_OBJ_2), numObjetivoPrimario, textColorCalangos + numCalangos);
        }
    }

    public void finishSandbox()
    {
        gameSuccess();
        int years = Mathf.FloorToInt(counter / timeManager.secondsForADay);
        int months = Mathf.FloorToInt(counter / timeManager.secondsForADay);
        textoSucesso.text = string.Format(GameTextController.getText(LanguageConstants.SAND_BOX_END_TXT), years, months);
    }
}

