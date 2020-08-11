using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour {

    public Text textYear;
    public Text textYearValue;
    public Image ponteiro;
    TemporalManager tempManager;

    // Use this for initialization
    void Start ()
    {
        GameObject mapController = GameObject.Find("MapController");
        tempManager = mapController.GetComponent<TemporalManager>();
        textYear.text = GameTextController.getText(LanguageConstants.YEARS);
    }
	
	// Update is called once per frame
	void Update () {
        textYearValue.text = tempManager.getDay().ToString();
        ponteiro.rectTransform.rotation = Quaternion.Euler(0,0,360 - tempManager.getHour() * 15);
    }
}
