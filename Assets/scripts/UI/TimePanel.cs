using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour {

    public Text textDayValue;
    public Image ponteiro;
    TemporalManager tempManager;

    // Use this for initialization
    void Start ()
    {
        GameObject mapController = GameObject.Find("MapController");
        tempManager = mapController.GetComponent<TemporalManager>();
    }
	
	// Update is called once per frame
	void Update () {
        textDayValue.text = tempManager.getDay().ToString();
        ponteiro.rectTransform.rotation = Quaternion.Euler(0,0,360 - tempManager.getHour() * 15);
    }
}
