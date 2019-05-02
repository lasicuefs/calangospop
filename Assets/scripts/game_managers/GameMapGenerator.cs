using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapGenerator : MapGenerator {

    public bool isRandomMap;
	public int initialPopulationSize = 10;
	cameraScript camera;

	// Use this for initialization
	void Start () {
		registry = GetComponentInParent<registryController> ();
		calangosParentObject = GameObject.Find ("Calangos");
		plantsParentObject = GameObject.Find ("Plants");
		animalsParentObject = GameObject.Find ("Animals");
		if (isRandomMap) initialize_map();


		camera = GameObject.Find ("Main Camera").GetComponent<cameraScript>();
		if (camera != null)
			camera.setCameraPos(0, -(mapSize/4)*tileSize);
	}

	protected void initialize_map(){
		// Background loop
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				Instantiate (sand, new Vector3 ((i-j) * (-tileSize / 2), (i+j) * (-tileSize / 4), ((float)(-i-j))/100), new Quaternion (0, 0, 0 ,0));

			}
		}
		// Lizard loop
		int initialArea = mapSize / 2 - initialPopulationSize / 2;
		int finalArea = mapSize / 2 + initialPopulationSize / 2;
		for (int i = initialArea; i < finalArea; i++) {
			for (int j = initialArea; j < finalArea; j++) {
				int result = Random.Range (1, 101);

				if (result <= lizardPercentage) {
					bool isMacho = (Random.value < .5);
					CalangoBehaviour calango = this.generateCalango (isMacho, new Vector3 ((i - j) * (-tileSize / 2), (i + j) * (-tileSize / 4), 0));	
					calango.setAge (Random.Range (1, (calango.maxAge-1) * 24)); // generating calangos at different ages
				}
			}
	    }

    }

	public void generateCustomPlant(GameObject prefab, float positionX, float positionY){	

		float randomOffsetx = Random.Range(-10,11)/10*tileSize;
		float randomOffsety = Random.Range(-10,11)/10*tileSize;

		GameObject edible = Instantiate (prefab, new Vector3 (positionX, positionY, 0), new Quaternion (0, 0, 0 ,0));
		edible.GetComponent<PlantModel> ().initialize (registry, this);
		registry.registerEdiblePlant (edible);

		edible.transform.parent = plantsParentObject.transform;
	}	
}
