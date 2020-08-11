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
        nameText.text = name;
        SecondaryAnimalBehaviour script = prefab.GetComponent<SecondaryAnimalBehaviour>();
        typeText.text = "Competidor";
        descText.text = (desciption == "" ? desciption : desciption + "\n");
        descText.text += "Tempo de vida: " + script.maxAge + " anos";
        descText.text += "\nReprodução: " + (script.chanceOfReproducingPerHour * 24) + " filhos por dia";
    }
}
