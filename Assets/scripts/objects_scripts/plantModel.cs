using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantModel : MonoBehaviour {

	public float chanceOfBeingSpawned = 5.0f; // chance of being spawned every day
    public float energyWhenConsumed = 50;
    public float hidrationWhenConsumed = 50;    
    public int startingInsectAmount = 0;
    public int maxInsectAmount = 0;

    public bool hasInsects = false;
    public bool edible = false;
    public bool isHideout = false;
    public bool sunProtection = false;

    public float shadowRadius = .5f;

    protected registryController registry;
	protected MapGenerator mapGenerator;
    protected InsectSwarmModel swarm;
    
    public string plantName = "";

	public void initialize(registryController registry, MapGenerator mapGenerator){
		this.registry = registry;
		this.mapGenerator = mapGenerator;

        if (hasInsects)
        {
            swarm = gameObject.AddComponent<InsectSwarmModel>();
            swarm.Initialize(startingInsectAmount, maxInsectAmount, registry);            
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void newHour(){
        if (swarm != null)
        {
            swarm.newInsects();
        }
       
        /*int result = Random.Range (1, 1001);

		if (result <= chanceOfBeingSpawned*10){
			mapGenerator.generateSecondaryPlants (gridX+Random.Range (-5, 6), gridY+Random.Range (-5, 6));
		}*/
    }

    public bool isEdible()
    {
        return edible;
    }

    public bool hasAnyInsect()
    {
        if (hasInsects) return swarm.insectCount > 0;
        else return false;
    }

    public InsectSwarmModel getSwarn()
    {
        return swarm;
    }

    public void Get_Insect_Eaten()
    {
        if(swarm!= null)
        {
            swarm.getEaten();
        }
    }

    public void Get_Eaten()
    {
        registry.unregisterEdiblePlant(this.gameObject);
        Destroy(this.gameObject);
    }   
}
