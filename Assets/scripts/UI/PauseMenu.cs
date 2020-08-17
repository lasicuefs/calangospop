using Assets.scripts.game_managers;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Button menuButton;
    public Button restartButton;
    float lasTimeScale = 1;

    LevelManager levelManager;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        menuButton.GetComponentInChildren<Text>().text = GameTextController.getText(LanguageConstants.MAIN_MENU);
        menuButton.onClick.AddListener(delegate () { levelManager.toMainMenu(); });

        restartButton.GetComponentInChildren<Text>().text = GameTextController.getText(LanguageConstants.RESTART);
        restartButton.onClick.AddListener(delegate () { levelManager.restart(); });
    }
    

    private void OnEnable()
    {
        lasTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = lasTimeScale;
    }


}
