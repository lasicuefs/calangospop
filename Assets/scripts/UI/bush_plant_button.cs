using Assets.scripts.game_managers;
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
        typeText.text = GameTextController.getText(LanguageConstants.BRUSH_BUTTON_TXT_1);
        descText.text = (script.hasInsects ? GameTextController.getText(LanguageConstants.HAS_INSECTS)+"\n" : "") + (script.isHideout ? GameTextController.getText(LanguageConstants.HAS_HIDEOUT)+"\n" : "") + (script.sunProtection ? GameTextController.getText(LanguageConstants.HAS_SHADOWS)+"\n" : "");
        if (script.hasInsects) descText.text += GameTextController.getText(LanguageConstants.INITIAL_INSECTS) +": "+ script.startingInsectAmount+ "\n"+ GameTextController.getText(LanguageConstants.ENERGY_BY_INSECTS)+":" + script.insectEnergy;
    }
}
