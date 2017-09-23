﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temporalManager : MonoBehaviour {

	public int secondsForDay = 60; //how much seconds does a day longs
	public int timeScale = 1;
	float hourCounter = 0.0f;
	int currentDay = 1;
	int currentHour = 0;

	int minTime = 0;
	public int maxTime = 3;

	public Button pauseB;
	public Button speedB1;
	public Button speedB2;
	public Button speedB3;

	MapGenerator mapGenerator;
	registryController registry;
	GameObject calangosParentObject;
	GameObject plantsParentObject;

	// Use this for initialization
	void Start () {
		mapGenerator = GetComponentInParent<MapGenerator> ();
		registry  = GetComponentInParent<registryController> ();
		calangosParentObject = GameObject.Find ("Calangos");
		plantsParentObject = GameObject.Find ("Plants");

		setTimeSpeed (timeScale);
	}
	
	// Update is called once per frame
	void Update () {
		hourCounter += Time.deltaTime;

		if(hourCounter > secondsForDay/24f){
			new_hour ();
			hourCounter = 0;
			currentHour++;
		}

		if (currentHour >= 24) {
			currentHour = 0;
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
