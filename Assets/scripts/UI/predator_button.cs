using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class predator_button : instance_button {
	
	void Start(){
		base.Start ();
	}

	void Update(){
		base.Update ();
	}

	public override void select_instance(){
		resControl.decreaseBiomass (price);
		generator.select (prefab, amount, radius, 1);
	}
    
    public override void setText()
    {
        AnimalModel script = prefab.GetComponent<AnimalModel>();
        typeText.text = "Predador";
        descText.text = "Velocidade de caça: " + script.maxVelocity + "\nGasto basal: " + script.defaultBasalExpense;
    }
}
