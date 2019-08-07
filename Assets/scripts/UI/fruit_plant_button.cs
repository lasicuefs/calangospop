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
    
    public override void setText()
    {
        fruitfulPlant script = prefab.GetComponent<fruitfulPlant>();
        PlantModel fruit = script.fruitPrefab.GetComponent<PlantModel>();
        nameText.text = name;
        typeText.text = "Planta frutífera";
        descText.text = "Frutas por hora: " + script.avarageFruitPerHour + "\nRaio de alcançe: " + script.fruitRadius + "\nEnergia da fruta: " + fruit.energyWhenConsumed;
        descText.text += "\n"+ (script.hasInsects ? "Contém insetos\n" : "") + (script.isHideout ? "Esconderijo contra predadores\n" : "") + (script.sunProtection ? "Prejeta sombras\n" : "");
        if (script.hasInsects) descText.text += "Quantidade inicial de insetos" + script.startingInsectAmount + "\n Energia por inseto:" + script.insectEnergy;
    }
}
