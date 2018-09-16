using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public GameObject sand		;
	public GameObject decorativePlant;
	public GameObject ediblePlant;
	public GameObject calangoMale;
	public GameObject calangoFemale;

	public float tileSize = 1;
	public int mapSize = 50;

	public int lizardPercentage = 10; // in %
	public int ediblePlantPercentage = 20; // in %
	public int decorativePlantPercentage = 20; // in %

	public bool autoGenerateResources = true;

	protected registryController registry;
	protected GameObject calangosParentObject;
	protected GameObject plantsParentObject;
	protected GameObject animalsParentObject;

	// Use this for initialization
	void Start () {
		registry = GetComponentInParent<registryController> ();
		calangosParentObject = GameObject.Find ("Calangos");
		plantsParentObject = GameObject.Find ("Plants");
		animalsParentObject = GameObject.Find ("Animals");
		initialize_map ();
	}

	protected void initialize_map(){
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				Instantiate (sand, new Vector3 ((i-j) * (-tileSize / 2), (i+j) * (-tileSize / 4), ((float)(-i-j))/100), new Quaternion (0, 0, 0 ,0));

				int result = Random.Range (1, 101);

				if(result<ediblePlantPercentage){
					generateEdiblePlant(i, j);
				} else if (result <= ediblePlantPercentage+decorativePlantPercentage){
					GameObject decor = Instantiate (decorativePlant, new Vector3 ((i-j) * (-tileSize / 2), (i+j) * (-tileSize / 4), 0),  new Quaternion (0, 0, 0 ,0));
					decor.transform.parent = plantsParentObject.transform;
				} else if (result <= ediblePlantPercentage+decorativePlantPercentage+lizardPercentage){
					bool isMacho = (Random.value < .5);
					CalangoBehaviour calango = this.generateCalango ( isMacho , new Vector3 ((i - j) * (-tileSize / 2), (i + j) * (-tileSize / 4), 0));	
					calango.setAge (Random.Range (1, (calango.maxAge-1)*24)); // generating calangos at different ages
				}
			}
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateMap(){
		if (autoGenerateResources) {
			for (int i = 0; i < mapSize; i++) {
				for (int j = 0; j < mapSize; j++) {
					int result = Random.Range (1, 1001);

					PlantModel model = ediblePlant.GetComponent<PlantModel> ();
					if (result <= model.chanceOfBeingSpawned * 10) {
						generateEdiblePlant (i, j);
					}
				}
			}
		}
	}

	public void generateSecondaryPlants(int positionX, int positionY){

		int mapLimit = mapSize - 1;

		if (positionX > mapLimit) {
			positionX = mapLimit + mapLimit - positionX;
		} else if(positionX < 0){
			positionX = -positionX;
		}
		if (positionY > mapLimit) {			
			positionY = mapLimit + mapLimit - positionY;
		} else if(positionY< 0){
			positionY = - positionY;
		}

		generateEdiblePlant (positionX, positionY);
		
	}

	public void generateEdiblePlant(int positionX, int positionY){	

		float randomOffsetx = Random.Range(-10,11)/10*tileSize;
		float randomOffsety = Random.Range(-10,11)/10*tileSize;

		GameObject edible = Instantiate (ediblePlant, new Vector3 (((positionX-positionY) * (-tileSize / 2))+randomOffsetx, ((positionX+positionY) * (-tileSize / 4))+randomOffsety, 0), new Quaternion (0, 0, 0 ,0));
		edible.GetComponent<PlantModel> ().initialize (registry, this);
		registry.registerEdiblePlant (edible);

		edible.transform.parent = plantsParentObject.transform;
	}

	public CalangoBehaviour generateCalango(bool macho, Vector3 location){
		GameObject calango;
		if (macho) {
			calango = Instantiate (calangoMale, location, new Quaternion (0, 0, 0 ,0));
		} else {
			calango = Instantiate (calangoFemale, location, new Quaternion (0, 0, 0 ,0));
		}
		CalangoBehaviour calangoScript = calango.GetComponent<CalangoBehaviour> ();
		calangoScript.setControllerReferences (registry, this);
		registry.registerCalango (calango);

		calango.transform.parent = calangosParentObject.transform;
		return calangoScript;
	}

    public void generateCustomAnimal(GameObject prefab, float positionX, float positionY)
    {

        float randomOffsetx = Random.Range(-10, 11) / 10 * tileSize;
        float randomOffsety = Random.Range(-10, 11) / 10 * tileSize;

        GameObject animal = Instantiate(prefab, new Vector3(positionX, positionY, 0), new Quaternion(0, 0, 0, 0));
        AnimalModel animalScript = animal.GetComponent<AnimalModel>();
        animalScript.setControllerReferences(registry, this);
        animal.transform.parent = animalsParentObject.transform;
    }
}
