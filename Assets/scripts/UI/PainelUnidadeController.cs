using UnityEngine;
using UnityEngine.UI;

static class SelectionTypes
{
    public const int ANIMAL = 0;
    public const int PLANT = 1; 

}

public class PainelUnidadeController : MonoBehaviour {

	public Text textEnergy;
	public Text textState;
	public Text textAge;

    public Text textName;
    public Text textInsect;
    public Text textType;

    public GameObject animalPanel;
    public GameObject plantPanel;

    GameObject selected;
    int selectionType;

	ClickableBehaviour oldUnit;
	Camera followCamera;
    RectTransform rect;
    Vector3 initiaPos;

    // Use this for initialization
    void Start () {
		followCamera = GameObject.Find("unitCamera").GetComponent<Camera>();
        rect = gameObject.GetComponent<RectTransform>();
        initiaPos = rect.localPosition;
    }

	public void select_unit(ClickableBehaviour target){
		if(oldUnit != null) oldUnit.GetComponent<SpriteRenderer>().enabled = false;
		target.GetComponent<SpriteRenderer>().enabled = true;

        if (target.GetComponentInParent<AnimalModel>() != null) selectionType = SelectionTypes.ANIMAL;
        else selectionType = SelectionTypes.PLANT;

        selected = target.gameObject;
		oldUnit = target;
        rect.localPosition = initiaPos;
    }
	
	// Update is called once per frame
	void Update () {

        if (selected != null)
        {
            if (selectionType == SelectionTypes.ANIMAL) {
                AnimalModel animal = selected.GetComponentInParent<AnimalModel>();
                followCamera.transform.position = selected.transform.position + new Vector3(0, .1f, -1);

                textEnergy.text = "Energia: " + (animal.Get_Energy() / animal.maxEnergy * 100).ToString("F0") + " %";
                textState.text = "Estado: " + animal.currState;
                textAge.text = "Idade: " + animal.getAge() + " anos";

                animalPanel.SetActive(true);
                plantPanel.SetActive(false);
            } else if (selectionType == SelectionTypes.PLANT)
            {
                PlantModel plant = selected.GetComponentInParent<PlantModel>();
                InsectSwarmModel swarm = plant.getSwarn();
                followCamera.transform.position = selected.transform.position + new Vector3(0, .1f, -1);

                 textName.text = "Nome: " + plant.name;               
                 textType.text = "Atributos: " + (plant.hasInsects ? "Contém insetos. " : "") + (plant.isHideout ? "Esconderijo contra predadores. " : "") + (plant.sunProtection ? "Prejeta sombras. " : "");

                if (swarm != null)
                {
                    textInsect.text = "Insects: " + swarm.insectCount;
                    textInsect.gameObject.SetActive(true);
                } else
                {
                    textInsect.gameObject.SetActive(false);
                }
                plantPanel.SetActive(true);
                animalPanel.SetActive(false);
            }
     
        } else
        {
            plantPanel.SetActive(false);
            animalPanel.SetActive(false);
            rect.localPosition = new Vector3(2000, 0, 0);
        }
	}
}
