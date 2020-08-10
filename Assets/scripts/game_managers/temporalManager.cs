using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporalManager : MonoBehaviour {

	public int secondsForADay = 60; //how much seconds does a day lasts
	public int timeScale = 1;
	float hourCounter = 0.0f;
	int currentDay = 1;
	int currentHour = 1;

	int minTime = 0;
	public int maxTime = 3;

	public Button pauseB;
	public Button speedB1;
	public Button speedB2;
	public Button speedB3;

	MapGenerator mapGenerator;
	RegistryController registry;
	GameObject calangosParentObject;
	GameObject plantsParentObject;
    GameObject animalsParentObject;

    // Use this for initialization
    void Start () {
		mapGenerator = GetComponentInParent<MapGenerator> ();
		registry  = GetComponentInParent<RegistryController> ();
		calangosParentObject = GameObject.Find ("Calangos");
		plantsParentObject = GameObject.Find ("Plants");
        animalsParentObject = GameObject.Find("Animals");

        setTimeSpeed (timeScale);
	}
	
	// Update is called once per frame
	void Update () {
		hourCounter += Time.deltaTime;

		if(hourCounter > secondsForADay/24f){
			new_hour ();
			hourCounter = 0;
			currentHour++;
		}

		if (currentHour > 24) {
			currentHour = 1;
			currentDay++;
		}

		//change time speed
		if (Input.GetKeyUp("x")){
			if(timeScale<maxTime) timeScale++;
			setTimeSpeed(timeScale);
		}

		if (Input.GetKeyUp("z")){
			if(timeScale>minTime)timeScale--;
			setTimeSpeed(timeScale);
		}
	}

	void new_hour(){
		if(plantsParentObject.transform.childCount > 0) plantsParentObject.BroadcastMessage ("newHour");
		if(calangosParentObject.transform.childCount > 0) calangosParentObject.BroadcastMessage ("newHour");
        if(animalsParentObject.transform.childCount > 0) animalsParentObject.BroadcastMessage("newHour");
        mapGenerator.UpdateMap ();
	}

	public int getHour(){
		return currentHour;
	}

	public int getDay(){
		return currentDay;
	}

	public void setTimeSpeed(int speed){
		switch (speed) {

		case 0:
			pauseB.interactable = false;
			speedB1.interactable = true; 
			speedB2.interactable = true; 
			speedB3.interactable = true; 
			break;
		case 1:
			pauseB.interactable = true;
			speedB1.interactable = false; 
			speedB2.interactable = true; 
			speedB3.interactable = true; 
			break;
		case 2:
			pauseB.interactable = true;
			speedB1.interactable = true; 
			speedB2.interactable = false; 
			speedB3.interactable = true; 
			break;
		case 3:
			pauseB.interactable = true;
			speedB1.interactable = true; 
			speedB2.interactable = true; 
			speedB3.interactable = false; 
			break;
		default:
			pauseB.interactable = true;
			speedB1.interactable = true; 
			speedB2.interactable = true; 
			speedB3.interactable = true; 
			break;
		}
		timeScale = speed;
		Time.timeScale = speed;
	}
}
