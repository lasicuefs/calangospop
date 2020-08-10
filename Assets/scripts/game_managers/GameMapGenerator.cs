using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapGenerator : MapGenerator {

    
    public bool isRandomMap;
	public int initialPopulationAreaSize = 10;

    public bool hasFixedPopulationSize = false;
    public int initialPopulationSize = 80;

    private float waterOffset = .065f;
    int riverSize = 3;
    
    // Use this for initialization
    void Start () {
		registry = GetComponentInParent<RegistryController> ();
		calangosParentObject = GameObject.Find ("Calangos");
		plantsParentObject = GameObject.Find ("Plants");
		animalsParentObject = GameObject.Find ("Animals");
		if (isRandomMap) initialize_map();
        
			
	}

	protected void initialize_map(){
        totalExtraTerrain = Mathf.CeilToInt(mapSize * ExtraTerrainPercentage);
        totalTerrain = mapSize + TotalExtraTerrain*2;
        int outsideSize = TotalExtraTerrain - riverSize;

        // Background loop
        for (int i = 0; i < totalTerrain; i++) {
			for (int j = 0; j < totalTerrain; j++) {
                if (i < outsideSize || i >= totalTerrain - outsideSize || j < outsideSize || j >= totalTerrain - outsideSize)
                {
                    Instantiate(sand, new Vector3((i - j) * (-tileSize / 2), (i + j) * (-tileSize / 4), ((float)(-i - j)) / 100), new Quaternion(0, 0, 0, 0));
                }
                else if (i< TotalExtraTerrain || i > totalTerrain - TotalExtraTerrain || j < TotalExtraTerrain || j > totalTerrain - TotalExtraTerrain)
                {
                    Instantiate(water, new Vector3((i - j) * (-tileSize / 2), (i + j) * (-tileSize / 4)- waterOffset, ((float)(-i - j)) / 100), new Quaternion(0, 0, 0, 0));
                } else 	Instantiate (sand, new Vector3 ((i-j) * (-tileSize / 2), (i+j) * (-tileSize / 4), ((float)(-i-j))/100), new Quaternion (0, 0, 0 ,0));

			}
		}
		// Lizard loop
		int initialArea = totalTerrain / 2 - initialPopulationAreaSize / 2;
		int finalArea = totalTerrain / 2 + initialPopulationAreaSize / 2;

        if (hasFixedPopulationSize)
        {
            float totalSpaces = Mathf.Pow(initialPopulationAreaSize, 2);
            int interval;
            if (totalSpaces < initialPopulationSize)
            {
                Debug.LogError("Initial population is too big for the initial area. The population must but at most the area raise to power 2. In this case, " + totalSpaces);
                interval = 1;
            }
            else
            {
                interval = Mathf.FloorToInt(totalSpaces / initialPopulationSize);
            }

            int counter = 0;
            int popCounter = 0;
            for (int i = initialArea; i < finalArea; i++)
            {
                for (int j = initialArea; j < finalArea; j++)
                {
                    if (popCounter < initialPopulationSize && counter++ == interval)
                    {
                        bool isMacho = (Random.value < .5);
                        CalangoBehaviour calango = this.generateCalango(isMacho, new Vector3((i - j) * (-tileSize / 2), (i + j) * (-tileSize / 4), 0));
                        calango.setAge(Random.Range(1, (calango.maxAge - 1) * 24)); // generating calangos at different ages
                        popCounter++;
                        counter = 1;
                    }
                }
            }
            if(popCounter < initialPopulationSize)
            {
                bool isMacho = (Random.value < .5);
                CalangoBehaviour calango = this.generateCalango(isMacho, new Vector3(initialArea * (-tileSize / 2), initialArea * (-tileSize / 4), 0));
                calango.setAge(Random.Range(1, (calango.maxAge - 1) * 24)); // generating calangos at different ages
            }
        }
        else
        {
            for (int i = initialArea; i < finalArea; i++)
            {
                for (int j = initialArea; j < finalArea; j++)
                {
                    int result = Random.Range(1, 101);

                    if (result <= lizardPercentage)
                    {
                        bool isMacho = (Random.value < .5);
                        CalangoBehaviour calango = this.generateCalango(isMacho, new Vector3((i - j) * (-tileSize / 2), (i + j) * (-tileSize / 4), 0));
                        calango.setAge(Random.Range(1, (calango.maxAge - 1) * 24)); // generating calangos at different ages
                    }
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
