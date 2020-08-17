using Assets.scripts.game_managers;
using UnityEngine;
using UnityEngine.UI;

static class SelectionTypes
{
    public const int ANIMAL = 0;
    public const int PLANT = 1; 

}

public class PainelUnidadeController : MonoBehaviour {

	public Text textState;
	public Text textAge;
    public Image energyBar;
    public Image tempBar;

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

                //textEnergy.text = "Energia: " + (animal.Get_Energy() / animal.maxEnergy * 100).ToString("F0") + " %";
                energyBar.fillAmount = animal.Get_Energy() /animal.maxEnergy;
                textState.text = GameTextController.getText(LanguageConstants.STATE)+": " + animal.currState;
                textAge.text = GameTextController.getText(LanguageConstants.AGE)+": " + animal.getAge() + " "+ GameTextController.getText(LanguageConstants.YEARS);

                CalangoBehaviour calango = selected.GetComponentInParent<CalangoBehaviour>();
                if (calango != null) tempBar.fillAmount = Mathf.Clamp((calango.getBodyTemp() - calango.lowBodyTempThreshold) / (calango.maxBodyTemp-calango.lowBodyTempThreshold), 0, 1);
                else tempBar.fillAmount = 0;

                animalPanel.SetActive(true);
                plantPanel.SetActive(false);
            } else if (selectionType == SelectionTypes.PLANT)
            {
                PlantModel plant = selected.GetComponentInParent<PlantModel>();
                InsectSwarmModel swarm = plant.getSwarn();
                followCamera.transform.position = selected.transform.position + new Vector3(0, .1f, -1);

                 textName.text = GameTextController.getText(LanguageConstants.NAME)+": " + plant.plantName;               
                 textType.text = (plant.hasInsects ? GameTextController.getText(LanguageConstants.HAS_INSECTS)+"\n" : "") + (plant.isHideout ? GameTextController.getText(LanguageConstants.HAS_HIDEOUT)+"\n" : "") + (plant.sunProtection ? GameTextController.getText(LanguageConstants.HAS_SHADOWS)+"\n" : "");

                if (swarm != null)
                {
                    textInsect.text = GameTextController.getText(LanguageConstants.INSECT_NAME)+": " + swarm.insectCount;
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

    public void disapear()
    {
        transform.position = new Vector3(10000,10000,0);
    }
}
