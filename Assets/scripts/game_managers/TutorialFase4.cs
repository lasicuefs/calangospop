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
        
        string[] initialText = new string[] { "Cuidado! A temperatura está muito alta", "Precisamos gerar sombras para os calangos", "Muita exposição ao sol pode levar os calangos a morte!", "Insira plantas que projetem sombra para evitar que os lagartos morram" };

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