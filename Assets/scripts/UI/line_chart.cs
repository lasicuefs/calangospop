using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_chart : MonoBehaviour {
    
    
    Dictionary<string, GameObject> lines = new Dictionary<string, GameObject>();

    private DD_DataDiagram m_DataDiagram;
    registryController registry;
    float counter;
    public float refreshTime = 1.0f;

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

        m_DataDiagram.PreDestroyLineEvent += (s, e) => { lines.Remove(e.line.GetComponent<DD_Lines>().lineName); };

        AddALine("Calangos", Color.blue); 
        AddALine("Predadores", Color.red);
        AddALine("Vegetação", Color.green);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        counter += Time.deltaTime;
        if(counter > refreshTime)
        {
            updateData(registry.getCalangosList().Count, registry.getPredatorList().Count, registry.getediblePlantsList().Count);
            counter = 0.0f;
        }
       
    }

    public void updateData( int qtCalango, int qtPredadores, int qtVegetacao)
    {
        m_DataDiagram.InputPoint(lines["Calangos"], new Vector2(1, qtCalango));
        m_DataDiagram.InputPoint(lines["Predadores"], new Vector2(1, qtPredadores));
         m_DataDiagram.InputPoint(lines["Vegetação"], new Vector2(1, qtVegetacao));
    }

    private void ContinueInput(float f)
    {

        if (null == m_DataDiagram)
            return;
        
        foreach (KeyValuePair<string, GameObject> l in lines)
        {
            //m_DataDiagram.InputPoint(l.Value, new Vector2(0.1f, 2f));
        }
    }

    public void onButton()
    {

        if (null == m_DataDiagram)
            return;

        foreach (KeyValuePair<string, GameObject> l in lines)
        {
            m_DataDiagram.InputPoint(l.Value, new Vector2(1, 4f));
        }
    }
    

}
