using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelGeralControler : MonoBehaviour {

	public Text textCalangos;
	public Text textEdiblePlants;
    public Text textInsects;
    public Text textCompetitor;
    public Text textPredators;

    public Text textStarvation;   
    public Text textAge;
    public Text textPredation;
    public Text textHeat;
    registryController registry;

	// Use this for initialization
	void Start () {
		GameObject mapController = GameObject.Find ("MapController");
		registry = mapController.GetComponent<registryController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//ebug.Log (registry.getCalangosList());
		textCalangos.text = "Calangos: " + registry.getCalangosList().Count;
		textEdiblePlants.text = "Plantas: " + registry.getediblePlantsList().Count;
        textInsects.text = "Insetos: " + registry.getInsectCount();
        if (textPredators != null)  textPredators.text = "Predadores: " + registry.getPredatorList().Count;
        if (textCompetitor != null) textCompetitor.text = "Competidores: " + registry.getCompetitorList().Count;


        textStarvation.text = "Fome: " + registry.getDeathsByStarvation();
        textAge.text = "Idade: " + registry.getDeathsByAge();
        if(textPredation != null) textPredation.text = "Predação: " + registry.getDeathsByPredation();
        if(textHeat != null) textHeat.text = "Insolação: " + registry.getDeathsByHeat();
        
    }
}
