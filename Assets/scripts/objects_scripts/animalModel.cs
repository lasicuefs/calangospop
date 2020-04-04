using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

abstract public class AnimalModel : MonoBehaviour {

	public float velocity = 1;
	public float maxVelocity = 1.5f;
	public float childrenGenerated = 2;
	public float malePercentage = 50;  // In percentage %
	public int reproductiveAge = 1;
	public int maxAge = 4;

	public float maxEnergy = 100;
	public float maxHidration = 100;

	public float lowNutritionBoundery = 30; // In percentage %
	public float maxNutritionBoundery = 75;  // In percentage % 

	public float energyWhenConsumed = 50;
	public float hidrationWhenConsumed = 50;

	public float defaultBasalExpense = 2; // Per second;

	public float lineOfSight = 1.2f;


	int directionX = 1;
	int directionY = 1; 
	protected float energy;
	protected float hidration;
	protected bool hungry = false;
	protected bool starving = false;
	protected int age = 0;
	protected int hourTimer = 0;

	protected Animator animator;
	protected registryController registry;
	protected MapGenerator mapGenerator;

    [HideInInspector]
	public string currState = GameConstants.states.IDDLE;
    bool hidden = false;

    public string[] predators;
    protected List<GameObject> predatorList;
    protected GameObject focusedPredator;
    protected GameObject focusedHideout;
    protected GameObject focusedShadowProducer;

    public bool Hidden
    {
        get
        {
            return hidden;
        }

        set
        {
            hidden = value;
        }
    }

    // Use this for initialization
    protected void Start () {
        currState = GameConstants.states.IDDLE;
        predatorList = new List<GameObject>();
        energy = Random.Range (maxEnergy/2, maxEnergy);
		hidration = Random.Range (maxHidration/2, maxHidration);

		animator = GetComponent<Animator> ();
        registry = GameObject.Find("MapController").GetComponent<registryController>();

    }
	
	// Update is called once per frame
	protected void Update () {
        check_events();
		plan_action ();
	}

	public void setControllerReferences(registryController registry, MapGenerator mapGenerator){
		this.registry = registry;
		this.mapGenerator = mapGenerator;
	}

	public float Get_Energy(){
		return energy;
	}

	protected void Get_Hungry(){
		findClosestEdible();
		hungry = true;
	}

	protected void Get_Starved(){
		findClosestEdible();
		starving = true;
	}

    abstract protected void check_events();
    abstract protected void plan_action ();

	public virtual void get_eaten(){		
		Destroy(this.gameObject);
	}

	protected void eat_animal(AnimalModel animal){

		energy = energy + animal.energyWhenConsumed;
		hidration = hidration + animal.hidrationWhenConsumed;
		if (energy > maxEnergy) {
			energy = maxEnergy;
		}
		if (hidration > maxHidration){
			hidration = maxHidration;
		}

		animal.get_eaten ();
		hungry = false;
		starving = false;
	}

	protected void eat_plant(PlantModel plant){
        
		energy = energy + plant.energyWhenConsumed;
		hidration = hidration + plant.hidrationWhenConsumed;
		if (energy > maxEnergy) {
			energy = maxEnergy;
		}
		if (hidration > maxHidration){
			hidration = maxHidration;
		}

        plant.Get_Eaten ();
		hungry = false;
		starving = false;
	}

    protected void eat_insect(PlantModel plant)
    {
        if (plant.hasAnyInsect())
        {
            energy = energy + plant.getSwarn().energyWhenConsumed;
            hidration = hidration + plant.getSwarn().hidrationWhenConsumed;
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
            if (hidration > maxHidration)
            {
                hidration = maxHidration;
            }

            plant.Get_Insect_Eaten();
            hungry = false;
            starving = false;
        }        
    }

    protected void walk_randomly(){
		if (Random.Range (0, 60) == 1) {
			directionX = - directionX;
			animator.SetBool("right", directionX >0 );
			findClosestEdible (); //Check if new edibles are found
		}

		if (Random.Range (0, 60) == 1) {
			directionY = - directionY;
			animator.SetBool("up", directionY >0 );
			findClosestEdible (); //Check if new edibles are found
		}

		walk_towards(transform.position + new Vector3 (directionX* 100, directionY *100 , 0));

		animator.SetBool("iddle", false );
		animator.SetBool("running", false);
		energy -= Time.deltaTime * defaultBasalExpense * 3.0f;
	}

	abstract protected void findClosestEdible (); 

	protected void walk_towards(Vector3 destinationPos){

        // já que o eixo y é duas vezes menor que o x em um mapa isométrico...
        transform.position = Vector3.MoveTowards (transform.position, new Vector3(destinationPos.x, transform.position.y, transform.position.z), Time.deltaTime * velocity);
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(transform.position.x, destinationPos.y, transform.position.z), Time.deltaTime * velocity/2);

        clampPosition();

        animator.SetBool("right", transform.position.x < destinationPos.x);
		animator.SetBool("up", transform.position.y < destinationPos.y);
		animator.SetBool("running", false);

		animator.SetBool("iddle", false );
		energy -= Time.deltaTime * defaultBasalExpense * 3.0f;
	}

	protected void run_towards(Vector3 destinationPos){

        // já que o eixo y é duas vezes menor que o x em um mapa isométrico...
        transform.position = Vector3.MoveTowards (transform.position, new Vector3(destinationPos.x, transform.position.y, transform.position.z), Time.deltaTime * maxVelocity);
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(transform.position.x, destinationPos.y, transform.position.z), Time.deltaTime * maxVelocity/2);

        clampPosition();

        animator.SetBool("right", transform.position.x < destinationPos.x);
		animator.SetBool("up", transform.position.y < destinationPos.y);
		animator.SetBool("running", true);

		animator.SetBool("iddle", false );
		energy -= Time.deltaTime * defaultBasalExpense * 3.0f;
	}

	protected void walk_away(Vector3 enemyPos)
    {

        // já que o eixo y é duas vezes menor que o x em um mapa isométrico...
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemyPos.x, transform.position.y, transform.position.z), -1 * Time.deltaTime * velocity);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, enemyPos.y, transform.position.z), -1 * Time.deltaTime * velocity / 2);

        clampPosition();

        animator.SetBool("right", transform.position.x > enemyPos.x);
        animator.SetBool("up", transform.position.y > enemyPos.y);

        animator.SetBool("iddle", false);
        energy -= Time.deltaTime * defaultBasalExpense * 3.0f;
    }

   
    protected void reposition_from(Vector3 source)
    {

        float variability = Random.Range(-0.2f, 0.2f);
        // já que o eixo y é duas vezes menor que o x em um mapa isométrico...
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(source.x + variability, transform.position.y, transform.position.z), -1 * Time.deltaTime * velocity);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, source.y + variability, transform.position.z), -1 * Time.deltaTime * velocity / 2);

        clampPosition();

        animator.SetBool("right", transform.position.x > source.x+ variability);
        animator.SetBool("up", transform.position.y > source.y+ variability);

        animator.SetBool("iddle", false);
        energy -= Time.deltaTime * defaultBasalExpense * 3.0f;
    }

    abstract protected void die();

    protected virtual void dieOfOldAge()
    {
        die();
    }


    public virtual void getOlder(){
		age++;
		if (age == reproductiveAge) {
			transform.localScale += transform.localScale * 0.2f;
		} else if (age > maxAge) {
            dieOfOldAge();
		}
	}

	public int getAge(){
		return age;
	}

	public void setAge(int hours){
		age = hours/24;
		hourTimer = hours % 24;
		if (age >= reproductiveAge) {
			transform.localScale += transform.localScale * 0.5f;
		} 
	}

	protected void newHour(){
		hourTimer++;
		if (hourTimer >= 24) {
			getOlder ();
			hourTimer = 0;
		}
	}

    /*protected void findPredators()
    {
        focusedPredator = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, lineOfSight);

        foreach (Collider2D collider in colliders)
        {
            if (predators.Contains(collider.gameObject.tag))
            {
                Vector3 diff = collider.gameObject.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    focusedPredator = collider.gameObject;
                    distance = curDistance;
                }
            }
        }

    }*/

    protected void findClosestPredatorsInSight()
    {
        focusedPredator = null;

        float distance = lineOfSight;
        Vector3 position = transform.position;
        
        foreach (GameObject predator in predatorList)
        {
            Vector3 diff = predator != null ? predator.transform.position - position : Vector3.positiveInfinity;
            float curDistance = diff.sqrMagnitude;
                         
            if (curDistance < distance)
            {
                focusedPredator = predator;
                distance = curDistance;
            }
            
        }

    }

    protected bool check_for_predators()
    {
        findClosestPredatorsInSight();
        return focusedPredator != null;
    }

    public void getHunted(GameObject predator)
    {
        predatorList.Add(predator);
    }

    public void stopHunting(GameObject predator)
    {
        predatorList.Remove(predator);
    }

    protected void findClosestHideoutInSight(GameObject threat)
    {
        focusedHideout = null;

        float distance = lineOfSight;
        Vector3 position = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, lineOfSight);

        foreach (Collider2D collider in colliders)
        {
            PlantModel plant;
            if (collider.gameObject.tag == "plant")
            {
                plant = collider.gameObject.GetComponent<PlantModel>();
                if (plant.isHideout)
                {
                    Vector3 diff = collider.gameObject.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        //The hideout must be at least closer to the calango than to the predator
                        float distanceToPredator = (threat.transform.position - collider.gameObject.transform.position).sqrMagnitude;
                        if(distanceToPredator > curDistance)
                        {
                            focusedHideout = collider.gameObject;
                            distance = curDistance;
                        }
                    }
                }
            }
        }

    }

    protected void findClosestShadowInSight()
    {
        focusedShadowProducer = null;

        float distance = lineOfSight * 4;
        Vector3 position = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, distance
            );

        foreach (Collider2D collider in colliders)
        {
            PlantModel plant;
            if (collider.gameObject.tag == "plant")
            {
                plant = collider.gameObject.GetComponent<PlantModel>();

                if (plant.sunProtection)
                {
                    Vector3 diff = collider.gameObject.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {                        
                        focusedShadowProducer = collider.gameObject;
                        distance = curDistance;                        
                    }
                }
            }
        }

    }

    private void clampPosition()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, limitLeft(transform.position.y), limitRigth(transform.position.y)), Mathf.Clamp(transform.position.y, limitDown(transform.position.x), limitUp(transform.position.x)), transform.position.z);
    }


    float limitUp(float x)
    {

        // (percentage of distance from the center) * (half map size in the y axis) - offset
       return (1 - (Mathf.Abs(x) / (mapGenerator.mapSize * mapGenerator.tileSize / 2))) * (mapGenerator.mapSize * mapGenerator.tileSize /4) - (mapGenerator.TotalTerrain * mapGenerator.tileSize / 4);
            
    }

    float limitDown(float x)
    {
        // -(percentage of distance from the center) * (half map size in the y axis) + offset
        return - (1 -(Mathf.Abs(x) / (mapGenerator.mapSize * mapGenerator.tileSize / 2))) * (mapGenerator.mapSize * mapGenerator.tileSize / 4) - (mapGenerator.TotalTerrain * mapGenerator.tileSize / 4);
        
    }

    float limitRigth(float y)
    {
        // (percentage of distance from the center) * (half map size in the x axis) 
        return (1 -  (Mathf.Abs(y + (mapGenerator.TotalTerrain * mapGenerator.tileSize / 4)) / (mapGenerator.mapSize * mapGenerator.tileSize / 4))) * (mapGenerator.mapSize * mapGenerator.tileSize / 2);
    }


    float limitLeft(float y)
    {
        // (percentage of distance from the center) * (half map size in the x axis) 
        return - (1 - (Mathf.Abs(y + (mapGenerator.TotalTerrain * mapGenerator.tileSize / 4)) / (mapGenerator.mapSize * mapGenerator.tileSize / 4))) * (mapGenerator.mapSize * mapGenerator.tileSize / 2);
    }
}
