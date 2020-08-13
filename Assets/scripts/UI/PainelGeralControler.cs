using Assets.scripts.game_managers;
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

    public Text deathCauses;

    public Text textStarvation;   
    public Text textAge;
    public Text textPredation;
    public Text textHeat;
    RegistryController registry;

	// Use this for initialization
	void Start () {
		GameObject mapController = GameObject.Find ("MapController");
		registry = mapController.GetComponent<RegistryController> ();
    }
	
	// Update is called once per frame
	void Update () {
        //ebug.Log (registry.getCalangosList());
        deathCauses.text = GameTextController.getText(LanguageConstants.DEATH_CAUSES);
        textCalangos.text = GameTextController.getText(LanguageConstants.LIZARD_NAME)+": " + registry.getCalangosList().Count;
		textEdiblePlants.text = GameTextController.getText(LanguageConstants.PLANT_NAME) + ": " + registry.getediblePlantsList().Count;
        textInsects.text = GameTextController.getText(LanguageConstants.INSECT_NAME) + ": " + registry.getInsectCount();
        if (textPredators != null)  textPredators.text = GameTextController.getText(LanguageConstants.PREDATOR_NAME) + ": " + registry.getPredatorList().Count;
        if (textCompetitor != null) textCompetitor.text = GameTextController.getText(LanguageConstants.COMPETITOR_NAME) + ": " + registry.getCompetitorList().Count;


        textStarvation.text = GameTextController.getText(LanguageConstants.HUNGER)+": " + registry.getDeathsByStarvation();
        textAge.text = GameTextController.getText(LanguageConstants.AGE) + ": " + registry.getDeathsByAge();
        if(textPredation != null) textPredation.text = GameTextController.getText(LanguageConstants.PREDATION) + ": " + registry.getDeathsByPredation();
        if(textHeat != null) textHeat.text = GameTextController.getText(LanguageConstants.INSOLATION) + ": " + registry.getDeathsByHeat();
        
    }
}
