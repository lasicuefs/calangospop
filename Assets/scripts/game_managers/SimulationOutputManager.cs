using System.IO;
using UnityEngine;

public class SimulationOutputManager : MonoBehaviour
{
    float counter;
    float simTime;
    public float refreshTimeInDays = 0.01f;
    public float limitInDay = 4f;
    TemporalManager temporal;
    RegistryController registry;
    public string simName;
    string fileName;

    // Use this for initialization
    void Start()
    {
        temporal = gameObject.GetComponent<TemporalManager>();
        registry = gameObject.GetComponent<RegistryController>();

        fileName = Application.dataPath + "/simulationOutput-" + simName + ".csv";
        Debug.Log(fileName);

        StreamWriter writer = new StreamWriter(fileName);
        writer.WriteLine("Time;Lizards;Predators;Vegetation;Competitors");
        writer.Close();
    }

    private void FixedUpdate()
    {
        counter += Time.deltaTime / (float)temporal.secondsForADay;
        if (counter > refreshTimeInDays && simTime < limitInDay)
        {
            simTime += counter;
            updateData(simTime, registry.getCalangosList().Count, registry.getPredatorList().Count, registry.getediblePlantsList().Count + registry.getInsectCount(), registry.getCompetitorList().Count);
            counter = 0.0f;
        }

    }

    public void updateData(float year, int qtCalango, int qtPredadores, int qtVegetacao, int qtCompetidores)
    {
        StreamWriter writer = new StreamWriter(fileName, true);
        writer.WriteLine(string.Format("{0:0.00};{1};{2};{3};{4}", year, qtCalango, qtPredadores,  qtVegetacao, qtCompetidores));
        writer.Close();
    }


    }
