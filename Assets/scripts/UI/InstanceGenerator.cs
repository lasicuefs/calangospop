using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstanceGenerator : MonoBehaviour {

	bool selected = false;
	GameObject selectedPrefab;
	float selectedSize = 1;
	int selectedAmount = 10;
	int selectedType = 0;

	GameMapGenerator mapGenerator;

	// Use this for initialization
	void Start () {
		mapGenerator = GameObject.Find ("MapController").GetComponent<GameMapGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = new Vector3(mousePosition.x, mousePosition.y, 1);

		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) {
			if (selected) {
				for (int i = 0; i < selectedAmount; i++) {
					float angle = 2f * Mathf.PI * i / selectedAmount;
					float radius = Random.value * selectedSize;
					float xOffset = Mathf.Sin (angle) * radius;
					float yOffset = Mathf.Cos (angle) * radius / 2f;
					//Vector3 offSet = Random.insideUnitCircle * selectedSize;
					switch (selectedType) {
					case 0:
						mapGenerator.generateCustomPlant (selectedPrefab, mousePosition.x - xOffset, mousePosition.y - yOffset);
						break;
					case 1:
						mapGenerator.generateCustomAnimal (selectedPrefab, mousePosition.x - xOffset, mousePosition.y - yOffset);
						break;
					}

				}
				selected = false;
				GetComponent<SpriteRenderer>().enabled = false;
			} 
		}
		
	}

	public void select(GameObject prefab, int amount, float radius, int type){
		selectedAmount = amount;
		selectedSize = radius;
		selectedPrefab = prefab;
		selectedType = type;
		selected = true;

		GetComponent<SpriteRenderer>().enabled = true;
		Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
		Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
		float scaleX = (radius * 2) / local_sprite_size.x;
		float scaleY = (radius * 2) / local_sprite_size.y;
		transform.localScale = new Vector3( scaleX, scaleY, 1f );
	}
}
