using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFase2 : Tutorial
{
    //public GameObject calangosPlaceholder;
    public Image painelRecursos;
    public Image highlight;
    public Image tutorialinstanceButton;
    bool isColorChangedPR;

    float counter = 0;
    float colorSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {
        base.Start();

        string[] initialText = new string[] { "Muito bem! Sua população cresceu.", "Mas, cuidado para ela não crescer demais!", "Alimentos estão sobrando e a população irá crescer de forma desequilibrada!", "Algumas espécies disputam por alimentos e podem manter sua população controlada", "Insira espécies concorrentes para controlar o crescimento de lagartos"};

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
               
                highlight.rectTransform.sizeDelta = tutorialinstanceButton.rectTransform.sizeDelta * new Vector2(1, 1);
                highlight.rectTransform.position = tutorialinstanceButton.rectTransform.position;
                break;
        }
    }

    protected override void FinishPresentation()
    {
        base.FinishPresentation();
        highlight.gameObject.SetActive(false);
    }

}