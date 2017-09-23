using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class herbivoralBehaviour : animalModel {
	
	private GameObject closestEdible = null;
	private bool noCloseFoodSource = false;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	protected override void plan_action (){	
		if (energy <= 0) {
			die ();	
		}if (!hungry && energy/maxEnergy < lowNutritionBoundery/100) {
			Get_Hungry ();
		} else if (hungry) {
			if (noCloseFoodSource) {
				walk_randomly ();
			} else {
				tryEating ();
			}
		} else {
			iddle();
		}

		energy -= Time.deltaTime * defaultBasalExpense;
	}

	void tryEating(){
		
		if (closestEdible != null) {
			Vector3 diff = closestEdible.transform.position - transform.position;

			if (diff.sqrMagnitude > 0.03f) {
				walk_towards (closestEdible.transform.position);
			} else {
				eat_plant (closestEdible);
				closestEdible = null;
			}
		} else { // The plant was taken			
			findClosestEdible();
		}
	}

	void iddle(){
		
	}

	protected override void findClosestEdible(){
		
		//trees = GameObject.FindGameObjectsWithTag ("edible plant");
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(position, lineOfSight);

		foreach(Collider2D collider in colliders){
			if(collider.gameObject.tag == "edible plant"){
				Vector3 diff = collider.gameObject.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closestEdible = collider.gameObject;
					distance = curDistance;
				}
			}
		}	

		if (closestEdible == null) {// there is no tree on the map
			noCloseFoodSource = true;
		} else {
			noCloseFoodSource = false;
		}
	}

	protected override void die(){
		Destroy (gameObject);
	}
}
