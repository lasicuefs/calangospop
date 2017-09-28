using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class consumable_plant_button : instance_button {

	void Start(){
		base.Start ();
	}

	void Update(){
		base.Update ();
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter (eventData);

		consumablePlants script = prefab.GetComponent<consumablePlants> ();
		typeText.text = "Planta consumível";
		descText.text = "Energia: " + script.energyWhenConsumed+"\nHidratação: "+script.hidrationWhenConsumed;

	}
}
