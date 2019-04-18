using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class femaleCalango : CalangoBehaviour {

	int hoursSinceBirth = 0;
	public int reproductiveInterval = 18; // in hours

	protected List<GameObject> interestedMates = new List<GameObject>();

	void Start(){
		base.Start ();
        hoursSinceBirth = Random.Range (0, reproductiveInterval);
	}

	protected override void searchMate(){
		
		if (interestedMates.Count > 0) {
			mating = true;
			currState = GameConstants.states.TRYTOMATE;
		} else
			notMating ();
		
	}

	protected override void tryMating(){	
		if (interestedMates.Count == 0) { // in case the parteners all died before mating
			mating = false;
			notMating ();
		}
	}

	void notMating(){
		if(hungry) currState = GameConstants.states.SEARCHINGFOOD;
		else  currState = GameConstants.states.IDDLE;
	}

	public void add_proposition(GameObject sender){
		interestedMates.Add(sender);
	}

	public void remove_proposition(GameObject sender){
		interestedMates.Remove(sender);
	}

	public CalangoBehaviour getCompetitors(GameObject competitor){
		foreach(GameObject mate in interestedMates){
			if (mate != competitor)
				return mate.GetComponent<CalangoBehaviour>();
		}
		return null;
	}


	public List<GameObject> get_interested_mates(){
		return interestedMates;
	}

	public void mate(GameObject partner){
		int result = Random.Range (1, 101);
		if (result <= matingChance) { //usando apenas a chance da fêmea por enquanto...
			giveBirth ();
		}

		mating = false;
		canReproduce = false;
		interestedMates.Clear ();
	}

	void giveBirth(){
		for(int i = 0; i < childrenGenerated ; i++){
			float randomVal = Random.Range (1, 101);
			mapGenerator.generateCalango (randomVal > malePercentage, transform.position + new Vector3(randomVal/100, randomVal/100, 0 ));
		}
	}

	void newHour(){
		base.newHour();

		if(!canReproduce){
			hoursSinceBirth++;

			if (hoursSinceBirth >= reproductiveInterval) {
				hoursSinceBirth = 0;
				canReproduce = true;
			}
		}
	}
}
