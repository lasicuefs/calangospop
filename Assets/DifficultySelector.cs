using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour {
    
	void Start () {
        Dropdown dropdown = gameObject.GetComponent<Dropdown>();
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

    public void chooseDificulty(int selected)
    {
        PlayerPrefs.SetInt(GameConstants.DIFFICULTY, selected);
    }
}
