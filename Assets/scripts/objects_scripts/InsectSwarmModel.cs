using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSwarmModel : MonoBehaviour {

    public float energyWhenConsumed = 30;
    public float hidrationWhenConsumed = 30;

    public float reproductionPercetagePerHour = 25;
    public float migrationAmountPerHour = 1;

    protected RegistryController registry;
    int maximumAmount = 0;

    int InsectCount = 0;
    public int insectCount
    {
        get
        {
            return InsectCount;
        }

        set
        {
            InsectCount = value;
        }
    }

    public void newInsects()
    {
        int newInsects = (int)((InsectCount * reproductionPercetagePerHour) / 100 + migrationAmountPerHour);
        if ((InsectCount + newInsects) > maximumAmount) newInsects = maximumAmount - insectCount;
        InsectCount += newInsects;
       
        registry.registerInsects(newInsects);
    }

    public void Initialize(int initialAmount, int maximun, float energyWhenConsumed, RegistryController registry)
    {
        this.energyWhenConsumed = energyWhenConsumed;
        this.registry = registry;
        InsectCount = initialAmount;
        maximumAmount = maximun;
        registry.registerInsects(initialAmount);
    }

    public void getEaten()
    {
        InsectCount--;
        registry.unregisterInsects(1);
    }

    public void getDestroyed (){
        registry.unregisterInsects(InsectCount);
    }
}
