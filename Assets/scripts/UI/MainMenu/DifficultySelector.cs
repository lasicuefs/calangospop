using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour {

    public Text title;
    Dropdown dropdown;


    void Start () {
        dropdown = gameObject.GetComponent<Dropdown>();
        int difficulty = PlayerPrefs.GetInt(GameConstants.DIFFICULTY);
        if(difficulty != null)
        {            
            dropdown.value = difficulty;
        }

        dropdown.onValueChanged.AddListener(delegate
        {
            chooseDificulty(dropdown.value);
        });
    }

    private void Update()
    {
        title.text = GameTextController.getText(LanguageConstants.GUI_DIFICULTY_TITLE);
        dropdown.options[0].text = GameTextController.getText(LanguageConstants.GUI_DIFICULTY_OPTION_1);
        dropdown.options[1].text = GameTextController.getText(LanguageConstants.GUI_DIFICULTY_OPTION_2);
        dropdown.options[2].text = GameTextController.getText(LanguageConstants.GUI_DIFICULTY_OPTION_3);
        dropdown.RefreshShownValue();
    }

    public void chooseDificulty(int selected)
    {
        PlayerPrefs.SetInt(GameConstants.DIFFICULTY, selected);
    }
}
