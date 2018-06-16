using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class carnivoreBehaviour : animalModel {

	public string[] preys;

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

    /*protected override void plan_action (){	
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
	}*/
    protected override void check_events()
    {
        if (energy <= 0)
        {
            this.die();
        }

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

    protected override void plan_action (){	
		
		switch (currState) {

		case "iddle":
			iddle();
			break;
		case "searchingFood":
			searchFood();
			break;
		case "tryingToEat":
			tryEating();
			break;

		}

		energy -= Time.deltaTime * defaultBasalExpense;
	}

	protected void searchFood(){
		findClosestEdible ();
		if (noCloseFoodSource) {
			walk_randomly ();
		} else {
			tryEating ();
			currState = "tryingToEat";
		}
	}

	void tryEating(){

		if (closestEdible != null) {
			Vector3 diff = closestEdible.transform.position - transform.position;

			if (diff.sqrMagnitude > 0.03f) {
				run_towards (closestEdible.transform.position);
			} else {
				animalModel model = closestEdible.GetComponent<animalModel> ();
				eat_animal (model);
				closestEdible = null;
				currState = "iddle";
			}
		} else { // The animal died			
			findClosestEdible();
		}
	}

	void iddle(){
		walk_randomly ();
	}

	protected override void findClosestEdible(){

		//trees = GameObject.FindGameObjectsWithTag ("edible plant");
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(position, lineOfSight);

		foreach(Collider2D collider in colliders){
			if(preys.Contains(collider.gameObject.tag)){
				Vector3 diff = collider.gameObject.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closestEdible = collider.gameObject;
                    animalModel model = closestEdible.GetComponent<animalModel>();
                    model.getHunted(gameObject);
                    distance = curDistance;
				}
			}
		}	

		if (closestEdible == null) {// there is no animal close
			noCloseFoodSource = true;
		} else {
			noCloseFoodSource = false;
		}
	}

    public animalModel getPrey()
    {
        return closestEdible.GetComponent<animalModel>();
    }

	protected override void die(){
        if (closestEdible != null)
        {
            animalModel model = closestEdible.GetComponent<animalModel>();
            model.stopHunting(gameObject);
        }
        
        Destroy (gameObject);
	}
}