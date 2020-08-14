using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFase1 : Tutorial
{
    //public GameObject calangosPlaceholder;
    public Image controls;
    public Image painelRecursos;
    public Image highlight;
    public Image painelInstancias;
    public Image painelInstanciasinfo;
    public instance_button tutorialinstanceButton;
    bool isColorChangedPR;

    float counter = 0;
    float colorSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {
        base.Start();
        
        string[] initialText = new string[15];
        for (int i=0; i<initialText.Length-1; i++)
        {
            if(i==3) initialText[i] = string.Format(GameTextController.getText(LanguageConstants.TUTORIAL_1_TEXT + (i + 1)), mapController.GetComponent<FirstStageRules>().NumObjetivo);
            else initialText[i] = GameTextController.getText(LanguageConstants.TUTORIAL_1_TEXT + (i+1));
        }
        initialText[initialText.Length - 1] = "";
        PresentText(initialText);
    }

    protected override void GoToNextText()
    {
        base.GoToNextText();
        counter = 0;
        float cameraOffset = 2;

        switch (curPresentationPosition)
        {
            case 5:
                professor.enabled = false;
                mouse.enabled = false;
                board.rectTransform.sizeDelta = new Vector2(340, 500);
                board.rectTransform.anchoredPosition = new Vector2(-170, 0);
                overrideBoardPosition = true;

                highlight.rectTransform.sizeDelta = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                highlight.rectTransform.position = new Vector3(Screen.width * 0.5f-cameraOffset, Screen.height * 0.5f, 0);
                highlight.gameObject.SetActive(true);
                break;
            case 6:
                board.rectTransform.sizeDelta = new Vector2(500, 500);
                board.rectTransform.anchoredPosition = new Vector2(-250, 0);
                overrideBoardPosition = false;

                highlight.rectTransform.sizeDelta = painelRecursos.rectTransform.sizeDelta * new Vector2(1, 2);
                highlight.rectTransform.position = painelRecursos.rectTransform.position;
                break;
            case 9:
                highlight.rectTransform.sizeDelta = painelInstancias.rectTransform.sizeDelta * new Vector2(1.6f, 1.2f);
                highlight.rectTransform.position = painelInstancias.rectTransform.position;
                break;
            case 10:
                highlight.rectTransform.sizeDelta = painelInstanciasinfo.rectTransform.sizeDelta * new Vector2(1.6f, 1.6f);
                highlight.rectTransform.position = painelInstanciasinfo.rectTransform.position;
                tutorialinstanceButton.setText();
                painelInstanciasinfo.gameObject.SetActive(true);
                break;
            case 14:
                controls.gameObject.SetActive(true);
                break;
        }
    }

    protected override void FinishPresentation()
    {
        base.FinishPresentation();
        painelInstanciasinfo.gameObject.SetActive(false);
        highlight.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
    }
}