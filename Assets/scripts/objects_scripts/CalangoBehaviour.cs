using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

abstract public class CalangoBehaviour : AnimalModel {

    public Sprite mateSprite;
    public Sprite eatSprite;
    public Sprite combatSprite;
    public Sprite losingSprite;

    public bool male;
    public float matingChance = 30;

    protected bool canReproduce = false;
    protected bool mating;

    protected SpriteRenderer actionIcon;

    protected CalangoBehaviour competitor;
    protected bool looser = false;
    protected bool noCloseFoodSource = false;

    protected GameObject closestEdible = null;

    protected float hideoutSearchCounter = 0;
    protected float hideoutSearchIntervalInSec = 1;

    protected float hideCounter = 0;
    protected float hiddingTimeInSec = 5;

    // Use this for initialization
    void Start() {
        base.Start();

        actionIcon = transform.Find("actionIcon").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        base.Update();
    }

    protected override void check_events()
    {
        //Check for events that may change the calango behaviour in order of importance
        if (!CheckIfDead())
        {
            if (!CheckIfStarving())
            {
                if (!CheckIfRunningFromPredator()){
                    if (!CheckIfBeingHunted())
                    {
                        CheckIfHungry();
                    }
                }
            }
        }        
    }

    bool CheckIfDead()
    {
        if (energy <= 0)
        {
            this.die();
            return true;
        }
        return false;
    }

    bool CheckIfRunningFromPredator()
    {
        string[] lifeOrDeathStates = { "runningFromPredator", "hidding" };
        if (lifeOrDeathStates.Contains(currState))
        {
            if (predatorList.Count == 0)
            {
                currState = "iddle";
                return false;
            }
            return true;
        }
        else return false;
    }

    bool CheckIfBeingHunted()
    {
        if (check_for_predators())
        {
            findClosestHideoutInSight(focusedPredator);
            hideoutSearchCounter = 0;
            currState = "runningFromPredator";
            return true;
        }
        return false;
    }

    bool CheckIfStarving()
    {
        if (!starving && energy / maxEnergy < lowNutritionBoundery / 100)
        {
            starving = true;
            looser = false;
            mating = false; // if it is starving it stops running from other animals or mating and start looking for food
            currState = "searchingFood";
            return true;
        }
        return false;
    }
    bool CheckIfHungry() { 
        
        if (!hungry && energy / maxEnergy < maxNutritionBoundery / 100)
        {
            hungry = true;
            return true;
        }
        return false;
    }

    protected override void plan_action (){			

 		switch (currState) {

		case "iddle":
			actionIcon.sprite = null;
			iddle();
			break;
		case "tryingToMate":
			actionIcon.sprite = mateSprite;
			tryMating ();
			break;
		case "searchingFood":
			actionIcon.sprite = eatSprite;
			searchFood();
			break;
		case "tryingToEat":
			actionIcon.sprite = eatSprite;
			tryEating();
			break;
        case "runningFromPredator":
            actionIcon.sprite = losingSprite;
            runFromPredator();
            break;
        case "hidding":
            actionIcon.sprite = losingSprite;
            hide();
            break;
        }


		energy -= Time.deltaTime * defaultBasalExpense;
	}

    protected void hide()
    {
        hideCounter += Time.deltaTime;
        if(hideCounter> hiddingTimeInSec)
        {
            currState = "iddle";
            Hidden = false;
        }
    }

        protected void runFromPredator()
    {
        //if there is a hideout close run towards it
        if(focusedHideout != null)
        {
            walk_towards(focusedHideout.transform.position);
            if ((focusedHideout.transform.position - transform.position).sqrMagnitude < 0.03f)
            {
                currState = "hidding";
                hideCounter = 0;
                Hidden = true;
            }
        } else
        {
            hideoutSearchCounter += Time.deltaTime;
            if(hideoutSearchCounter > hideoutSearchIntervalInSec)
            {
                findClosestHideoutInSight(focusedPredator);
                hideoutSearchCounter = 0;
            }
            walk_away(focusedPredator.transform.position);
            if((focusedPredator.transform.position-transform.position).sqrMagnitude > lineOfSight)
            {
                currState = "hidding";
            }
        }
    }

	protected void iddle(){	
	
		if (age < reproductiveAge) { // If not in reproductive stage
			if (hungry)
				currState = "searchingFood";
			else
				currState = "iddle";
		} else {
			searchMate ();
		}

		animator.SetBool("iddle", true );
	}

	private bool Is_Hungry(){
		hungry = (energy / maxEnergy < maxNutritionBoundery / 100);
		return hungry;
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

	protected abstract void searchMate();

	protected abstract void tryMating();

	/*protected abstract void tryMating();
	protected abstract void findClosestMate();*/

	public bool isFertile(){
		return canReproduce;
	}

	protected override void die(){
		registry.unregisterCalango(gameObject, Deaths.INANICAO);
		Destroy (gameObject);
	}

	public void getChallenged(CalangoBehaviour source){
		competitor = source;
	}

	public void winChallenge(CalangoBehaviour looser){
		if (competitor == looser)
			competitor = null;
	}

	public void atack(CalangoBehaviour competitor){
		float damage = age / 10 * Time.deltaTime; // Preciso bolar um calculo para o ataque (dano por segundo)
		competitor.getAttacked (damage);
	}

	public void getAttacked(float damage){
		this.energy -= damage;
	}

	protected void runFromCompetitor(){
		if (competitor != null) {
			Vector3 diff = competitor.transform.position - transform.position;
			if (diff.sqrMagnitude < 10f) { // still in range			
				walk_away (competitor.transform.position);
			} else {
				competitor = null;
				looser = false;
			}
		} else {  // In case the competitor died
			looser = false;
		}
	}

    public override void get_eaten(){	
		registry.unregisterCalango (this.gameObject, Deaths.PREDADOR);

		base.get_eaten ();	
	}

    protected void tryEating()
    {

        if (closestEdible != null)
        {
            Vector3 diff = closestEdible.transform.position - transform.position;

            if (diff.sqrMagnitude > 0.03f)
            {
                walk_towards(closestEdible.transform.position);
            }
            else
            {
                if (closestEdible.tag == "plant")
                {
                    PlantModel plant = closestEdible.GetComponent<PlantModel>();
                    if (plant.edible)
                    {
                        eat_plant(plant);
                    } else {
                        eat_insect(plant);
                    }
                } else
                {

                }
                closestEdible = null;
                currState = "iddle";
            }
        }
        else
        { // The plant was taken	
            findClosestEdible();
            currState = "searchingFood";
        }
    }

    protected override void findClosestEdible()
    { // refatorar para procurar por animais também

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, lineOfSight);

        foreach (Collider2D collider in colliders)
        {
            PlantModel plant;
            if (collider.gameObject.tag == "plant")
            {
                plant = collider.gameObject.GetComponent<PlantModel>();
                if (plant.edible || plant.hasAnyInsect())
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

        if (closestEdible == null)
        {
            noCloseFoodSource = true;
        }
        else
        {
            noCloseFoodSource = false;
        }
    }
    
}
