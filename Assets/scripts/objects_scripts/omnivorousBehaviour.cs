﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class omnivorousBehaviour : SecondaryAnimalBehaviour {

	protected GameObject closestEdible = null;
	protected bool noCloseFoodSource = false;

	// Use this for initialization
	void Start () {
		base.Start ();
	}

	// Update is called once per frame
	void Update () {		
		base.Update();
	}

    protected override void check_events()
    {
        if (energy <= 0)
        {
            this.die();
        }

        if (check_for_predators())
        {
            currState = "runningFromPredator";
        }
        else
        {
            if (currState == "runningFromPredator") currState = "iddle";

            if (!starving && energy / maxEnergy < lowNutritionBoundery / 100)
            {
                starving = true;
                currState = "searchingFood";
            }
            else
            {
                if (!hungry && energy / maxEnergy < maxNutritionBoundery / 100)
                {
                    hungry = true;
                    currState = "searchingFood";
                }
            }
        }
    }

    protected override void plan_action()
    {

        switch (currState)
        {

            case "iddle":
                iddle();
                break;
            case "searchingFood":
                searchFood();
                break;
            case "tryingToEat":
                tryEating();
                break;
            case "runningFromPredator":
                walk_away(focusedPredator.transform.position);
                break;
        }

        energy -= Time.deltaTime * defaultBasalExpense;
    }

    protected void searchFood()
    {
        findClosestEdible();
        if (noCloseFoodSource)
        {
            walk_randomly();
        }
        else
        {
            tryEating();
            currState = "tryingToEat";
        }
    }

    protected void tryEating(){

		if (closestEdible != null) {
			Vector3 diff = closestEdible.transform.position - transform.position;

			if (diff.sqrMagnitude > 0.03f) {
				walk_towards (closestEdible.transform.position);
			} else {
                PlantModel plant = closestEdible.GetComponent<PlantModel>();
                eat_plant (plant);
				closestEdible = null;
				currState = "iddle";
			}
		} else { // The plant was taken	
			findClosestEdible();
			currState = "searchingFood";
		}
	}

	protected void iddle(){
		animator.SetBool("iddle", true );
	}

	protected override void findClosestEdible(){ // refatorar para procurar por animais também

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
