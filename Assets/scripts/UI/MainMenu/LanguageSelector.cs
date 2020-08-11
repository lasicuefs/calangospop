using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour {


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
    }

    public void chooseLanguage(int selected)
    {
        PlayerPrefs.SetInt(GameConstants.LANGUAGE, selected);
    }
}
