using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantModel : MonoBehaviour {

	public float chanceOfBeingSpawned = 5.0f; // chance of being spawned every day
	public float energyWhenConsumed = 50;
	public float hidrationWhenConsumed = 50;

	protected registryController registry;
	protected MapGenerator mapGenerator;

	// Use this for initialization
	void Start () {
		registry = GameObject.Find ("MapController").GetComponent<registryController> ();
	}

	public void initialize(registryController registry, MapGenerator mapGenerator){
		this.registry = registry;
		this.mapGenerator = mapGenerator;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void newHour(){
		/*int result = Random.Range (1, 1001);

		if (result <= chanceOfBeingSpawned*10){
			mapGenerator.generateSecondaryPlants (gridX+Random.Range (-5, 6), gridY+Random.Range (-5, 6));
		}*/
	}

	public void Get_Eaten(){
		registry.unregisterEdiblePlant (this.gameObject);
		Destroy(this.gameObject);
	}
}
