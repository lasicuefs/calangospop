using Assets.scripts.game_managers;
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
        nameText.text = GameTextController.getText(name); 
        typeText.text = GameTextController.getText(LanguageConstants.FRUIT_PLANT_NAME);
        descText.text = GameTextController.getText(LanguageConstants.FRUIT_BY_HOUR)+": " + script.avarageFruitPerHour + "\n"+ GameTextController.getText(LanguageConstants.PRODUCTION_RADIUS) + ": " + script.fruitRadius + "\n" + GameTextController.getText(LanguageConstants.FRUIT_ENERGY) + ": " + fruit.energyWhenConsumed + "\n";
        descText.text += (script.hasInsects ? GameTextController.getText(LanguageConstants.HAS_INSECTS) + "\n" : "") + (script.isHideout ? GameTextController.getText(LanguageConstants.HAS_HIDEOUT) + "\n" : "") + (script.sunProtection ? GameTextController.getText(LanguageConstants.HAS_SHADOWS) + "\n" : "");
        if (script.hasInsects) descText.text += GameTextController.getText(LanguageConstants.INITIAL_INSECTS) + ": " + script.startingInsectAmount + "\n" + GameTextController.getText(LanguageConstants.ENERGY_BY_INSECTS) + ":" + script.insectEnergy;
    }
}
