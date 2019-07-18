using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstanceGenerator : MonoBehaviour {

    public GameObject objPreview;
	bool selected = false;
	GameObject selectedPrefab;
	float selectedSize = 1;
	int selectedAmount = 10;
	int selectedType = 0;
    int selectedPrice = 0;
    instance_button selectedButton;

	GameMapGenerator mapGenerator;
    Resources_Controller resControl;

    // Use this for initialization
    void Start () {
		mapGenerator = GameObject.Find ("MapController").GetComponent<GameMapGenerator> ();
        resControl = GameObject.Find("MapController").GetComponent<Resources_Controller>();
    }
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = new Vector3(mousePosition.x, mousePosition.y, 1);

        if (Mathf.Abs(mousePosition.y + mapGenerator.TotalTerrain * mapGenerator.tileSize / 4) * 2 + Mathf.Abs(mousePosition.x) > mapGenerator.mapSize * mapGenerator.tileSize/2 - selectedSize*2)
        {
            GetComponent<SpriteRenderer>().color = new Color(255,0,0,0.2f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.2f); ;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (selected)
                {
                    List<GameObject> children = new List < GameObject > ();
                    foreach (Transform child in transform)
                    {
                       
                        switch (selectedType)
                        {
                            case 0:
                                mapGenerator.generateCustomPlant(selectedPrefab, child.position.x, child.position.y);
                                break;
                            case 1:
                                mapGenerator.generateCustomAnimal(selectedPrefab, child.position.x, child.position.y);
                                break;
                        }
                        children.Add(child.gameObject);
                    }
                    children.ForEach(child => Destroy(child));

                    selected = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    resControl.decreaseBiomass(selectedPrice);
                    selectedButton.Deselect();
                }
            }

        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selected = false;
            GetComponent<SpriteRenderer>().enabled = false;
            selectedButton.Deselect();
            clear_children();
        }
		
	}

	public void select(GameObject prefab, int amount, float radius, int price, int type, instance_button selectedButton)
    {
		selectedAmount = amount;
		selectedSize = radius;
		selectedPrefab = prefab;
        selectedPrice = price;
        selectedType = type;
		selected = true;
        this.selectedButton = selectedButton;


        GetComponent<SpriteRenderer>().enabled = true;
		Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
		Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
		float scaleX = (radius * 2) / local_sprite_size.x;
		float scaleY = (radius * 2) / local_sprite_size.y;
		transform.localScale = new Vector3( scaleX, scaleY, 1f );

        generatePreview();

    }

    void generatePreview()
    {
        clear_children();

        for (int i = 0; i < selectedAmount; i++)
        {
            float angle = 2f * Mathf.PI * i / selectedAmount;
            float radius = Random.value * selectedSize;
            float xOffset = Mathf.Sin(angle) * radius;
            float yOffset = Mathf.Cos(angle) * radius / 2f;
            //Vector3 offSet = Random.insideUnitCircle * selectedSize;

            GameObject generated = GameObject.Instantiate(objPreview, new Vector3 (transform.position.x - xOffset, transform.position.y - yOffset, 1), transform.rotation);
            generated.transform.parent = transform;
            generated.GetComponent<SpriteRenderer>().sprite = selectedButton.transform.Find("Image").GetComponent<Image>().sprite;
            generated.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.4f) ;
        }
       
       
    }

    void clear_children()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        children.ForEach(child => Destroy(child));
    }
}
