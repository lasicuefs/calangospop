﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class animalModel : MonoBehaviour {

	public float velocity = 1;
	public float maxVelocity = 1.5f;
	public float childrenGenerated = 2;
	public float malePercentage = 50;  // In percentage %
	public int reproductiveAge = 1;
	public int seniority = 3;
	public int maxAge = 4;

	public float maxEnergy = 100;
	public float maxHidration = 100;

	public float lowNutritionBoundery = 30; // In percentage %
	public float maxNutritionBoundery = 75;  // In percentage % 

	public float energyWhenConsumed = 50;
	public float hidrationWhenConsumed = 50;

	public float defaultBasalExpense = 10; // Per second;

	public float lineOfSight = 1.2f;


	int directionX = 1;
	int directionY = 1; 
	protected float energy;
	protected float hidration;
	protected bool hungry = false;
	protected bool starving = false;
	protected int age = 0;
	protected int hourTimer = 0;

	protected Animator animator;
	protected registryController registry;
	protected MapGenerator mapGenerator;

	public string currState = "iddle";

	// Use this for initialization
	protected void Start () {
		energy = Random.Range (maxEnergy/2, maxEnergy);
		hidration = Random.Range (maxHidration/2, maxHidration);

		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	protected void Update () {
		plan_action ();
	}

	public void setControllerReferences(registryController registry, MapGenerator mapGenerator){
		this.registry = registry;
		this.mapGenerator = mapGenerator;
	}

	public float Get_Energy(){
		return energy;
	}

	protected void Get_Hungry(){
		findClosestEdible();
		hungry = true;
	}

	protected void Get_Starved(){
		findClosestEdible();
		starving = true;
	}

	abstract protected void plan_action ();

	public void get_eaten(){
	}

	protected void eat_animal(animalModel animal){
	}

	protected void eat_plant(GameObject plant){

		plantModel model = plant.GetComponent<plantModel> ();

		energy = energy + model.energyWhenConsumed;
		hidration = hidration + model.hidrationWhenConsumed;
		if (energy > maxEnergy) {
			energy = maxEnergy;
		}
		if (hidration > maxHidration){
			hidration = maxHidration;
		}

		model.Get_Eaten ();
		hungry = false;
		starving = false;
	}

	protected void walk_randomly(){
		if (Random.Range (0, 60) == 1) {
			directionX = - directionX;
			animator.SetBool("right", directionX >0 );
			findClosestEdible (); //Check if new edibles are found
		}

		if (Random.Range (0, 60) == 1) {
			directionY = - directionY;
			animator.SetBool("up", directionY >0 );
			findClosestEdible (); //Check if new edibles are found
		}

		walk_towards(transform.position + new Vector3 (directionX* 100, directionY *100 , 0));

		animator.SetBool("iddle", false );
		energy -= Time.deltaTime * defaultBasalExpense;
	}

	abstract protected void findClosestEdible (); 

	protected void walk_towards(Vector3 destinationPos){
		// já que o eixo y é duas vezes menor que o x em um mapa isométrico...
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(destinationPos.x, transform.position.y, transform.position.z), Time.deltaTime * velocity);
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(transform.position.x, destinationPos.y, transform.position.z), Time.deltaTime * velocity/2); 

		animator.SetBool("right", transform.position.x < destinationPos.x);
		animator.SetBool("up", transform.position.y < destinationPos.y);

		animator.SetBool("iddle", false );
		energy -= Time.deltaTime * defaultBasalExpense;
	}

	protected void walk_away(Vector3 enemyPos){
		Vector3 destinationPos = - enemyPos;

		// já que o eixo y é duas vezes menor que o x em um mapa isométrico...
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(destinationPos.x, transform.position.y, transform.position.z), Time.deltaTime * velocity);
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(transform.position.x, destinationPos.y, transform.position.z), Time.deltaTime * velocity/2); 

		animator.SetBool("right", transform.position.x < destinationPos.x);
		animator.SetBool("up", transform.position.y < destinationPos.y);

		animator.SetBool("iddle", false );
		energy -= Time.deltaTime * defaultBasalExpense;
	}

	abstract protected void die();

	public void getOlder(){
		age++;
		if (age == reproductiveAge) {
			transform.localScale += transform.localScale * 0.5f;
		} else if (age > maxAge) {
			die ();
		}
	}

	public int getAge(){
		return age;
	}

	public void setAge(int hours){
		age = hours/24;
		hourTimer = hours % 24;
		if (age >= reproductiveAge) {
			transform.localScale += transform.localScale * 0.5f;
		} 
	}

	protected void newHour(){
		hourTimer++;
		if (hourTimer >= 24) {
			getOlder ();
			hourTimer = 0;
		}
	}
}
