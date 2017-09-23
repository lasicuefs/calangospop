using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelGeralControler : MonoBehaviour {

	public Text textCalangos;
	public Text textEdiblePlants;
	public Text textDay;
	public Text textTime;
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
		textEdiblePlants.text = "Plants: " + registry.getediblePlantsList().Count;

		textDay.text = "Day " + tempManager.getDay();
		textTime.text = "Hour " + tempManager.getHour()+":00";
	}
}
