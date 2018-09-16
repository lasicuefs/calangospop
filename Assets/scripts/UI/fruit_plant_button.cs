using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class fruit_plant_button : instance_button {

	void Start(){
		base.Start ();
	}

	void Update(){
		base.Update ();
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter (eventData);

		fruitfulPlant script = prefab.GetComponent<fruitfulPlant> ();
        PlantModel fruit =  script.fruitPreab.GetComponent<PlantModel>();
		typeText.text = "Planta frutífera";
		descText.text = "Frutas por hora: " + script.avarageFruitPerHour+"\nRaio de alcançe: "+script.fruitRadius+"\nEnergia da fruta: " + fruit.energyWhenConsumed+"\nHidratação da fruta: "+fruit.hidrationWhenConsumed;

	}
}
