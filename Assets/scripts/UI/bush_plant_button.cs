using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bush_plant_button : instance_button {

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);        
    }

    public override void setText()
    {
        nameText.text = name;
        PlantModel script = prefab.GetComponent<PlantModel>();
        typeText.text = "Planta não frutífera";
        descText.text = (script.hasInsects ? "Contém insetos\n" : "") + (script.isHideout ? "Esconderijo contra predadores\n" : "") + (script.sunProtection ? "Prejeta sombras\n" : "");
    }
}
