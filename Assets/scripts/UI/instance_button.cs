using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instance_button : MonoBehaviour {

	public Text priceText;
	public GameObject prefab;
	public int amount; 
	public float radius = 4;
	public int price = 0;
	InstanceGenerator generator;
	Resources_Controller resControl;

	// Use this for initialization
	void Start () {
		generator = GameObject.Find ("InstanceGenerator").GetComponent<InstanceGenerator> ();
		resControl = GameObject.Find ("MapController").GetComponent<Resources_Controller> ();
		priceText.text = price.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		Button button = GetComponent<Button>();
		if (price > resControl.getBiomass ()) {			
			button.interactable = false;
		} else {
			button.interactable = true;
		}
	}

	void select_instance(){
		resControl.decreaseBiomass (price);
		generator.select (prefab, amount, radius);
	}
}
