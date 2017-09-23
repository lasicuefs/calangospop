using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources_Controller : MonoBehaviour {

	public int starting_biomass = 100;
	public int biomassPerSecond = 10; 
	public Text biomassText;
	float counter;
	int biomass;

	// Use this for initialization
	void Start () {
		biomass = starting_biomass;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter > 1) {
			increaseBiomass (biomassPerSecond);
			counter = 0;
		}
		biomassText.text = biomass.ToString ();
	}

	public int getBiomass(){
		return biomass;
	}

	public void decreaseBiomass(int amount){
		biomass -= amount;
		if (biomass < 0)
			biomass = 0;
	}

	public void increaseBiomass (int amount){
		biomass += amount;
	}
}
