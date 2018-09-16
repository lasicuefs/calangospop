using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitfulPlant : PlantModel {

	public int avarageFruitPerHour = 0;
	public float fruitRadius = 1;
	public GameObject fruitPreab;

	public void initialize(registryController registry, GameMapGenerator mapGenerator){
		this.registry = registry;
		this.mapGenerator = mapGenerator;
	}

	void newHour(){
		base.newHour();
		int randOffSet = Random.Range (0, avarageFruitPerHour*2) - avarageFruitPerHour;
		int fruitAmount = avarageFruitPerHour - randOffSet;

		float angleOffset = Random.value * 2f * Mathf.PI;
		for (int i = 0; i < fruitAmount; i++) {
			float angle = (2f * Mathf.PI * i / fruitAmount) + angleOffset;
			float radius = Random.value * fruitAmount;

			float xOffset = Mathf.Sin (angle) * radius;
			float yOffset = Mathf.Cos (angle) * radius / 2f;
			//Vector3 offSet = Random.insideUnitCircle * selectedSize;
			((GameMapGenerator)mapGenerator).generateCustomPlant (fruitPreab, transform.position.x - xOffset, transform.position.y - yOffset);
		}
	} 
}
