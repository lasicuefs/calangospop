using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelGeralControler : MonoBehaviour {

	public Text textCalangos;
	public Text textEdiblePlants;
    public Text textInsects;
    public Text textPredators;
    public Text textDay;
	public Text textTime;

    public Text textStarvation;
    public Text textPredation;
    registryController registry;
	temporalManager tempManager;

	// Use this for initialization
	void Start () {
		GameObject mapController = GameObject.Find ("MapController");
		registry = mapController.GetComponent<registryController> ();
		tempManager =  mapController.GetComponent<temporalManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//ebug.Log (registry.getCalangosList());
		textCalangos.text = "Calangos: " + registry.getCalangosList().Count;
		textEdiblePlants.text = "Plantas: " + registry.getediblePlantsList().Count;
        textInsects.text = "Insetos: " + registry.getInsectCount();
        textPredators.text = "Predadores: " + registry.getPredatorList().Count;


        textStarvation.text = "Fome: " + registry.getDeathsByStarvation();
        textPredation.text = "Predação: " + registry.getDeathsByPredation();

        textDay.text = "Dias: " + tempManager.getDay();
		textTime.text = "Horas: " + tempManager.getHour()+":00";
	}
}
