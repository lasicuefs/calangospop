using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageRules : MonoBehaviour, GameRules
{

    public GameObject gameOverMessage;
    public GameObject successMessage;
    public bool enableHeat;
    registryController registry;
    TemporalManager timeManager;

    bool gameOver = false;
    bool success = false;

    // Use this for initialization
    void Start()
    {
        registry = GetComponent<registryController>();
        timeManager = GetComponent<TemporalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int numCalangos = registry.getCalangosList().Count;
        if (!gameOver && numCalangos <= 0)
        {
            game_over();
        }
        else if (!success && numCalangos >= 500)
        {
            gameSuccess();
        }
    }

    void game_over()
    {
        gameOverMessage.SetActive(true);
        gameOver = true;
    }

    void gameSuccess()
    {
        successMessage.SetActive(true);
        success = true;
    }

    public void resetGame()
    {
        timeManager.setTimeSpeed(0);
        Application.LoadLevel(Application.loadedLevel);
    }

    public bool isHeatEnabled()
    {
        return enableHeat;
    }
}
