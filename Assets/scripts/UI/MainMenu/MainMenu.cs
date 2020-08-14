using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text buttonTutorial1;
    public Text buttonTutorial2;
    public Text buttonTutorial3;
    public Text buttonTutorial4;
    public Text buttonSandBox;
    
    void Update() {
        buttonTutorial1.text = GameTextController.getText(LanguageConstants.GUI_BUTTON_TUTORIAL_1);
        buttonTutorial2.text = GameTextController.getText(LanguageConstants.GUI_BUTTON_TUTORIAL_2);
        buttonTutorial3.text = GameTextController.getText(LanguageConstants.GUI_BUTTON_TUTORIAL_3);
        buttonTutorial4.text = GameTextController.getText(LanguageConstants.GUI_BUTTON_TUTORIAL_4);
        buttonSandBox.text = GameTextController.getText(LanguageConstants.GUI_BUTTON_SAND_BOX);
    }

    public void StartTutorial1()
    {
        SceneManager.LoadScene("Tutorial1", LoadSceneMode.Single);
    }

    public void StartTutorial2()
    {
        SceneManager.LoadScene("Tutorial2", LoadSceneMode.Single);
    }

    public void StartTutorial3()
    {
        SceneManager.LoadScene("Tutorial3", LoadSceneMode.Single);
    }

    public void StartTutorial4()
    {
        SceneManager.LoadScene("Tutorial4", LoadSceneMode.Single);
    }

    public void StartSandBox()
    {
        SceneManager.LoadScene("SandBox", LoadSceneMode.Single);
    }
}
