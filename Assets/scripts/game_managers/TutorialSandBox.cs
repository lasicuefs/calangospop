using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSandBox : Tutorial
{
    bool isColorChangedPR;

    float counter = 0;
    float colorSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {
        base.Start();

        string[] initialText = new string[3];
        for (int i = 0; i < initialText.Length; i++)
        {
            initialText[i] = GameTextController.getText(LanguageConstants.TUTORIAL_SAND_BOX_TEXT + (i + 1));
        }

        PresentText(initialText);
    }

    protected override void GoToNextText()
    {
        base.GoToNextText();
        counter = 0;

    }

    protected override void FinishPresentation()
    {
        base.FinishPresentation();
    }

}
