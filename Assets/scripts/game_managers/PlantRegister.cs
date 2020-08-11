using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRegister : MonoBehaviour {

    protected RegistryController registry;
    protected MapGenerator mapGenerator;

    // Use this for initialization
    void Start () {
        GameObject gameController = GameObject.Find("MapController");
        registry = gameController.GetComponent<RegistryController>();
        mapGenerator = gameController.GetComponent<MapGenerator>();

        Register();
    }
	
	void Register()
    {
        foreach (Transform child in transform)
        {
            GameObject plant = child.gameObject;
            plant.GetComponent<PlantModel>().initialize(registry, mapGenerator);
            registry.registerEdiblePlant(plant);
        }
    }
}
