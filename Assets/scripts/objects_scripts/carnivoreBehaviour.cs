using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class carnivoreBehaviour : SecondaryAnimalBehaviour {

	public string[] preys;

	private AnimalModel closestEdible = null;
	private bool noCloseFoodSource = false;

	// Use this for initialization
	void Start () {
		base.Start ();

        registry.registerPredator(gameObject);
    }

	// Update is called once per frame
	void Update () {
		base.Update ();
	}
    
    protected override void check_events()
    {
        if (energy <= 0)
        {
            this.die();
        }

        if (!starving && energy / maxEnergy < lowNutritionBoundery / 100)
        {
            starving = true;
            currState = GameConstants.states.SEARCHINGFOOD;
        }
        else
        {
            if (!hungry && energy / maxEnergy < maxNutritionBoundery / 100)
            {
                hungry = true;
                currState = GameConstants.states.SEARCHINGFOOD;
            }
        }
    }

    protected override void plan_action (){	
		
		switch (currState) {

		case GameConstants.states.IDDLE:
			iddle();
			break;
		case GameConstants.states.SEARCHINGFOOD:
			searchFood();
			break;
		case GameConstants.states.TRYTOEAT:
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
			currState = GameConstants.states.TRYTOEAT;
		}
	}

	void tryEating(){

		if (closestEdible != null && !closestEdible.Hidden) {
			Vector3 diff = closestEdible.transform.position - transform.position;

			if (diff.sqrMagnitude > 0.03f) {
				run_towards (closestEdible.transform.position);
			} else {
				AnimalModel model = closestEdible.GetComponent<AnimalModel> ();
				eat_animal (model);
				closestEdible = null;
				currState = GameConstants.states.IDDLE;
			}
		} else { // The animal died			
			findClosestEdible();
		}
	}

	void iddle(){
		walk_randomly ();
	}

	protected override void findClosestEdible(){
        
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(position, lineOfSight);

		foreach(Collider2D collider in colliders){
			if(preys.Contains(collider.gameObject.tag)){
                AnimalModel model = collider.GetComponent<AnimalModel>();
                if (!model.Hidden)
                {
                    Vector3 diff = collider.gameObject.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closestEdible = model;
                        model.getHunted(gameObject);
                        distance = curDistance;
                    }
                }
               
			}
		}	

		if (closestEdible == null) {// there is no animal close
			noCloseFoodSource = true;
		} else {
			noCloseFoodSource = false;
		}
	}

    public AnimalModel getPrey()
    {
        return closestEdible.GetComponent<AnimalModel>();
    }

	protected override void die(){
        if (closestEdible != null)
        {
            AnimalModel model = closestEdible.GetComponent<AnimalModel>();
            model.stopHunting(gameObject);
        }
        registry.unregisterPredator(gameObject);
        Destroy (gameObject);
	}
}