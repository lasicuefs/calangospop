using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registryController : MonoBehaviour {

	List<GameObject> animals = new List<GameObject>();
	List<GameObject> calangos = new List<GameObject>();
	List<GameObject> ediblePlants = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Grapher.Log (calangos.Count, "calangos");
		Grapher.Log (ediblePlants.Count, "edibles");
	}

	public void registerCalango(GameObject calango){
		calangos.Add (calango);
		animals.Add (calango);
	}

	public void unregisterCalango(GameObject calango){
		calangos.Remove (calango);
		animals.Remove (calango); 
	}

	public List<GameObject> getCalangosList(){
		return calangos;
	}

	public List<GameObject> getAnimalList(){
		return animals;
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
}
