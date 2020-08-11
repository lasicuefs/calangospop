using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SecondaryAnimalBehaviour : AnimalModel
{

    public float chanceOfReproducingPerHour = .05f;
    public GameObject childPrefab;

    new void newHour()
    {
        base.newHour();
        randomReproducing();
    }

    public void randomReproducing()
    {
        if (Random.value < chanceOfReproducingPerHour)
        {
            float offset = 2.0f;
            mapGenerator.generateCustomAnimal(childPrefab, transform.position.x+offset, transform.position.y+offset);
        }
    }
}
