using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.game_managers;

public class MessagesPanel : MonoBehaviour {

    public Text gameOver;
    public Text congratulationText;
    public Button restartButton;
    public Button menuButton;
    public Button nextButton;
    LevelManager levelManager;

    void Start () {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gameOver.text = GameTextController.getText(LanguageConstants.GAME_OVER);
        congratulationText.text = GameTextController.getText(LanguageConstants.CONGRATULATIONS);
        restartButton.onClick.AddListener(delegate () { levelManager.restart(); });
        restartButton.GetComponentInChildren<Text>().text = GameTextController.getText(LanguageConstants.RESTART);
        menuButton.onClick.AddListener(delegate () { levelManager.toMainMenu(); });
        menuButton.GetComponentInChildren<Text>().text = GameTextController.getText(LanguageConstants.MAIN_MENU);
        nextButton.onClick.AddListener(delegate () { levelManager.toNextLevel(); });
        nextButton.GetComponentInChildren<Text>().text = GameTextController.getText(LanguageConstants.NEXT);
    }
}
