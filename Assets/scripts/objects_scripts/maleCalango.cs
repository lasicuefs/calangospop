using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maleCalango : CalangoBehaviour {

	protected femaleCalango closestMate;

	float mateCounter = 0;
	public float mateDuration = 2.0f;
    public float aggressivenessPercentage = .5f;

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
		case "runningFromCompetitor":
			actionIcon.sprite = losingSprite;
			runFromMatingCompetitor ();
			break;
		case "engaging":
			actionIcon.sprite = combatSprite;
			engageCompetitor ();
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
            walk_away(focusedPredator.transform.position);
            break;

        }

		energy -= Time.deltaTime * defaultBasalExpense;
	}

	protected override void searchMate(){		
		
		findClosestMate ();
		if (closestMate != null) {	// mate found
			competitor = closestMate.getCompetitors (this.gameObject); // look for competitors
			if (competitor != null) {
                if (Random.value < energy / 100)
                {// In case there are competitors it has a chance of engaging them first
                    currState = "engaging";
                }
                else
                {
                    closestMate = null;
                    currState = "runningFromCompetitor";
                    return;
                }
                closestMate.add_proposition(this.gameObject);
                mating = true;
            } else {
				currState = "tryingToMate"; // In case there is no competition...
			}
		} else {
			notMating ();
		}
	}

	void notMating(){
		if(hungry) currState = "searchingFood";
		else  currState = "iddle";
	}

	protected void findClosestMate(){
		
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		Collider2D[] colliders = Physics2D.OverlapCircleAll (position, lineOfSight);

		foreach (Collider2D collider in colliders) {
			if (collider.gameObject.tag == "calango") {
				CalangoBehaviour calango = collider.gameObject.GetComponent<CalangoBehaviour> ();
				if(!calango.male && calango.isFertile() && calango.getAge() >= calango.reproductiveAge){
					Vector3 diff = collider.gameObject.transform.position - position;
					float curDistance = diff.sqrMagnitude;
					if (curDistance < distance) {
						closestMate = collider.gameObject.GetComponent<femaleCalango>();
						distance = curDistance;
					}
				}
			}
		}
	}

	protected override void tryMating(){	
		if (competitor != null) { // In case a competitor apears in the middle of the mating...	

			mateCounter = 0.0f;  // the mating proccess goes on hold
			currState = "engaging"; // and it engages the competitor
		} else {
			if (closestMate != null) {	// Mate still alive	 
				Vector3 diff = closestMate.transform.position - transform.position;

				if (diff.sqrMagnitude > 0.03f) { // not in range
					walk_towards (closestMate.transform.position);
				} else {
					matingRitual (); // I use this function to make the Calangos take some time for the mating to be complete because it was being done instatly
					closestMate = null;
				}

			} else { // The female has died
				closestMate = null;
				competitor = null;
				mating = false;
				currState = "iddle";
			}
		}

	}

	protected void engageCompetitor(){	
		if (competitor == null) { // if the competitor died for any reason
			currState = "tryingToMate";
			return;
		}

		competitor.getChallenged (this); // declares a challenge

		Vector3 diff = competitor.transform.position - transform.position;
		if (diff.sqrMagnitude > 0.03f) { // not in range
			walk_towards (competitor.transform.position);
		} else {
			atack (competitor);
		}

		if (this.energy < lowNutritionBoundery || Random.value*this.energy < 0.05f) { // In case the individual is close to starving it loses
			competitor.winChallenge (this.GetComponent<CalangoBehaviour>());
			closestMate.remove_proposition (this.gameObject);
			closestMate = null;
			mating = false;

			currState = "runningFromCompetitor";
		}
	}

	void runFromMatingCompetitor(){
		runFromCompetitor ();
		if(!looser) currState = "iddle";
	}

	void matingRitual(){
		mateCounter += Time.deltaTime;

		if (mateCounter > mateDuration) { // after the defined time the mating gets a conclusion
			mateCounter = 0.0f;
			mate ();
		}
	}

	void mate(){
		closestMate.mate (this.gameObject);

		competitor = null;
		closestMate = null;
		mating = false;

		currState = "iddle";
	}

	public void winChallenge(CalangoBehaviour looser){
		if (competitor == looser)
			competitor = null;

		currState = "tryingToMate";
	}


}
