using Assets.scripts.game_managers;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPanel : MonoBehaviour {

    public Text title;
    public Text[] orTexts;
    
	void Start () {
		title.text = GameTextController.getText(LanguageConstants.CONTROLS);
        foreach (Text text in orTexts)
        {
            text.text = GameTextController.getText(LanguageConstants.OR);
        }
    }
}
