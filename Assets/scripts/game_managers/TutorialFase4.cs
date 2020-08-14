using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFase4 : Tutorial
{
    //public GameObject calangosPlaceholder;
    public Image highlight;
    public Image tutorialinstanceButton;
    bool isColorChangedPR;

    float counter = 0;
    float colorSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {
        base.Start();
        string[] initialText = new string[4];
        for (int i = 0; i < initialText.Length; i++)
        {
            initialText[i] = GameTextController.getText(LanguageConstants.TUTORIAL_4_TEXT + (i + 1));
        }

        PresentText(initialText);
    }

    protected override void GoToNextText()
    {
        base.GoToNextText();
        counter = 0;

        switch (curPresentationPosition)
        {
            case 3:
                professor.enabled = false;
                mouse.enabled = false;
                board.rectTransform.sizeDelta = new Vector2(500, 500);
                highlight.gameObject.SetActive(true);

                highlight.rectTransform.sizeDelta = tutorialinstanceButton.rectTransform.sizeDelta * new Vector2(1, 1.5f);
                highlight.rectTransform.position = tutorialinstanceButton.rectTransform.position ;
                break;
        }
    }

    protected override void FinishPresentation()
    {
        base.FinishPresentation();
        highlight.gameObject.SetActive(false);
    }

}