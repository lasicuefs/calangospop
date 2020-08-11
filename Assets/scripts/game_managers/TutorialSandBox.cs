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

        string[] initialText = new string[] { "Parabéns! Você agora já sabe tudo sobre o Calangos", "Agora vamos testar o que você aprendeu...", "Cresça sua população e mantenha-a estável pelo máximo de tempo possivel!" };

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
