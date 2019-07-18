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
		generator.select (prefab, amount, radius, price, 1, this);
	}
    
    public override void setText()
    {
        nameText.text = name;
        AnimalModel script = prefab.GetComponent<AnimalModel>();
        typeText.text = "Predador";
        descText.text = (desciption =="" ? desciption : desciption + "\n") + "Velocidade de caça: " + script.maxVelocity + "\nGasto basal: " + script.defaultBasalExpense;
    }
}
