using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFase1 : Tutorial
{
    //public GameObject calangosPlaceholder;
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

        string[] initialText = new string[] { "Oi, Bem vido ao Calangos", "Eu sou o professor X e estou aqui para lhe ajudar a aprender!",
            "Esta é sua população de lagartos", "Neste tutorial, devemos garantir que ela aumente de tamanho até atingir 100 indivíduos!",
            "Mas antes, vamos aprender mais sobre o jogo", "Como já disse, aqui está sua população", "Esta é a barra de Biomassa, e mostra quanto você tem de Biomassa no momento",
            "sua biomassa aumenta com o tempo", "Utilizando a Biomassa você pode adicionar novas espécies de plantas ao ambiente", "Aqui voce pode escolher quais espécies adicionar",
            "Cada espécie apresenta característias próprias e tem um custo de biomassa", "Só poderá inserir uma espécie se tiver a biomassa exigida"};

        PresentText(initialText);
    }

    protected override void GoToNextText()
    {
        base.GoToNextText();
        counter = 0;

        switch (curPresentationPosition)
        {
            case 5:
                professor.enabled = false;
                mouse.enabled = false;
                board.rectTransform.sizeDelta = new Vector2(500, 500);

                highlight.rectTransform.sizeDelta = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                highlight.rectTransform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
                highlight.gameObject.SetActive(true);
                break;
            case 6:
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
        }
    }

    protected override void FinishPresentation()
    {
        base.FinishPresentation();
        painelInstanciasinfo.gameObject.SetActive(false);
        highlight.gameObject.SetActive(false);
    }

    /*protected override void Update()
    {
        base.Update();
        switch (curPresentationPosition)
        {
            case 6:
                alternateResourceBar();
                break;


        }
    }

    void alternateResourceBar()
    {
        counter += Time.deltaTime;
        if (counter > colorSpeed)
        {
            painelRecursos.color = isColorChangedPR ? painelRecursosColor : Color.red;
            isColorChangedPR = !isColorChangedPR;
            counter = 0;
        }       
    }*/
}