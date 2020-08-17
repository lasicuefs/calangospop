using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsTab : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject infoPanel;
    private GameObject mortalityPanel;
    private GameObject graphChart; 

	// Use this for initialization
	void Start () {
        graphChart = GameObject.Find("PanelLineChart");
        mortalityPanel = GameObject.Find("PanelMortalidade");
    }
	
	public void togglePainelGeral()
    {
        infoPanel.active = !infoPanel.active;
    }

    public void togglePainelMortalidade()
    {
        mortalityPanel.active = !mortalityPanel.active;
    }

    public void toggleGraph()
    {
        graphChart.active = !graphChart.active;
    }

    public void toggleMainMenu()
    {
        mainMenu.active = !mainMenu.active;
    }
}
