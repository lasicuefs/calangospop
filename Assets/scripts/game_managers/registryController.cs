using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class Deaths
{
    public const string INANICAO = "INANICAO";
    public const string PREDADOR = "PREDADOR";
    public const string INSOLACAO = "INSOLACAO";
    public const string NATURAL = "NATURAL";
}
public class registryController : MonoBehaviour {

	List<GameObject> animals = new List<GameObject>();
	List<GameObject> calangos = new List<GameObject>();
    List<GameObject> predators = new List<GameObject>();
    List<GameObject> ediblePlants = new List<GameObject>();

    int insectCount = 0;
    
    private Dictionary<string, int> deaths;

    // Use this for initialization
    void Start () {
        deaths = new Dictionary<string, int>();
    }
	
	public void registerCalango(GameObject calango){
		calangos.Add (calango);
		animals.Add (calango);
	}

	public void unregisterCalango(GameObject calango, string reason){
		calangos.Remove (calango);
		animals.Remove (calango);

        if (deaths.ContainsKey(reason))
        {
            deaths[reason]++;
        }
        else
        {
            deaths.Add(reason, 1);
        }
    }

    public void registerPredator(GameObject predator)
    {
        predators.Add(predator);
        animals.Add(predator);
    }

    public void unregisterPredator(GameObject predator)
    {
        predators.Remove(predator);
        animals.Remove(predator);
    }

    public List<GameObject> getCalangosList(){
		return calangos;
	}

	public List<GameObject> getAnimalList(){
		return animals;
	}

    public List<GameObject> getPredatorList()
    {
        return predators;
    }

    public void registerEdiblePlant(GameObject plant){
		ediblePlants.Add (plant);
	}

	public void unregisterEdiblePlant(GameObject plant){
		ediblePlants.Remove (plant);
	}

	public List<GameObject> getediblePlantsList(){
		return ediblePlants;
	}

    public void registerInsects(int amount)
    {
        insectCount += amount;
    }

    public void unregisterInsects(int amount)
    {
        insectCount -= amount;
    }

    public int getInsectCount()
    {
        return insectCount;
    }

    public int getDeathsByPredation()
    {
        if (!deaths.ContainsKey(Deaths.PREDADOR)) return 0;
        return deaths[Deaths.PREDADOR];
    }

    public int getDeathsByStarvation()
    {
        if (!deaths.ContainsKey(Deaths.INANICAO)) return 0;
        return deaths[Deaths.INANICAO];
    }

    public int getDeathsByAge()
    {
        if (!deaths.ContainsKey(Deaths.NATURAL)) return 0;
        return deaths[Deaths.NATURAL];
    }

    public int getDeathsByHeat()
    {
        if (!deaths.ContainsKey(Deaths.INSOLACAO)) return 0;
        return deaths[Deaths.INSOLACAO];
    }
}
