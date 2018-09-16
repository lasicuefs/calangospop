using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSwarmModel : MonoBehaviour {

    public float energyWhenConsumed = 30;
    public float hidrationWhenConsumed = 30;

    public float reproductionPercetagePerHour = 30;
    public float migrationAmountPerHour = 2;

    protected registryController registry;
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
        InsectCount += newInsects;
        if (InsectCount > maximumAmount) InsectCount = maximumAmount;
        registry.registerInsects(newInsects);
    }

    public void Initialize(int initialAmount, int maximun, registryController registry)
    {
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
