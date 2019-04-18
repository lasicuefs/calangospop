	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class herbivoralBehaviour : SecondaryAnimalBehaviour {
		
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

        protected override void check_events()
        {
            if (energy <= 0)
            {
                this.die();
            }

            if (check_for_predators())
            {
                currState = GameConstants.states.RUNNINGFROMPREDATOR;
            }
            else
            {
                if (currState == GameConstants.states.RUNNINGFROMPREDATOR) currState = GameConstants.states.IDDLE;

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
        }

    protected override void plan_action (){	
		
        switch (currState)
        {

            case GameConstants.states.IDDLE:
                iddle();
                break;
            case GameConstants.states.SEARCHINGFOOD:
                searchFood();
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
            currState = GameConstants.states.TRYTOEAT;
        }
    }

    void tryEating(){
			
			if (closestEdible != null) {
				Vector3 diff = closestEdible.transform.position - transform.position;

				if (diff.sqrMagnitude > 0.03f) {
					walk_towards (closestEdible.transform.position);
				} else {
                    PlantModel plant = closestEdible.GetComponent<PlantModel>();
                    eat_plant (plant);
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
                PlantModel plant;
                if (collider.gameObject.tag == "plant")
                {
                    plant = collider.gameObject.GetComponent<PlantModel>();
                    if (plant.edible)
                    {
                        Vector3 diff = collider.gameObject.transform.position - position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            closestEdible = collider.gameObject;
                            distance = curDistance;
                        }
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
