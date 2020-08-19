using Assets.scripts.game_managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor_Button : instance_button
{

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    public override void select_instance()
    {
        generator.select(prefab, amount, radius, price, 1, this);
    }

    public override void setText()
    {
        nameText.text = GameTextController.getText(name); 
        SecondaryAnimalBehaviour script = prefab.GetComponent<SecondaryAnimalBehaviour>();
        typeText.text = GameTextController.getText(LanguageConstants.COMPETITOR_NAME);
        descText.text = (desciption == "" ? desciption : GameTextController.getText(desciption) + "\n");
        descText.text += GameTextController.getText(LanguageConstants.TIME_OF_LIFE)+": " + script.maxAge + " "+GameTextController.getText(LanguageConstants.YEARS).ToLower();
        descText.text += "\n"+ GameTextController.getText(LanguageConstants.REPRODUCTION) + ": " + (script.chanceOfReproducingPerHour * 24) + " "+ GameTextController.getText(LanguageConstants.DESCENDANTS_PER_DAY);
    }
}
