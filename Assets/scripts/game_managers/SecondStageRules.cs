using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStageRules : MonoBehaviour, GameRules
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
        if (!gameOver && numCalangos > 700)
        {
            game_over();
        }
        else if (!success && numCalangos <= 100)
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
