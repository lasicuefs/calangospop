using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorial : MonoBehaviour {

    public Image professor;
    public Image board;
    public Image mouse;

    public Text boardText;

    Image background;
    TemporalManager tempManager;
    bool onTextPresentation;
    string[] curPresentationText;
    int curPresentationLength;
    protected int curPresentationPosition;

    float originalXProfessor;
    float originalXBoard;
    float dislocatedXProfessor = -1000;
    float dislocatedXBoard = 2000;

    protected void Start ()
    {
        GameObject mapController = GameObject.Find("MapController");
        tempManager = mapController.GetComponent<TemporalManager>();
        background = GetComponent<Image>();

        originalXProfessor = professor.rectTransform.localPosition.x;
        originalXBoard = board.rectTransform.localPosition.x;
        professor.rectTransform.localPosition = new Vector3(dislocatedXProfessor, professor.rectTransform.localPosition.y, professor.rectTransform.localPosition.z);
        board.rectTransform.localPosition = new Vector3(dislocatedXBoard, board.rectTransform.localPosition.y, board.rectTransform.localPosition.z);

        professor.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        mouse.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    protected void Update () {

        if (onTextPresentation)
        {
            professor.rectTransform.localPosition = Vector3.Lerp(professor.rectTransform.localPosition, new Vector3(originalXProfessor, professor.rectTransform.localPosition.y, professor.rectTransform.localPosition.z),.05f);
            board.rectTransform.localPosition = Vector3.Lerp(board.rectTransform.localPosition, new Vector3(originalXBoard, board.rectTransform.localPosition.y, board.rectTransform.localPosition.z), .05f);


            if (Input.GetMouseButtonDown(0))
            {
                GoToNextText();
            }
        } else
        {
            professor.rectTransform.localPosition = new Vector3(dislocatedXProfessor, professor.rectTransform.localPosition.y, professor.rectTransform.localPosition.z);
            board.rectTransform.localPosition = new Vector3(dislocatedXBoard, board.rectTransform.localPosition.y, board.rectTransform.localPosition.z);
        }

    }

    protected void PresentText(string[] texts)
    {
        curPresentationText = texts;
        curPresentationLength = texts.Length;
        curPresentationPosition = 0;
        onTextPresentation = true;
        tempManager.setTimeSpeed(0);
        background.enabled = true;
        ShowText();
    }

    protected void ShowText()
    {
        boardText.text = curPresentationText[curPresentationPosition];
    }

    protected virtual void GoToNextText()
    {
        curPresentationPosition++;
        if(curPresentationPosition == curPresentationLength)
        {
            FinishPresentation();
        } else
        {
            ShowText();
        }
        
    }

    protected virtual void FinishPresentation()
    {
        tempManager.setTimeSpeed(1);
        onTextPresentation = false;
        background.enabled = false;
    }
}
