using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameRules : MonoBehaviour {

    public GameObject gameOverMessage;
    public GameObject successMessage;
    public Text objectiveText;
    public bool enableHeat;
    public float temperatureInSun = 40;
    public float temperatureInShadow = 25;
    protected RegistryController registry;
    protected TemporalManager timeManager;

    protected bool gameOver = false;
    protected bool success = false;

    void Start()
    {
        registry = GetComponent<RegistryController>();
        timeManager = GetComponent<TemporalManager>();
    }

    void Update()
    {
        if(!gameOver && !success) checkRules();
        updateObjectives();
    }

    protected abstract void checkRules();
    protected abstract void updateObjectives();

    public void resetGame()
    {
        timeManager.setTimeSpeed(0);
        Application.LoadLevel(Application.loadedLevel);
    }

    public bool isHeatEnabled()
    {
        return enableHeat;
    }

    protected void game_over()
    {
        gameOverMessage.SetActive(true);
        gameOver = true;
    }

    protected void gameSuccess()
    {
        successMessage.SetActive(true);
        success = true;
    }
}
