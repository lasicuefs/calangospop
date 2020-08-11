using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFase3 : Tutorial
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

        string[] initialText = new string[] { "Atenção! A população de lagartos continua a se expandir...", "Precisamo de medidas mais radicais!", "Podemos inserir espécies Predadoras para reduzir sua população" };

        PresentText(initialText);
    }

    protected override void GoToNextText()
    {
        base.GoToNextText();
        counter = 0;

        switch (curPresentationPosition)
        {
            case 2:
                professor.enabled = false;
                mouse.enabled = false;
                board.rectTransform.sizeDelta = new Vector2(500, 500);
                highlight.gameObject.SetActive(true);

                highlight.rectTransform.sizeDelta = tutorialinstanceButton.rectTransform.sizeDelta * new Vector2(2, 1.5f);
                highlight.rectTransform.position = tutorialinstanceButton.rectTransform.position - new Vector3(-tutorialinstanceButton.rectTransform.sizeDelta.x / 2, 0, 0);
                break;
        }
    }

    protected override void FinishPresentation()
    {
        base.FinishPresentation();
        highlight.gameObject.SetActive(false);
    }

}