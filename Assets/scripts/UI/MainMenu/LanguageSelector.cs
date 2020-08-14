using Assets.scripts.game_managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour {

    public Text title;

    void Start()
    {
        Dropdown dropdown = gameObject.GetComponent<Dropdown>();
        int language = PlayerPrefs.GetInt(GameConstants.LANGUAGE, 1);
        if (language != null)
        {
            dropdown.value = language;
        }

        dropdown.onValueChanged.AddListener(delegate
        {
            chooseLanguage(dropdown.value);
        });

        title.text = GameTextController.getText(LanguageConstants.GUI_LANGUAGE_TITLE);
    }

    public void chooseLanguage(int selected)
    {
        PlayerPrefs.SetInt(GameConstants.LANGUAGE, selected);
        title.text = GameTextController.getText(LanguageConstants.GUI_LANGUAGE_TITLE);
    }
}
