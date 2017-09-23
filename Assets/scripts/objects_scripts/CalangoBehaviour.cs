using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CalangoBehaviour : omnivorousBehaviour {

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

	// Use this for initialization
	void Start () {
		base.Start ();

		actionIcon = transform.Find ("actionIcon").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	protected override void plan_action (){	
		if (energy <= 0) {
			this.die ();	
		}

		if (!starving && energy / maxEnergy < lowNutritionBoundery / 100) {
			starving = true;
			looser = false;
			mating = false; // if it is starving it stops running from other animals or mating and start looking for food
			currState = "searchingFood";
		} else {
			if (!hungry && energy / maxEnergy < maxNutritionBoundery / 100) {
				hungry = true;
			}
		}

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

		}

		energy -= Time.deltaTime * defaultBasalExpense;
	}

	protected void iddle(){	
	
		if (age < reproductiveAge || age >= seniority) { // If not in reproductive stage
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
		registry.unregisterCalango(gameObject);
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

}
