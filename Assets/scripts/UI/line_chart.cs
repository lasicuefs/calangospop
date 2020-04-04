using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_chart : MonoBehaviour {
    
    
    Dictionary<string, GameObject> lines = new Dictionary<string, GameObject>();

    private DD_DataDiagram m_DataDiagram;
    registryController registry;
    float counter;
    public float refreshTimeInDays = 0.01f;
    TemporalManager temporal;

    void AddALine(string name, Color colorI)
    {

        if (null == m_DataDiagram)
            return;

        Color color = colorI;
        GameObject line = m_DataDiagram.AddLine(name, color);
        if (null != line)
            lines.Add(name, line);
    }
    
    void Start()
    {

        GameObject dd = GameObject.Find("DataDiagram");
        if (null == dd)
        {
            Debug.LogWarning("can not find a gameobject of DataDiagram");
            return;
        }
        m_DataDiagram = dd.GetComponent<DD_DataDiagram>();

        registry = gameObject.GetComponent<registryController>();

        temporal = gameObject.GetComponent<TemporalManager>();

        m_DataDiagram.PreDestroyLineEvent += (s, e) => { lines.Remove(e.line.GetComponent<DD_Lines>().lineName); };

        AddALine(GameTextController.getText("lizard_name"), Color.blue);
        AddALine(GameTextController.getText("predator_name"), Color.red);
        AddALine(GameTextController.getText("competitor_name"), Color.yellow);
        AddALine(GameTextController.getText("food_name"), Color.green);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        counter += Time.deltaTime/(float)temporal.secondsForADay;
        if(counter > refreshTimeInDays)
        {
            updateData(registry.getCalangosList().Count, registry.getPredatorList().Count, registry.getediblePlantsList().Count, registry.getCompetitorList().Count);
            counter = 0.0f;
        }
       
    }

    public void updateData( int qtCalango, int qtPredadores, int qtVegetacao, int qtCompetidores)
    {
        m_DataDiagram.InputPoint(lines[GameTextController.getText("lizard_name")], new Vector2(1, qtCalango));
        m_DataDiagram.InputPoint(lines[GameTextController.getText("predator_name")], new Vector2(1, qtPredadores));
        m_DataDiagram.InputPoint(lines[GameTextController.getText("competitor_name")], new Vector2(1, qtCompetidores));
        m_DataDiagram.InputPoint(lines[GameTextController.getText("food_name")], new Vector2(1, qtVegetacao));
    }

 
    

}
