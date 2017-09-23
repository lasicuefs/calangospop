using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelUnidadeController : MonoBehaviour {

	public Text textEnergy;
	public Text textState;
	public Text textAge;

	animalModel selected;
	ClickableBehaviour oldUnit;
	Camera followCamera;

	// Use this for initialization
	void Start () {
		followCamera = GameObject.Find("unitCamera").GetComponent<Camera>();
	}

	public void select_Animal(ClickableBehaviour target){
		if(oldUnit != null) oldUnit.GetComponent<SpriteRenderer>().enabled = false;
		target.GetComponent<SpriteRenderer>().enabled = true;

		selected = target.GetComponentInParent<animalModel>();
		oldUnit = target;
	}
	
	// Update is called once per frame
	void Update () {
		/*RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay (ray.origin, ray.direction * 10, Color.yellow);

		if (Physics.Raycast (ray, hit))
		{  
			//  Do whatever you want to detect what's been hit from the data stored in the "hit" variable - this should rotate it...

			Transform transform = hit.collider.GetComponent(Transform);

			if (transform )
			{
				Debug.Log (transform.gameObject.name);
			}

		}*/

		if (selected != null) {
			followCamera.transform.position = selected.transform.position + new Vector3 (0, .1f, -1);

			textEnergy.text = "Energia: " + (selected.Get_Energy()/selected.maxEnergy*100).ToString("F0") +" %";
			textState.text = "Estado: " + selected.currState;
			textAge.text = "Idade: " + selected.getAge() + " anos";
		}
	}
}
